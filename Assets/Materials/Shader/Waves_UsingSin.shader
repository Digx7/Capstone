Shader "Custom/Waves_UsingSin"
{
    Properties
    {
        _BaseMap("Base Map", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _Amplitude ("Amplitude", Float) = 1
        _Wavelength ("Wavelength", Float) = 10
        _Speed ("Speed", Float) = 1
    }

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
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
            float4 _Color;
            float4 _BaseMap_ST;
            float _Amplitude, _Wavelength, _Speed;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                // Modify Verticies =====
                float3 position = IN.positionOS.xyz;
                float k = 2 * PI / _Wavelength;
                float f = k * (position.x - _Speed * _Time.y);

                position.y = _Amplitude * sin(f);

                float3 tangent = normalize(float3(1, k * _Amplitude * cos(f), 0));
                float3 normal = float3(-tangent.y, tangent.x, 0);

                IN.positionOS.xyz = position;
                IN.normalOS.xyz = normal;

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
                float4 trueColor = _Color * shadowAmount;
                return texel * trueColor;
            }
            ENDHLSL
        }
    }
}
