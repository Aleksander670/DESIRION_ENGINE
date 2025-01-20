#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

// Матрица для преобразования координат
matrix WorldViewProjection;

// Параметры источника света
float2 LightPosition; // Позиция источника света
float LightRadius; // Радиус света
float4 LightColor; // Цвет света
float LightIntensity; // Интенсивность освещения

// Разрешение экрана
uniform float2 screenSize;

struct VertexShaderInput
{
    float4 Position : POSITION0;
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float2 TexCoord : TEXCOORD0; // Используем TEXCOORD для передачи позиции
};

// Вершинный шейдер
VertexShaderOutput MainVS(VertexShaderInput input)
{
    VertexShaderOutput output;
    output.Position = mul(input.Position, WorldViewProjection);
    output.TexCoord = input.Position.xy; // Передаем x и y координаты позиции
    return output;
}

// Пиксельный шейдер для света
float4 LightPass(VertexShaderOutput input) : COLOR
{
    float distanceToLight = distance(input.TexCoord, LightPosition);
    
    // Если объект находится за пределами радиуса света, возвращаем черный цвет
    if (distanceToLight > LightRadius)
    {
        return float4(0, 0, 0, 0); // Прозрачный
    }

    // Нормализуем интенсивность света
    float intensity = smoothstep(LightRadius, 0.0, distanceToLight);
    
    // Возвращаем цвет света с учетом интенсивности
    return LightColor * intensity * LightIntensity;
}

// Техника рендеринга
technique LightRendering
{
    pass P0
    {
        VertexShader = compile VS_SHADERMODEL MainVS();
        PixelShader = compile PS_SHADERMODEL LightPass();
    }
};