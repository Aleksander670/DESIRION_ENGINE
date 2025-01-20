using DESIRION_ENGINE._gamedata;
using DESIRION_ENGINE._gamedata.FileSystem;
using DESIRION_ENGINE._gamedata.Render;
using DESIRION_ENGINE._gamedata.Render.GameElements;
using DESIRION_ENGINE._gamedata.Render.Test;
using DESIRION_ENGINE._gamedata.Scene;
using DESIRION_ENGINE._gamedata.settings;
using DesirionEngine._gamedata.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace DESIRION_ENGINE;

public class Engine : Game  
{
    private GameAppData appData;
    private GameSettings gameSettings;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public  RenderCanvas renderCanvas;
    private UiCanvas uiCanvas;
    private SceneClass GameScene;
    private Cursor Cursor;
    
    private Effect BrightnessShader;
    public float Brightness;

    public float FPS;

    public Engine()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        Window.AllowUserResizing = true;

    }

    protected override void Initialize()
    {
        InitializeAppData();
        InitializeSettingsData(appData);

        InitializeUserScreens();

        InitilaizeRenderCanvas();

        InitializeBrightness();

        InitializeGameUi();
        InitializeGameScene();
        

        
        base.Initialize();

        DebugLog.Log("Инициализация игры началась.");
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        InputManager.Update();

        if (InputManager.KeyPressed(Keys.P))
        {
            SaveGame.CreateSaveFile(GraphicsDevice,renderCanvas);
        }

        if (Keyboard.GetState().IsKeyDown(Keys.D1))
        {
            GameScene = SceneManager.SetScene("StartScene", GraphicsDevice, Content, gameSettings, renderCanvas, uiCanvas);
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D2))
        {
            GameScene = SceneManager.SetScene("SecondScene", GraphicsDevice, Content, gameSettings, renderCanvas, uiCanvas);
        }

        
        GameScene?.Update(gameTime);
        uiCanvas?.Update(gameTime);
        Cursor?.Update(gameTime);
        renderCanvas.SetDestinationRectangle(); // сделать обработку события

        FPS = 1f / (float)gameTime.ElapsedGameTime.TotalSeconds;
        DebugLog.LogFPS(FPS);


        base.Update(gameTime);
    }

    protected override void UnloadContent()
    {
        DebugLog.Log("Завершение игры. Сохранение лога.");
        DebugLog.SaveLog();
        
        base.UnloadContent();
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        renderCanvas.Activate();
        GameScene?.Draw(_spriteBatch);
        renderCanvas.Draw(_spriteBatch, BrightnessShader);

        _spriteBatch.Begin(samplerState: SamplerState.PointWrap, effect: BrightnessShader);
        uiCanvas?.Draw(_spriteBatch);
        _spriteBatch.End();

        _spriteBatch.Begin(samplerState: SamplerState.LinearClamp, effect: BrightnessShader);
        Cursor?.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void InitializeAppData()
    {
        appData = new GameAppData("DESIRION ENGINE", "DesirionStudio", "0.0.0.0", 800, 600, false, false, "settings.json", "DESIRION_ENGINE/Saves", "Save", "localhost", "", true);

        IsMouseVisible = appData.isMouseVisible;
        Window.Title = appData.AppTitle;
        Window.IsBorderless = appData.noneBorless;
        _graphics.PreferredBackBufferWidth = appData.ClientStandartWidth;
        _graphics.PreferredBackBufferHeight = appData.ClientStandartHeight;

        SaveGame.SaveGameDataInitialization(appData.SaveFilePath, appData.SaveFileName);

    }

    private void InitializeUserScreens()
    {
        var screensList = System.Windows.Forms.Screen.AllScreens;

        var screen = screensList[0];

        if (screensList.Length > 1)
        {
            screen = screensList[gameSettings.MonitorNumber];
        }

        var centerX = screen.Bounds.X + (screen.Bounds.Width - _graphics.PreferredBackBufferWidth) / 2;
        var centerY = screen.Bounds.Y + (screen.Bounds.Height - _graphics.PreferredBackBufferHeight) / 2;

        Window.Position = new Point(centerX, centerY);
    }

    public void InitializeBrightness()
    {
        BrightnessShader = Content.Load<Effect>("Shaders/Brightness");
       

        BrightnessShader.Parameters["WorldViewProjection"].SetValue(Matrix.CreateOrthographicOffCenter(0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0, 0, 1));
        BrightnessShader.Parameters["Brightness"].SetValue(Brightness);
    }

    private void InitializeSettingsData(GameAppData gameApp)
    {
        gameSettings = new GameSettings(gameApp.SettingsFilePath);
        _graphics.PreferredBackBufferWidth = gameSettings.WindowSizeWidth;
        _graphics.PreferredBackBufferHeight = gameSettings.WindowSizeHeight;
        _graphics.SynchronizeWithVerticalRetrace = gameSettings.VerticalSync;
        _graphics.IsFullScreen = gameSettings.isFullScreen;
        
        TargetElapsedTime = TimeSpan.FromSeconds(1.0f / gameSettings.FPSlimit);

        MusicManager.SetMaxMusicVolume(gameSettings.MaximumMusicVolume);
        SoundManager.SetMaxSoundVolume(gameSettings.MaximumSoundVolume);
        
        Brightness = gameSettings.Brightness;

        _graphics.ApplyChanges();
    }

    private void InitilaizeRenderCanvas()
    {
        float aspectRatio = (float)gameSettings.WindowSizeWidth / gameSettings.WindowSizeHeight;
        int desiredHeight = gameSettings.desiredHightForCamera;
        int desiredWidth = (int)(desiredHeight * aspectRatio);
        renderCanvas = new RenderCanvas(GraphicsDevice, desiredWidth, desiredHeight);
    }

    private void InitializeGameScene()
    {
        GameScene = SceneManager.LoadScene("StartScene", GraphicsDevice, Content, gameSettings, renderCanvas, uiCanvas);
        GameScene.Initialize();
        Cursor = new Cursor(GraphicsDevice, Content, renderCanvas, "");
    }

    private void InitializeGameUi()
    {
        uiCanvas = UIManager.LoadUI("MainMenu", GraphicsDevice, Content, gameSettings, renderCanvas);
    }
}
