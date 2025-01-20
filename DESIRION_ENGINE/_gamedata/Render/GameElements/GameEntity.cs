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
    public class GameEntity : GameObject
    {
        public GameEntity(GraphicsDevice graphicsDevice, ContentManager contentManager, RenderCanvas renderCanvas, UiCanvas uiCanvas, string texturePath, int width, int height, OriginSprite origin) : base(graphicsDevice, contentManager, renderCanvas, uiCanvas, texturePath, width, height, origin)
        {
        }

    }
}
