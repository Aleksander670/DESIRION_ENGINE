using DESIRION_ENGINE._gamedata.API;
using DesirionEngine._gamedata.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE._gamedata.Render.UiElements
{
    public enum TypeUI
    {
        Text,
        Image,
        CheckBox,
        RadioButton
    }

    public enum OriginElement
    {
        Center,
        Top,
        Bottom,
        Left,
        Right
    }

    public abstract class UiElement
    {
        protected GraphicsDevice GraphicsDevice;
        protected ContentManager ContentManager;

        public string Content;

        public RenderCanvas RenderCanvas;
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; } = Vector2.One;
        public List<Script> Script = new List<Script>();

        protected UiElement(GraphicsDevice graphicsDevice, ContentManager contentManager, RenderCanvas renderCanvas)
        {
            GraphicsDevice = graphicsDevice;
            ContentManager = contentManager;
            this.RenderCanvas = renderCanvas;
            Position = Vector2.Zero;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Draw(SpriteBatch spriteBatch, Vector2 position);

        public abstract void Update(GameTime gameTime);

        public void AddUIScript(Script script)
        {
            Script.Add(script);
        }

        public void UpdateScripts(GameTime gameTime)
        {
            foreach (var script in Script)
            {
                script.Update(this);
            }
        }

        public void DrawScripts(SpriteBatch spriteBatch)
        {
            foreach (var script in Script)
            {
                script.Draw(this, spriteBatch);
            }
        }

        
        public Vector2 GetScreenPosition()
        {
            float scale = RenderCanvas.GetScaleFactor();
            return Position * scale;
        }

        public Vector2 GetScaledSize()
        {
            float scale = RenderCanvas.GetScaleFactor();
            return Size * scale;
        }

        



    }


}
