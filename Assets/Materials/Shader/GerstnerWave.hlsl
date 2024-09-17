#ifndef ADDITIONAL_LIGHT_INCLUDED
#define ADDITIONAL_LIGHT_INCLUDED

void GerstnerWave_float(float4 wave, float3 position, float3 inputTangent, float3 inputBinormal, float time, 
                        out float3 wavePosition, out float3 outputTangent, out float3 outputBinormal)
{
    wavePosition = float3(0,0,0);
    outputTangent = float3(0,0,0);
    outputBinormal = float3(0,0,0);
    

    #ifdef SHADERGRAPH_PREVIEW
    // Shader graph preview code

        float steepness = wave.z;
        float wavelength = wave.w;
        float k = 2 * PI / wavelength;
        float c = sqrt(9.8 / k);
        float2 d = normalize(wave.xy);
        float f = k * (dot(d, position.xz) - c * time);
        float a = steepness / k;

        outputTangent = inputTangent += float3(
            -d.x - d.x * d.x * (steepness * sin(f)),
            d.x * (steepness * cos(f)),
            -d.x * d.y * (steepness * sin(f))
        );
        outputBinormal = inputBinormal += float3(
            -d.x * d.y * (steepness * sin(f)),
            d.y * (steepness * cos(f)),
            -d.y - d.y * d.y * (steepness * sin(f))
        );
        wavePosition = float3(
            d.x * (a * cos(f)),
            a * sin(f),
            d.y * (a * cos(f))
        );

    #else
    // Normal code

        float steepness = wave.z;
        float wavelength = wave.w;
        float k = 2 * PI / wavelength;
        float c = sqrt(9.8 / k);
        float2 d = normalize(wave.xy);
        float f = k * (dot(d, position.xz) - c * time);
        float a = steepness / k;

        outputTangent = inputTangent += float3(
            -d.x - d.x * d.x * (steepness * sin(f)),
            d.x * (steepness * cos(f)),
            -d.x * d.y * (steepness * sin(f))
        );
        outputBinormal = inputBinormal += float3(
            -d.x * d.y * (steepness * sin(f)),
            d.y * (steepness * cos(f)),
            -d.y - d.y * d.y * (steepness * sin(f))
        );
        wavePosition = float3(
            d.x * (a * cos(f)),
            a * sin(f),
            d.y * (a * cos(f))
        );

    #endif
}

#endif