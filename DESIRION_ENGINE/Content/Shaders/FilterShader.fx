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
float4 ColorFilter; // Цветовой фильтр
float LightIntensity; // Интенсивность освещения

// Разрешение экрана
uniform float2 screenSize;

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float4 TextureCoordinate : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 TextureCoordinate : TEXCOORD0;
};

// Вершинный шейдер
VertexShaderOutput MainVS(VertexShaderInput input)
{
    VertexShaderOutput output;
    output.Position = mul(input.Position, WorldViewProjection);
    output.TextureCoordinate = input.TextureCoordinate;
    return output;
}

sampler2D TextureSampler : register(s0);

// Пиксельный шейдер
float4 MainPS(VertexShaderOutput input) : COLOR
{
    // Получаем цвет текстуры
    float4 texColor = tex2D(TextureSampler, input.TextureCoordinate);

    // Вычисляем расстояние до источника света
    float distanceToLight = distance(input.TextureCoordinate * screenSize, LightPosition);

    // Нормализуем интенсивность света
    float intensity = saturate(1.0 - (distanceToLight / LightRadius));

    // Применяем цветовой фильтр
    float4 filteredColor = texColor * ColorFilter;

    // Смешиваем цвета с учетом интенсивности света
    float4 finalColor = lerp(filteredColor, LightColor * intensity * LightIntensity, intensity);

    return saturate(finalColor);
}

// Техника рендеринга
technique BasicColorDrawing
{
    pass P0
    {
        VertexShader = compile VS_SHADERMODEL MainVS();
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
}