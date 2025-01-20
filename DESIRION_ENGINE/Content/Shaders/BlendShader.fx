#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

matrix WorldViewProjection;

// Указываем сэмплеры для текстур
sampler2D SceneTexture : register(s0);
sampler2D LightTexture : register(s1);

// Входные данные для вершинного шейдера
struct VertexInput
{
    float4 Position : POSITION;
    float2 TexCoord : TEXCOORD0;
};

// Выходные данные для пиксельного шейдера
struct PixelInput
{
    float4 Position : SV_POSITION;
    float2 TexCoord : TEXCOORD0;
};

// Вершинный шейдер
PixelInput VertexShaderFunction(VertexInput input)
{
    PixelInput output;
    output.Position = input.Position; // Передаем позицию
    output.TexCoord = input.TexCoord; // Передаем текстурные координаты
    return output;
}

// Пиксельный шейдер
float4 PixelShaderFunction(PixelInput input) : SV_Target
{
    // Получаем цвета из текстур
    float4 sceneColor = tex2D(SceneTexture, input.TexCoord);
    float4 lightColor = tex2D(LightTexture, input.TexCoord);

    // Смешиваем текстуры на основе альфа-канала освещения
    float4 blendedColor = sceneColor * (1 - lightColor.a) + lightColor * lightColor.a;

    return blendedColor;
}

// Техника и проход
technique Blend
{
    pass P0
    {
        VertexShader = compile vs_4_0 VertexShaderFunction();
        PixelShader = compile ps_4_0 PixelShaderFunction();
    }
}