using DESIRION_ENGINE._gamedata.Scene;
using DESIRION_ENGINE._gamedata.settings;
using DesirionEngine._gamedata.Render;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE._gamedata.Render
{
    public static class UIManager
    {
        private static List<UiCanvasData> UiData;
        private static UiCanvas currentUI;

        public static RenderCanvas RenderCanvas;

        static UIManager()
        {
            LoadUIData("Content/Assets/UI/UI.json");
        }

        public static void LoadUIData(string filePath)
        {
            if (File.Exists(filePath))
            {
                var jsonData = File.ReadAllText(filePath);
                UiData = JsonConvert.DeserializeObject<List<UiCanvasData>>(jsonData);
            }
            else
            {
                DebugLog.Log($"Файл не найден: {filePath}");
            }
        }

        public static UiCanvas LoadUI(string UiName, GraphicsDevice graphicsDevice, ContentManager contentManager, GameSettings gameSettings, RenderCanvas renderCanvas)
        {
            if (UiData == null || !UiData.Any())
            {
                DebugLog.Log("Данные сцен не загружены.");
            }

            var _UiData = UiData.FirstOrDefault(s => s.Name.Equals(UiName, StringComparison.OrdinalIgnoreCase));

            if (_UiData != null)
            {
                int UIId = _UiData.ID;

                string uiFilePath = $"Content/Assets/UI/UI{UIId}.json";

                RenderCanvas = renderCanvas;

                if (File.Exists(uiFilePath))
                {
                    var uiJsonData = File.ReadAllText(uiFilePath);
                    var uiDataFromJson = JsonConvert.DeserializeObject<UiCanvasData>(uiJsonData);

                    UiCanvas Ui = new UiCanvas(graphicsDevice, contentManager, renderCanvas)
                    {
                        ID = UIId,
                        Name = UiName
                    };
                    
                    Ui.InitializeElements(uiDataFromJson.UiElements);

                    currentUI = Ui;
                    return currentUI;
                }
                else
                {
                    DebugLog.Log($"Файл сцены не найден: {uiFilePath}");
                    throw new FileNotFoundException($"Файл сцены не найден: {uiFilePath}");
                }
            }
            else
            {
                DebugLog.Log($"Интерфейс с именем '{UiName}' не найден.");
                throw new Exception($"Интерфейс с именем '{UiName}' не найден.");
            }
        }

        public static UiCanvas GetCurrentUI()
        {
            return currentUI;
        }

        public static UiCanvas SetUI(string sceneName, GraphicsDevice graphicsDevice, ContentManager contentManager, GameSettings gameSettings)
        {
            currentUI?.Clear();

            currentUI = LoadUI(sceneName, graphicsDevice, contentManager, gameSettings, RenderCanvas);

            return currentUI;
        }

    }
}
