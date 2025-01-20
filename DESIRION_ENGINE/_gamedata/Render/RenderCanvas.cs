using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesirionEngine._gamedata.Render
{
    public class RenderCanvas
    {
        public RenderTarget2D renderTarget2D;
        private GraphicsDevice graphicsDevice;
        private Rectangle destinationRectangle;

        public int Width;
        public int Height;

        public RenderCanvas(GraphicsDevice graphicsDevice, int width, int height)
        {
            this.graphicsDevice = graphicsDevice;
            renderTarget2D = new RenderTarget2D(graphicsDevice, width, height);
            destinationRectangle = new Rectangle(0, 0, width, height);
        }

        public void SetDestinationRectangle()
        {
            var screenSize = graphicsDevice.PresentationParameters.Bounds;
            float scale = GetScaleFactor();
            int newWidth = (int)(renderTarget2D.Width * scale);
            int newHeight = (int)(renderTarget2D.Height * scale);
            int positionX = (screenSize.Width - newWidth) / 2;
            int positionY = (screenSize.Height - newHeight) / 2;
            destinationRectangle = new Rectangle(positionX, positionY, newWidth, newHeight);
        }

        public Vector2 ConvertToScreenPosition(Vector2 uiPosition)
        {
            var screenSize = graphicsDevice.PresentationParameters.Bounds;
            float scaleX = (float)screenSize.Width / renderTarget2D.Width;
            float scaleY = (float)screenSize.Height / renderTarget2D.Height;
            float scale = Math.Min(scaleX, scaleY);

            Vector2 scaledPosition = uiPosition * scale;

            int positionX = (screenSize.Width - (int)(renderTarget2D.Width * scale)) / 2;
            int positionY = (screenSize.Height - (int)(renderTarget2D.Height * scale)) / 2;

            return new Vector2(scaledPosition.X + positionX, scaledPosition.Y + positionY);
        }

        public float GetScaleFactor()
        {
            var screenSize = graphicsDevice.PresentationParameters.Bounds;
            float scaleX = (float)screenSize.Width / renderTarget2D.Width;
            float scaleY = (float)screenSize.Height / renderTarget2D.Height;
            return Math.Min(scaleX, scaleY);
        }

        public void Activate()
        {
            graphicsDevice.SetRenderTarget(renderTarget2D);
            graphicsDevice.Clear(Color.Transparent);
        }

        public void Draw(SpriteBatch spriteBatch, Effect Effect)
        {
            graphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: Effect);
            spriteBatch.Draw(renderTarget2D, destinationRectangle, Color.White);
            spriteBatch.End();
        }


    }

}
