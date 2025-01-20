using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE._gamedata.Render.Test
{
    public class RenderBackgroundObj
    {
        private GraphicsDevice GraphicsDevice { get; set; }
        private ContentManager ContentManager { get; set; }

        private Texture2D Texture2D;

        public RenderBackgroundObj(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            this.GraphicsDevice = graphicsDevice;
            this.ContentManager = contentManager;

            Initilize();
        }

        public void Initilize()
        {
            Texture2D = ContentManager.Load<Texture2D>("Sprites/spizdil");
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(Texture2D, new Vector2(0, 0), Color.White);
        }


    }
}
