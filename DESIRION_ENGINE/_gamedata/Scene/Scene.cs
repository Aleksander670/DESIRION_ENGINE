using DESIRION_ENGINE._gamedata.API;
using DESIRION_ENGINE._gamedata.Render;
using DESIRION_ENGINE._gamedata.Render.GameElements;
using DESIRION_ENGINE._gamedata.Render.Light;
using DESIRION_ENGINE._gamedata.Render.Test;
using DESIRION_ENGINE._gamedata.settings;
using DESIRION_ENGINE.Content.Assets.Scripts;
using DesirionEngine._gamedata.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;


namespace DESIRION_ENGINE._gamedata.Scene
{
    public class SceneClass
    {
        public int ID;
        public string Name;
        public int Width;
        public int Height;

        public GraphicsDevice graphicdevice;
        public ContentManager contentManager;
        private GameSettings gameSettings;
        public RenderCanvas RenderCanvas;
        public UiCanvas UiCanvas;

        public Camera Camera;
        public Background Background;

        public List<GameObject> gameObjects = new List<GameObject>();
        public List<Script> SceneScripts = new List<Script>();
        public Dictionary<int, Filter> Filters = new Dictionary<int, Filter>();

        private Effect unifiedShader; // Шейдер для фильтрации

        private RadialLight light;
        private RadialLight light2;

        public SceneClass(int width, int height, GraphicsDevice graphicsDevice, ContentManager contentManager, GameSettings gameSettings, RenderCanvas renderCanvas, UiCanvas canvas)
        {
            this.graphicdevice = graphicsDevice;
            this.contentManager = contentManager;
            this.UiCanvas = canvas;
            this.RenderCanvas = renderCanvas;
            this.gameSettings = gameSettings;
            this.Width = width;
            this.Height = height;
        }

        public void Initialize()
        {
            Background = new Background(graphicdevice, contentManager, "Sprites/Background_test3");
            Camera = new Camera(gameSettings.WindowSizeWidth, gameSettings.desiredHightForCamera);
            Camera.Position = new Vector2(0, 0);

            MusicManager.MusicPlay(contentManager, "ArdSkellige");

            InitializeFilters();
            InitializeLight(); // Инициализация света
            InitializeSceneScript();
            InitializeUnifiedShader();
            LoadObjects($"Scene{ID}");
        }

        public void InitializeFilters()
        {
            //default filter
            AddFilter(0, "DefaultFilter", "FilterShader");
        }

        public void InitializeLight()
        {
            light = new RadialLight(contentManager, graphicdevice, RenderCanvas);
            light._radius = 100f;
            light._color = Color.White;
            light._intensity = 0.4f;

            light2 = new RadialLight(contentManager, graphicdevice, RenderCanvas);
            light2._radius = 120f;
            light2._color = Color.White;
            light2._intensity = 0.6f;
        }

        public void InitializeUnifiedShader()
        {
            unifiedShader = contentManager.Load<Effect>("Shaders/FilterShader");
            unifiedShader.Parameters["WorldViewProjection"].SetValue(Matrix.CreateOrthographicOffCenter(0, graphicdevice.Viewport.Width, graphicdevice.Viewport.Height, 0, 0, 1));
            unifiedShader.Parameters["screenSize"].SetValue(new Vector2(graphicdevice.Viewport.Width, graphicdevice.Viewport.Height));
            graphicdevice.BlendState = BlendState.Additive;
        }

        public void InitializeSceneScript()
        {
            if (SceneScripts.Count > 0)
            {
                foreach (Script script in SceneScripts)
                {
                    script.Initialize(this);
                }
            }
        }

        public void LoadObjects(string sceneName)
        {
            Clear();

            string jsonPath = $"Content/Assets/Scenes/{sceneName}.json";

            if (File.Exists(jsonPath))
            {
                string json = File.ReadAllText(jsonPath);
                var sceneObjectsData = JsonConvert.DeserializeObject<SceneData>(json);

                if (sceneObjectsData.Background != null)
                {
                    Background = new Background(graphicdevice, contentManager, sceneObjectsData.Background.TexturePath);
                }

                if (sceneObjectsData.CameraScript != null && sceneObjectsData.CameraScript.Count > 0)
                {
                    foreach (var scriptName in sceneObjectsData.CameraScript)
                    {
                        Script cameraScript = ScriptLoader.LoadScript(scriptName);
                        if (cameraScript != null)
                        {
                            Camera.AddScript(cameraScript);
                        }
                    }
                }

                foreach (var objData in sceneObjectsData.Objects)
                {
                    GameObject gameObject = null;

                    switch (objData.Type)
                    {
                        case "Player":
                            gameObject = new GameEntity(graphicdevice, contentManager, RenderCanvas, UiCanvas, objData.TexturePath, objData.Width, objData.Height, OriginSprite.Center);
                            break;

                            // Добавьте другие типы объектов по мере необходимости 
                    }

                    if (gameObject != null)
                    {
                        gameObject.Type = objData.Type;
                        gameObject.RenderCanvas = RenderCanvas;
                        gameObject.Position = new Vector2(objData.PositionX, objData.PositionY);

                        if (objData.Script != null && objData.Script.Count > 0)
                        {
                            foreach (var scriptName in objData.Script)
                            {
                                Script script = ScriptLoader.LoadScript(scriptName);
                                if (script != null && gameObject.Type != "Player")
                                {
                                    gameObject.AddScript(script);
                                }
                                else
                                {
                                    gameObject.AddPlayerScript(Camera, script);
                                }
                            }
                        }

                        gameObjects.Add(gameObject);
                    }
                }
            }
            else
            {
                DebugLog.Log($"Файл '{jsonPath}' для загрузки сцен - не найден.");
                throw new Exception($"Файл '{jsonPath}' не найден.");
            }
        }

        public void Update(GameTime gameTime)
        {
            Camera.Update();

            foreach (Script script in SceneScripts)
            {
                script?.Update(gameTime);
            }

            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.IsActive)
                {
                    gameObject.Update();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Начинаем отрисовку с использованием камеры и основного шейдера
            spriteBatch.Begin(transformMatrix: Camera.GetViewMatrix(), effect: unifiedShader);

            // Отрисовываем фон
            Background.Draw(spriteBatch);

            // Обновляем параметры общего шейдера
            UpdateUnifiedShaderParameters();

            // Отрисовываем скрипты
            foreach (Script script in SceneScripts)
            {
                script?.Draw(spriteBatch);
            }

            // Отрисовываем объекты
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject?.Draw(spriteBatch);
            }

            spriteBatch.End();

            // Отрисовка света
            DrawLights(spriteBatch);
        }


        public void DrawLights(SpriteBatch spriteBatch)
        {
            // Начинаем новую отрисовку для света
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, light._lightShader, Camera.GetViewMatrix());

            // Устанавливаем параметры шейдера света
            light.SetSceneTexture(RenderCanvas.renderTarget2D);
            light.SetPosition(Mouse.GetState().Position.ToVector2()); // Устанавливаем позицию света
            light.Draw(spriteBatch); // Отрисовываем свет

            // Устанавливаем параметры шейдера света
            light2.SetSceneTexture(RenderCanvas.renderTarget2D);
            light2.SetPosition(new Vector2(200,160)); // Устанавливаем позицию света
            light2.Draw(spriteBatch); // Отрисовываем свет

            spriteBatch.End();
        }



        private void UpdateUnifiedShaderParameters()
        {
            if (unifiedShader != null)
            {
                unifiedShader.Parameters["WorldViewProjection"].SetValue(Camera.GetViewMatrix() * Matrix.CreateOrthographicOffCenter(0, graphicdevice.Viewport.Width, graphicdevice.Viewport.Height, 0, 0, 1));
                unifiedShader.Parameters["ColorFilter"].SetValue(Filters[0].GetCurrentColor()); // Получаем текущий цвет из фильтра
            }

            
        }

        public void ApplyFilters(Vector3 Color, float alpha)
        {
            for (int i = 0; i < Filters.Count; i++)
            {
                Filters[i].ApplySceneFilter(Color, alpha);
            }
        }

        public void AddFilter(int id, string name, string filterShader)
        {
            var filter = new Filter(contentManager, graphicdevice, Camera, filterShader);
            filter.Id = id;
            filter.Name = name;
            Filters.Add(id, filter);
        }

        public Filter GetFilterById(int id)
        {
            Filters.TryGetValue(id, out Filter filter);
            return filter;
        }

        public Filter GetFilterByName(string name)
        {
            return Filters.Values.FirstOrDefault(f => f.Name == name);
        }

        public void Clear()
        {
            gameObjects.Clear();
        }
    }
}
