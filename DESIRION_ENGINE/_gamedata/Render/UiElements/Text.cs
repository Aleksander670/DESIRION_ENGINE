using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesirionEngine._gamedata.Render;

namespace DESIRION_ENGINE._gamedata.Render.UiElements
{
    public class Text : UiElement
    {
        public SpriteFont font;
        public OriginElement Origin { get; set; }
        public Color TextColor { get; set; }

        public Text(GraphicsDevice graphicsDevice, ContentManager contentManager, string content, SpriteFont font, RenderCanvas renderCanvas, Color color) : base(graphicsDevice, contentManager, renderCanvas)
        {
            this.Content = content;
            this.font = font;
            this.TextColor = color;
        }

        public override void Draw(SpriteBatch spriteBatch) //UI
        {
            DrawScripts(spriteBatch);

            Vector2 screenPosition = GetScreenPosition();

            Vector2 textSize = font.MeasureString(Content);

            switch (Origin)
            {
                case OriginElement.Center:
                    screenPosition -= textSize / 2;
                    break;
                case OriginElement.Top:
                    screenPosition.X -= textSize.X / 2;
                    break;
                case OriginElement.Bottom:
                    screenPosition.X -= textSize.X / 2;
                    screenPosition.Y -= textSize.Y;
                    break;
                case OriginElement.Left:
                    screenPosition.Y -= textSize.Y / 2;
                    break;
                case OriginElement.Right:
                    screenPosition.Y -= textSize.Y / 2;
                    screenPosition.X -= textSize.X;
                    break;
            }

            spriteBatch.DrawString(font, Content, screenPosition, TextColor);
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 position) // GameInstance
        {
            DrawScripts(spriteBatch);

            Vector2 textSize = font.MeasureString(Content);

            switch (Origin)
            {
                case OriginElement.Center:
                    position -= textSize / 2;
                    break;
                case OriginElement.Top:
                    position.X -= textSize.X / 2;
                    break;
                case OriginElement.Bottom:
                    position.X -= textSize.X / 2;
                    position.Y -= textSize.Y;
                    break;
                case OriginElement.Left:
                    position.Y -= textSize.Y / 2;
                    break;
                case OriginElement.Right:
                    position.Y -= textSize.Y / 2;
                    position.X -= textSize.X;
                    break;
            }

            spriteBatch.DrawString(font, Content, position, TextColor);
        }



        public override void Update(GameTime gameTime)
        {
            UpdateScripts(gameTime);
        }

    }



}
