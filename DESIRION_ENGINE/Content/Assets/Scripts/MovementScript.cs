using DESIRION_ENGINE._gamedata;
using DESIRION_ENGINE._gamedata.API;
using DESIRION_ENGINE._gamedata.Render.GameElements;
using DESIRION_ENGINE._gamedata.Render.UiElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE.Content.Assets.Scripts
{
    public class MovementScript : Script
    {
        private Text Text;
        private Camera Camera;

        public override void Initialize(Camera camera, GameObject gameObject)
        {
            Camera = camera;
            gameObject.Speed = new Random().Next(0);
            
            Text = new Text(gameObject.GraphicsDevice, gameObject.ContentManager, $"Type: {gameObject.Speed}", gameObject.ContentManager.Load<SpriteFont>("Fonts/BaseFont"), gameObject.RenderCanvas, Color.White);
            Text.Origin = OriginElement.Left;


            gameObject.UiCanvas.UiElements.Add(Text);
        }

        public override void Draw(GameObject gameObject, SpriteBatch spriteBatch)
        {
            
            Text.Position = gameObject.UiCanvas.GetWorldPosition(Camera, new Vector2(gameObject.Position.X - 40, gameObject.Position.Y - 40));
        }

        public override void Update(GameObject gameObject)
        {
            gameObject.Position += new Vector2(gameObject.Speed, 0);
            gameObject.Rotation += 0.1f;
        }
    }

}
