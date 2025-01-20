using DESIRION_ENGINE._gamedata;
using DESIRION_ENGINE._gamedata.API;
using DESIRION_ENGINE._gamedata.Render.GameElements;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE.Content.Assets.Scripts
{
    public class CameraMovementScript : Script
    {
        public override void Draw(Camera camera, SpriteBatch spriteBatch)
        {

        }

        public override void Update(Camera camera)
        {
            if (InputManager.KeyDown(Microsoft.Xna.Framework.Input.Keys.W))
            {
                camera.Position.Y -= 2;
            }
            if (InputManager.KeyDown(Microsoft.Xna.Framework.Input.Keys.S))
            {
                camera.Position.Y += 2;
            }
            if (InputManager.KeyDown(Microsoft.Xna.Framework.Input.Keys.A))
            {
                camera.Position.X -= 2;
            }
            if (InputManager.KeyDown(Microsoft.Xna.Framework.Input.Keys.D))
            {
                camera.Position.X += 2;
            }

            if (InputManager.KeyDown(Microsoft.Xna.Framework.Input.Keys.Q))
            {
                camera.Rotation += 0.1f;
            }
            if (InputManager.KeyDown(Microsoft.Xna.Framework.Input.Keys.R))
            {
                camera.Rotation -= 0.1f;
            }

        }

    }
}
