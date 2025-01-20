using DesirionEngine._gamedata.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE._gamedata.Render.Light
{
    public class RadialLight
    {
        private GraphicsDevice _graphicsDevice;
        public RenderCanvas RenderCanvas;
        public Effect _lightShader;
        public Vector2 _position;
        public float _radius;
        public Color _color;
        public float _intensity;
        private Texture2D _texture; // Текстура для отрисовки света 
        private Texture2D _sceneTexture; // Текстура сцены 

        public RadialLight(ContentManager contentManager, GraphicsDevice graphicsDevice, RenderCanvas renderCanvas)
        {
            _graphicsDevice = graphicsDevice;
            RenderCanvas = renderCanvas;

            // Загружаем шейдер
            _lightShader = contentManager.Load<Effect>("Shaders/RadialLightShader");

            // Устанавливаем начальные параметры света
            _position = Vector2.Zero;
            _radius = 100f; // Радиус света 
            _color = Color.White; // Цвет света 
            _intensity = 1.0f; // Интенсивность света 

           
            _texture = contentManager.Load<Texture2D>("Sprites/LightSpriteTest");

            // Инициализация параметров шейдера
            InitializeShaderParameters();
        }

        private void InitializeShaderParameters()
        {
            // Установка матрицы проекции
            _lightShader.Parameters["WorldViewProjection"].SetValue(Matrix.CreateOrthographicOffCenter(0, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height, 0, 0, 1));

            // Убедимся, что параметр SceneTexture существует в шейдере
            if (_lightShader.Parameters["SceneTexture"] != null)
            {
                _lightShader.Parameters["SceneTexture"].SetValue(_texture); // Инициализация текстуры 
            }
        }

        public void Update(Vector2 newPosition)
        {
            _position = newPosition;
        }

        public void SetSceneTexture(Texture2D sceneTexture)
        {
            _sceneTexture = sceneTexture;
            if (_lightShader.Parameters["SceneTexture"] != null)
            {
                _lightShader.Parameters["SceneTexture"].SetValue(_sceneTexture);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Обновляем параметры шейдера
            UpdateShaderParameters();

            // Отрисовываем свет в позиции _position с радиусом _radius
            spriteBatch.Draw(_texture, new Rectangle((int)(_position.X - _radius), (int)(_position.Y - _radius), (int)(2 * _radius), (int)(2 * _radius)), Color.White);
        }

        private void UpdateShaderParameters()
        {
            _lightShader.Parameters["LightPosition"].SetValue(_position);
            _lightShader.Parameters["LightRadius"].SetValue(_radius);
            _lightShader.Parameters["LightColor"].SetValue(_color.ToVector4());
            _lightShader.Parameters["LightIntensity"].SetValue(_intensity);
        }

        public void SetRadius(float radius)
        {
            _radius = radius;
        }

        public void SetColor(Color color)
        {
            _color = color;
        }

        public void SetIntensity(float intensity)
        {
            _intensity = intensity;
        }

        public void SetPosition(Vector2 position)
        {
            _position = position;
        }
    }



}
