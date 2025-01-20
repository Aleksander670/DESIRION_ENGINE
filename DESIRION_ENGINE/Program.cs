using DESIRION_ENGINE;
using DESIRION_ENGINE._gamedata;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.Xml;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

        using (var game = new Engine())
        {
            try
            {
                game.Run();
            }
            catch (Exception ex)
            {
                string tempFilePath = Path.GetTempFileName();
                File.WriteAllText(tempFilePath, $"{ex.Message}\n{ex.StackTrace}");

                var CrushReporter = new Process();
                CrushReporter.StartInfo.FileName = "CrashReporter.exe";
                CrushReporter.StartInfo.Arguments = tempFilePath;
                CrushReporter.Start();


                DebugLog.Log($"Исключение при запуске игры: {ex.Message}");
                DebugLog.SaveLog();
            }
        }
    }

    public static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is Exception exception)
        {
            string message = exception.Message;
            string stackTrace = exception.StackTrace;

            DebugLog.Log($"Необработанное исключение в домене приложения: {message}\n{stackTrace}");
            DebugLog.SaveLog();

            try
            {
                string tempFilePath = Path.GetTempFileName();
                File.WriteAllText(tempFilePath, $"{exception.Message}\n{exception.StackTrace}");

                var CrushReporter = new Process();
                CrushReporter.StartInfo.FileName = "CrashReporter.exe";
                CrushReporter.StartInfo.Arguments = tempFilePath;
                CrushReporter.Start();
            }
            catch (Exception ex)
            {
                DebugLog.Log($"Не удалось запустить CrashReporter: {ex.Message}");
                DebugLog.SaveLog();
            }
        }

        Environment.Exit(1);
    }


}
