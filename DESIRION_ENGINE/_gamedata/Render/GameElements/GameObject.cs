using DESIRION_ENGINE._gamedata.API;
using DesirionEngine._gamedata.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE._gamedata.Render.GameElements
{

    public enum OriginSprite
    {
        Center,
        Top,
        Bottom,
        Left,
        Right
    }

    public abstract class GameObject
    {
        public GraphicsDevice GraphicsDevice;
        public ContentManager ContentManager;
        public RenderCanvas RenderCanvas;
        public UiCanvas UiCanvas;

        public string texturePath;
        public Texture2D texture;
        
        public string Type;
        public bool IsActive = true;
        public int Width;
        public int Height;
        public Vector2 Position;
        public OriginSprite Origin { get; private set; }
        public float Rotation;

        private List<Script> Scripts = new List<Script>();


        public float Speed { get; set; } = 0f;


        protected GameObject(GraphicsDevice graphicsDevice, ContentManager contentManager, RenderCanvas renderCanvas, UiCanvas uiCanvas, string texturePath, int width, int height, OriginSprite origin)
        {
            this.GraphicsDevice = graphicsDevice;
            this.ContentManager = contentManager;
            this.RenderCanvas = renderCanvas;
            this.UiCanvas = uiCanvas;
            this.texturePath = texturePath;
            this.Width = width;
            this.Height = height;
            this.Origin = origin;

            Initialize(texturePath);
        }

        protected void Initialize(string texturePath)
        {
            try
            {
                texture = ContentManager.Load<Texture2D>(texturePath);
            }
            catch (Exception ex)
            {
                texture = CreateNullTexture();
            }
        }

        public void Update()
        {
            foreach (var script in Scripts)
            {
                script.Update(this);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, null, Color.White, Rotation, GetSpriteOrigin(), 1.0f, SpriteEffects.None, 0f);

            foreach (var script in Scripts)
            {
                script.Draw(this, spriteBatch);
            }

        }

        public void AddScript(Script script)
        {
            Scripts.Add(script);
            script.Initialize(this);
        }

        public void AddPlayerScript(Camera camera, Script script)
        {
            Scripts.Add(script);
            script.Initialize(camera, this);
        }

        protected Vector2 GetSpriteOrigin()
        {
            switch (Origin)
            {
                case OriginSprite.Center:
                    return new Vector2(Width / 2f, Height / 2f);
                case OriginSprite.Top:
                    return new Vector2(Width / 2f, 0);
                case OriginSprite.Bottom:
                    return new Vector2(Width / 2f, Height);
                case OriginSprite.Left:
                    return new Vector2(0, Height / 2f);
                case OriginSprite.Right:
                    return new Vector2(Width, Height / 2f);
                default:
                    return Vector2.Zero;
            }
        }


        private Texture2D CreateNullTexture()
        {
            Texture2D texture = new Texture2D(GraphicsDevice, Width, Height);
            Color[] data = new Color[Width * Height]; // Одномерный массив для хранения данных цвета

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    // Определяем индекс в одномерном массиве
                    int index = i * Width + j;

                    if ((i + j) % 2 == 0)
                    {
                        data[index] = Color.MediumVioletRed;
                    }
                    else
                    {
                        data[index] = Color.Black;
                    }
                }
            }

            texture.SetData(data); // Устанавливаем данные текстуры
            return texture;
        }



    }

}
