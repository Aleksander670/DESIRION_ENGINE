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
    public class Background
    {
        private GraphicsDevice GraphicsDevice;
        private ContentManager ContentManager;

        public string TexturePath { get; private set; }
        public Texture2D Texture { get; private set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        
        public Background(GraphicsDevice graphicsDevice, ContentManager contentManager, string texturePath)
        {
            this.GraphicsDevice = graphicsDevice;
            this.ContentManager = contentManager;
            this.TexturePath = texturePath;

            
            Initialize(texturePath);
        }

        
        public void Initialize(string texturePath)
        {
            Texture = ContentManager.Load<Texture2D>(texturePath);
            
            Width = Texture.Width;
            Height = Texture.Height;
        }

        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle(0, 0, Width, Height), Color.White);
        }

    }

}
