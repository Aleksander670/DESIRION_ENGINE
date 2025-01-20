using DESIRION_ENGINE._gamedata.Render.UiElements;
using DesirionEngine._gamedata.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE._gamedata.Render
{
    public class Cursor
    {
        public GraphicsDevice GraphicsDevice;
        public ContentManager ContentManager;
        public RenderCanvas RenderCanvas;

        public Vector2 Position;
        public string SpritePath;

        private Texture2D CursorSprite;

        public bool DebugMode = true;//дописать отображение координат
        public bool IsVisible = true;
        
        public Cursor(GraphicsDevice graphicsDevice, ContentManager contentManager, RenderCanvas renderCanvas, string spritePath)
        {
            this.GraphicsDevice = graphicsDevice;
            this.ContentManager = contentManager;
            this.RenderCanvas = renderCanvas;

            LoadSprite(spritePath);
        }

        public void LoadSprite(string spritePath)
        {
            try
            {
                CursorSprite = ContentManager.Load<Texture2D>(spritePath);
            }
            catch (Exception ex)
            {
                CursorSprite = ContentManager.Load<Texture2D>("Sprites/System/SystemCursor");
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CursorSprite, Position, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            Position = new Vector2(mouseState.X - CursorSprite.Width/2, mouseState.Y - CursorSprite.Height/2);

        }


    }
}
