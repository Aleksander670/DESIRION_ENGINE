using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE._gamedata.Render.Light
{
    public class Filter
    {
        public int Id = 0;
        public string Name = "";

        public GraphicsDevice graphicsDevice;
        public ContentManager ContentManager;
        public Effect FilterEffect;
        public Camera GameCamera;

        private Vector3 currentColor;

        public Filter(ContentManager contentManager, GraphicsDevice GraphicsDevice, Camera camera, string filterShader)
        {
            this.ContentManager = contentManager;
            this.graphicsDevice = GraphicsDevice;
            this.GameCamera = camera;

            FilterEffect = contentManager.Load<Effect>($"Shaders/{filterShader}");

            currentColor = new Vector3(1f, 1f, 1f);
        }

        public void ApplySceneFilter(Vector3 Color, float Alpha)
        {
            currentColor = Color;

            FilterEffect.Parameters["WorldViewProjection"].SetValue(GameCamera.GetViewMatrix() * Matrix.CreateOrthographicOffCenter(0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height, 0, 0, 1));
            FilterEffect.Parameters["ColorFilter"].SetValue(new Vector4(Color, Alpha));
        }

        public Vector3 GetCurrentColor()
        {
            return currentColor;
        }
    }
}

