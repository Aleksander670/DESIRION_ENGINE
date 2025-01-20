using DESIRION_ENGINE._gamedata.API;
using DESIRION_ENGINE._gamedata.Render;
using DESIRION_ENGINE._gamedata.settings;
using DesirionEngine._gamedata.Render;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE._gamedata.Scene
{
    public static class SceneManager
    {
        private static List<SceneData> scenesData;
        private static SceneClass currentScene;

        static SceneManager()
        {
            LoadScenesData("Content/Assets/Scenes/Scenes.json");
        }

        public static void LoadScenesData(string filePath)
        {
            if (File.Exists(filePath))
            {
                var jsonData = File.ReadAllText(filePath);
                scenesData = JsonConvert.DeserializeObject<List<SceneData>>(jsonData);
            }
            else
            {
                DebugLog.Log($"Файл не найден: {filePath}");
            }
        }

        public static SceneClass LoadScene(string sceneName, GraphicsDevice graphicsDevice, ContentManager contentManager, GameSettings gameSettings, RenderCanvas renderCanvas, UiCanvas uiCanvas)
        {
            
            if (scenesData == null || !scenesData.Any())
            {
                DebugLog.Log("Данные сцен не загружены.");
            }

            var sceneData = scenesData.FirstOrDefault(s => s.Name.Equals(sceneName, StringComparison.OrdinalIgnoreCase));

            if (sceneData != null)
            {
                int sceneId = sceneData.ID;
                int sceneWidth = sceneData.Width;
                int sceneHeight = sceneData.Height;
                

                string sceneFilePath = $"Content/Assets/Scenes/Scene{sceneId}.json";

                if (File.Exists(sceneFilePath))
                {
                    var sceneJsonData = File.ReadAllText(sceneFilePath);

                    var sceneDataFromJson = JsonConvert.DeserializeObject<SceneData>(sceneJsonData);

                    SceneClass scene = new SceneClass(sceneDataFromJson.Width, sceneDataFromJson.Height, graphicsDevice, contentManager, gameSettings, renderCanvas, uiCanvas)
                    {
                        ID = sceneId,
                        Name = sceneName,
                        Width = sceneWidth,
                        Height = sceneHeight
                    };

                    currentScene = scene;

                    if (sceneData.Scripts.Count > 0) 
                    {
                        foreach (string script in sceneData.Scripts)
                        {
                            currentScene.SceneScripts.Add(ScriptLoader.LoadScript(script));
                        }
                    }

                    return currentScene;
                }
                else
                {
                    DebugLog.Log($"Файл сцены не найден: {sceneFilePath}");
                    throw new FileNotFoundException($"Файл сцены не найден: {sceneFilePath}");
                }
            }
            else
            {
                DebugLog.Log($"Сцена с именем '{sceneName}' не найдена.");
                throw new Exception($"Сцена с именем '{sceneName}' не найдена.");
            }
        }

        public static SceneClass GetCurrentScene()
        {
            return currentScene;
        }

        public static SceneClass SetScene(string sceneName, GraphicsDevice graphicsDevice, ContentManager contentManager, GameSettings gameSettings, RenderCanvas renderCanvas, UiCanvas uiCanvas)
        {
            currentScene?.Clear();
            currentScene?.SceneScripts.Clear();

            currentScene = LoadScene(sceneName, graphicsDevice, contentManager, gameSettings, renderCanvas, uiCanvas);
            currentScene?.Initialize();

            return currentScene;
        }

    }


}
