using DESIRION_ENGINE._gamedata.API;
using DESIRION_ENGINE._gamedata.Render.GameElements;
using DESIRION_ENGINE._gamedata.Render.UiElements;
using DesirionEngine._gamedata.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE._gamedata.Render
{
    public class UiCanvas
    {
        public int ID;
        public string Name;

        public Camera Camera;
        private GraphicsDevice GraphicsDevice;
        private ContentManager ContentManager;
        private RenderCanvas RenderCanvas;

        public List<UiElement> UiElements = new List<UiElement>();

        public UiCanvas(GraphicsDevice graphicsDevice, ContentManager contentManager, RenderCanvas renderCanvas)
        {
            this.GraphicsDevice = graphicsDevice;
            this.ContentManager = contentManager;
            this.RenderCanvas = renderCanvas;
        }

        public void InitializeElements(List<UiElementData> uiElementDataList)
        {
            foreach (var elementData in uiElementDataList)
            {
                UiElement uiElement = CreateUiElement(elementData);
                if (uiElement != null)
                {
                    UiElements.Add(uiElement);
                }
            }
        }

        private UiElement CreateUiElement(UiElementData data)
        {

            Color color = Color.White;

            if (!string.IsNullOrEmpty(data.ColorHex))
            {
                color = ColorHelper.FromHex(data.ColorHex);
            }

            UiElement uiElement = null;

            switch (data.Type)
            {
                case TypeUI.Text:
                    SpriteFont font = ContentManager.Load<SpriteFont>(data.Font);
                    uiElement = new Text(GraphicsDevice, ContentManager, data.Content, font, RenderCanvas, color)
                    {
                        Position = new Vector2(data.PositionX, data.PositionY),
                        Origin = data.Origin
                    };
                    break;

                case TypeUI.Image:
                    uiElement = new Image(GraphicsDevice, ContentManager, data.Source, RenderCanvas, new Vector2(data.SizeX, data.SizeY), color)
                    {
                        Position = new Vector2(data.PositionX, data.PositionY),
                        Origin = data.Origin
                    };
                    break;

                case TypeUI.CheckBox:
                /*
                uiElement = new CheckBox(GraphicsDevice, ContentManager, data.ActiveTexture, data.InactiveTexture, data.Content, RenderCanvas, new Vector2(data.SizeX, data.SizeY))
                {
                    Position = new Vector2(data.PositionX, data.PositionY),
                    Origin = data.Origin
                };
                break;
                */
                case TypeUI.RadioButton:
                /*
                uiElement = new RadioButton(GraphicsDevice, ContentManager, data.ActiveTexture, data.InactiveTexture, RenderCanvas, new Vector2(data.SizeX, data.SizeY))
                {
                    Position = new Vector2(data.PositionX, data.PositionY),
                    //Origin = data.Origin
                };
                break;
                */
                default:
                    return null;
            }

            if (data.Script != null && data.Script.Count > 0)
            {
                foreach (var scriptName in data.Script)
                {
                    Script script = ScriptLoader.LoadScript(scriptName);
                    if (script != null)
                    {
                        uiElement.AddUIScript(script);
                    }
                }
            }

            return uiElement;
        }

        public void Update(GameTime gameTime)
        {
            foreach (UiElement uiElement in UiElements)
            {
                uiElement.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (UiElement uiElement in UiElements)
            {
                uiElement.Draw(spriteBatch);
            }
        }

        public Vector2 GetWorldPosition(Camera camera, Vector2 worldPosition)
        {
            Vector2 cameraPosition = camera.Position;
            Vector2 uiPosition = worldPosition - cameraPosition;

            //Vector2 scale = GetScaledSize();
            return uiPosition; //* scale;
        }


        public void Clear()
        {
            UiElements.Clear();
        }
    }


}
