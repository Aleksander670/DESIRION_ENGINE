using DESIRION_ENGINE._gamedata.Render.GameElements;
using DESIRION_ENGINE._gamedata.Render.UiElements;
using DESIRION_ENGINE._gamedata.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE._gamedata.API
{
    public abstract class Script
    {
        public virtual void Initialize(SceneClass scene) { }
        public virtual void Initialize(GameObject gameObject) { }
        public virtual void Initialize(Camera camera, GameObject gameObject) { }

        public virtual void Update(GameTime gameTime) { }
        public virtual void Update(GameObject gameObject) { }
        public virtual void Update(Camera camera) { }
        public virtual void Update(UiElement uiElement) { }

        public virtual void Draw(SpriteBatch spriteBatch) { }
        public virtual void Draw(GameObject gameObject, SpriteBatch spriteBatch) { }
        public virtual void Draw(Camera camera, SpriteBatch spriteBatch) { }
        public virtual void Draw(UiElement uiElement, SpriteBatch spriteBatch) { }

    }

}
