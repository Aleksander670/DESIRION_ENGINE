using DESIRION_ENGINE._gamedata.API;
using DESIRION_ENGINE._gamedata.Render.Light;
using DESIRION_ENGINE._gamedata.Render.UiElements;
using DESIRION_ENGINE._gamedata.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE.Content.Assets.Scripts
{
    public class TestSceneScript : Script
    {
        private SceneClass thisScene;

        private float r = 1f;
        private float g = 1f;
        private float b = 1f;

        private float transitionSpeed = 0.001f; // Скорость перехода
        private int currentState = 0; // Текущий цветовой статус

        private int timercounter = 0;

        private Text timer;

        public override void Initialize(SceneClass scene)
        {
            thisScene = scene;
            thisScene.GetFilterByName("DefaultFilter").ApplySceneFilter(new Vector3(1f, 1f, 1f), 1); // Начинаем с черного
            
            timer = new Text(scene.graphicdevice, scene.contentManager, "", scene.contentManager.Load<SpriteFont>("Fonts/BaseFont"), scene.RenderCanvas, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            
            // Плавный переход между состояниями
            switch (currentState)
            {
                case 0: // Черный
                    r = Math.Min(r + transitionSpeed, 0f);
                    g = Math.Min(g + transitionSpeed, 0f);
                    b = Math.Min(b + transitionSpeed, 0f);
                    if (r == 0f && g == 0f && b == 0f)
                        currentState++;
                    break;

                case 1: // Ярко-белый
                    r = Math.Min(r + transitionSpeed, 1f);
                    g = Math.Min(g + transitionSpeed, 1f);
                    b = Math.Min(b + transitionSpeed, 1f);
                    if (r >= 1f && g >= 1f && b >= 1f)
                        currentState++;
                    break;

                case 2: // Простой белый
                    r = Math.Max(r - transitionSpeed, 1f);
                    g = Math.Max(g - transitionSpeed, 1f);
                    b = Math.Max(b - transitionSpeed, 1f);
                    if (r <= 1f && g <= 1f && b <= 1f)
                        currentState++;
                    break;

                case 3: // Желтоватый
                    r = Math.Min(r + transitionSpeed * 0.5f, 1f); // Увеличиваем красный
                    g = Math.Min(g + transitionSpeed * 0.5f, 1f); // Увеличиваем зеленый
                    b = Math.Max(b - transitionSpeed * 0.5f, 0.5f); // Уменьшаем синий
                    if (r >= 1f && g >= 1f && b <= 0.5f)
                        currentState++;
                    break;

                case 4: // Оранжевый
                    r = Math.Min(r + transitionSpeed * 0.5f, 1f); // Увеличиваем красный
                    g = Math.Max(g - transitionSpeed * 0.5f, 0f); // Уменьшаем зеленый
                    b = Math.Max(b - transitionSpeed * 0.5f, 0f); // Уменьшаем синий
                    if (r >= 1f && g <= 0f && b <= 0f)
                        currentState++;
                    break;

                case 5: // Возврат к черному
                    r = Math.Max(r - transitionSpeed, 0f);
                    g = Math.Max(g - transitionSpeed, 0f);
                    b = Math.Max(b - transitionSpeed, 0f);
                    if (r <= 0f && g <= 0f && b <= 0f)
                        currentState = 0; // Сброс к началу
                    break;
            }
            timercounter += gameTime.TotalGameTime.Minutes;
            if (timercounter > 12)
            {
                timercounter = 0;
            }
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            timer.Draw(spriteBatch, new Vector2(10,10));

            timer.Content = timercounter.ToString();

            thisScene.GetFilterByName("DefaultFilter").ApplySceneFilter(new Vector3(r, g, b), 1);
        }
           
    }



}
