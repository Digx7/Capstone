Shader "Custom/Waves_UsingGerstner"
{
    Properties
    {
        _BaseMap("Base Map", 2D) = "white" {}
        _CrestColor("Crest Color", Color) = (1,1,1,1)
        _PeakColor("Peak Color", Color) = (1,1,1,1)
        _MiddleColor("Middle Color", Color) = (1,1,1,1)
        _TroughtColor("Trought Color", Color) = (0,0,0,1)
        _ColorRange("Color Range (Crest, Peak, Middle)", Vector) = (0.8, 0.5, -0.5, 0)
        _WaveA ("Wave A (dir, steepness, wavelength)", Vector) = (1,0,0.5,10)
        _WaveB ("Wave B", Vector) = (0,1,0.25,20)
        _WaveC ("Wave C", Vector) = (0,1,0.25,20)
    }

    // chang

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            #define PI       3.14159

            struct Attributes
            {
                float4 positionOS   : POSITION;
                float4 normalOS     : NORMAL;
                float2 uv: TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS  : SV_POSITION;
                float4 normalCS    : NORMAL;
                float4 shadowCoords : TEXCOORD3;
                float2 uv: TEXCOORD0;
                float4 color: MYSEMANIC;
            };
            // change

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
            float4 _CrestColor, _PeakColor, _MiddleColor,_TroughtColor;
            float4 _ColorRange;
            float4 _BaseMap_ST;
            float4 _WaveA, _WaveB, _WaveC;
            CBUFFER_END

            float3 GerstnerWave (float4 wave, float3 position, inout float3 tangent, inout float3 binormal)
            {
                float steepness = wave.z;
                float wavelength = wave.w;
                float k = 2 * PI / wavelength;
                float c = sqrt(9.8 / k);
                float2 d = normalize(wave.xy);
                float f = k * (dot(d, position.xz) - c * _Time.y);
                float a = steepness / k;

                tangent += float3(
                    -d.x - d.x * d.x * (steepness * sin(f)),
                    d.x * (steepness * cos(f)),
                    -d.x * d.y * (steepness * sin(f))
                );
                binormal += float3(
                    -d.x * d.y * (steepness * sin(f)),
                    d.y * (steepness * cos(f)),
                    -d.y - d.y * d.y * (steepness * sin(f))
                );
                return float3(
                    d.x * (a * cos(f)),
                    a * sin(f),
                    d.y * (a * cos(f))
                );
            }

            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                // Modify Verticies =====
                float3 gridPoint = IN.positionOS.xyz;
                float3 tangent = float3(1, 0, 0);
                float3 binormal = float3(0, 0, 1);
                float3 position = gridPoint;
                position += GerstnerWave(_WaveA, gridPoint, tangent, binormal);
                position += GerstnerWave(_WaveB, gridPoint, tangent, binormal);
                position += GerstnerWave(_WaveC, gridPoint, tangent, binormal);
                float3 normal = normalize(cross(binormal, tangent));

                IN.positionOS.xyz = position;
                IN.normalOS.xyz = normal;

                float4 waveColor;
                if(position.y >= _ColorRange.x) waveColor = _CrestColor;
                else if(position.y < _ColorRange.x && position.y >= _ColorRange.y) waveColor = _PeakColor;
                else if(position.y < _ColorRange.y && position.y >= _ColorRange.z) waveColor = _MiddleColor;
                else waveColor = _TroughtColor;

                OUT.color = waveColor;

                OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.normalCS = IN.normalOS;

                // END Modify Vertices ======

                // Shadows =======

                // Get the VertexPositionInputs for the vertex position  
                VertexPositionInputs positions = GetVertexPositionInputs(IN.positionOS.xyz);

                // Convert the vertex position to a position on the shadow map
                float4 shadowCoordinates = GetShadowCoord(positions);

                // Pass the shadow coordinates to the fragment shader
                OUT.shadowCoords = shadowCoordinates;

                // END Shadows =======

                OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);

                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                // Get the value from the shadow map at the shadow coordinates
                half shadowAmount = MainLightRealtimeShadow(IN.shadowCoords);

                // Set the fragment color to the shadow value
                // return shadowAmount;
                
                float4 texel = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, IN.uv);
                // float4 trueColor = _Color * shadowAmount;

                float height = IN.positionCS.y;
                float4 color = IN.color;

                float4 trueColor = color * shadowAmount;

                return texel * trueColor;
            }
            ENDHLSL
        }
    }
}
