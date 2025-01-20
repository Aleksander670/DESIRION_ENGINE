using DESIRION_ENGINE._gamedata.Scene;
using DesirionEngine._gamedata.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE._gamedata.FileSystem
{
    public static class SaveGame
    {
        public static string saveFilePath { get; set; }
        public static string saveFileName { get; set; }

        private static string appDataPath;

        static SaveGame() { }
        
        public static void SaveGameDataInitialization(string SaveFilePath, string SaveFileName)
        {
            saveFilePath = SaveFilePath;
            saveFileName = SaveFileName;

            appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        public static void CreateSaveFile(GraphicsDevice graphicsDevice, RenderCanvas renderCanvas)
        {
            try
            {
                string folderSaveGame = Path.Combine(appDataPath, saveFilePath);
                string imagesFolder = Path.Combine(folderSaveGame, "images");

                if (!Directory.Exists(folderSaveGame))
                {
                    Directory.CreateDirectory(folderSaveGame);
                }

                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }

                var existingFiles = Directory.GetFiles(folderSaveGame, $"{saveFileName}*");

                int nextFileNumber = 1;
                while (true)
                {
                    string formattedNumber = nextFileNumber.ToString("D3");
                    string newFileName = $"{saveFileName}{formattedNumber}.save";
                    string fullPath = Path.Combine(folderSaveGame, newFileName);

                    if (!File.Exists(fullPath))
                    {
                        File.WriteAllText(fullPath, "Save file!");

                        SaveScreenshot(graphicsDevice, renderCanvas, Path.Combine(imagesFolder, $"screenshot{formattedNumber}.png"));
                        break;
                    }

                    nextFileNumber++;
                }
            }
            catch (Exception ex)
            {
                DebugLog.Log($"Error when creating a save file: {ex}");
            }
        }


        private static void SaveScreenshot(GraphicsDevice graphicsDevice, RenderCanvas renderCanvas, string filePath)
        {
            RenderTarget2D screenTarget = renderCanvas.renderTarget2D;

            Color[] pixels = new Color[screenTarget.Width * screenTarget.Height];
            screenTarget.GetData(pixels);

            int minX = screenTarget.Width;
            int minY = screenTarget.Height;
            int maxX = 0;
            int maxY = 0;

            for (int y = 0; y < screenTarget.Height; y++)
            {
                for (int x = 0; x < screenTarget.Width; x++)
                {
                    Color pixel = pixels[y * screenTarget.Width + x];
                    if (pixel.A > 0)
                    {
                        if (x < minX) minX = x;
                        if (x > maxX) maxX = x;
                        if (y < minY) minY = y;
                        if (y > maxY) maxY = y;
                    }
                }
            }

            if (maxX < minX || maxY < minY)
            {
                return;
            }

            int newWidth = maxX - minX + 1;
            int newHeight = maxY - minY + 1;

            Texture2D croppedTexture = new Texture2D(graphicsDevice, newWidth, newHeight);

            Color[] croppedPixels = new Color[newWidth * newHeight];
            for (int y = 0; y < newHeight; y++)
            {
                for (int x = 0; x < newWidth; x++)
                {
                    croppedPixels[y * newWidth + x] = pixels[(minY + y) * screenTarget.Width + (minX + x)];
                }
            }

            croppedTexture.SetData(croppedPixels);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                croppedTexture.SaveAsPng(stream, croppedTexture.Width, croppedTexture.Height);
            }

            croppedTexture.Dispose();
        }


        public static void LoadSaveFile()
        {

        }


    }
}
