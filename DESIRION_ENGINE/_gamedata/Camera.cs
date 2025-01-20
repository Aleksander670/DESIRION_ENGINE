using DESIRION_ENGINE._gamedata.API;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE._gamedata
{
    public class Camera
    {
        public Vector2 Position;
        public float Zoom { get; set; } = 1f;
        public float Rotation { get; set; } = 0f;
        public int _viewportWidth;
        public int _viewportHeight;

        public List<Script> Scripts = new List<Script>();

        public Camera(int viewportWidth, int viewportHeight)
        {
            _viewportWidth = viewportWidth;
            _viewportHeight = viewportHeight;
            Position = Vector2.Zero;
        }

        public Matrix GetViewMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(-Position, 0)) *
                   Matrix.CreateRotationZ(Rotation) *
                   Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                   Matrix.CreateTranslation(new Vector3(0, 0, 0));

        }


        public void Update()
        {
            foreach (var script in Scripts)
            {
                script.Update(this);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var script in Scripts)
            {
                script.Draw(this, spriteBatch);
            }
        }

        public void AddScript(Script script)
        {
            Scripts.Add(script);
        }

    }
}
