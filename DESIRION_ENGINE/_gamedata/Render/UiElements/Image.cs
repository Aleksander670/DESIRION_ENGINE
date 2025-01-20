using DESIRION_ENGINE._gamedata.Render.GameElements;
using DesirionEngine._gamedata.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE._gamedata.Render.UiElements
{
    public class Image : UiElement
    {
        private Texture2D texture;
        public string Source { get; set; }
        public float Rotation;
        public OriginElement Origin { get; set; }
        public Color ImageColor { get; set; }

        public Image(GraphicsDevice graphicsDevice, ContentManager contentManager, string source, RenderCanvas renderCanvas, Vector2 size, Color color) : base(graphicsDevice, contentManager, renderCanvas)
        {
            this.Source = source;
            this.Size = size;
            this.ImageColor = color;
            InitializeImage();
        }

        public void InitializeImage()
        {
            texture = ContentManager.Load<Texture2D>(Source);
        }

        public override void Draw(SpriteBatch spriteBatch) //UI
        {
            DrawScripts(spriteBatch);

            Vector2 screenPosition = GetScreenPosition();
            Vector2 scaledSize = GetScaledSize();
            spriteBatch.Draw(texture, screenPosition, null, ImageColor, Rotation, GetSpriteOrigin(), scaledSize, SpriteEffects.None, 0f);
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 position) //GameInstant
        {
            DrawScripts(spriteBatch);

            spriteBatch.Draw(texture, position, null, ImageColor, Rotation, GetSpriteOrigin(), 1, SpriteEffects.None, 0f);
        }

        public Vector2 GetSpriteOrigin()
        {
            float width = texture.Width;
            float height = texture.Height;

            switch (Origin)
            {
                case OriginElement.Center:
                    return new Vector2(width / 2f, height / 2f);
                case OriginElement.Top:
                    return new Vector2(width / 2f, 0);
                case OriginElement.Bottom:
                    return new Vector2(width / 2f, height);
                case OriginElement.Left:
                    return new Vector2(0, height / 2f);
                case OriginElement.Right:
                    return new Vector2(width, height / 2f);
                default:
                    return Vector2.Zero;
            }
        }

        public override void Update(GameTime gameTime)
        {
            UpdateScripts(gameTime);
        }


    }




}
