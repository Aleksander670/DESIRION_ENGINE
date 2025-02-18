﻿#if OPENGL
#define SV_POSITION POSITION 
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0 
#else 
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1 
#endif 

matrix WorldViewProjection;
float Brightness; // Новый параметр

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float4 Color : COLOR0;
    float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TexCoord : TEXCOORD0;
};

VertexShaderOutput MainVS(in VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput) 0;
    output.Position = mul(input.Position, WorldViewProjection);
    output.Color = input.Color;
    output.TexCoord = input.TexCoord;
    return output;
}

sampler2D TextureSampler : register(s0);

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float4 textureColor = tex2D(TextureSampler, input.TexCoord);
    float4 color = textureColor * input.Color;
    color.rgb *= Brightness;
    color.rgb = saturate(color.rgb);
    return color;
}

technique BasicColorDrawing
{
    pass P0
    {
        VertexShader = compile VS_SHADERMODEL MainVS();
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};
