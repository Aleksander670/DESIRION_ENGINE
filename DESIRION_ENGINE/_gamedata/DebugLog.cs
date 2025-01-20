using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE._gamedata
{
    public static class DebugLog
    {
        private static List<string> logContent = new List<string>();
        private static string logFilePath;

        private static float maxFPS = float.MinValue;
        private static float minFPS = float.MaxValue;
        private static float totalFPS = 0;
        private static int frameCount = 0;

        private static float minCpuUsage = float.MaxValue;
        private static float maxCpuUsage = float.MinValue;
        private static float totalCpuUsage = 0;
        private static int usageCount = 0;

        static DebugLog()
        {
            logFilePath = Path.Combine("Log", $"DebugLog_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
            Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));
        }

        public static void Log(string message)
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
            logContent.Add(logEntry);
            Console.WriteLine(logEntry);
        }

        public static void LogFPS(float fps)
        {
            if (frameCount == 0)
            {
                minFPS = fps;
            }

            if (fps > maxFPS) maxFPS = fps;
            if (fps < minFPS) minFPS = fps;
            totalFPS += fps;
            frameCount++;
        }

        public static float GetAverageFPS()
        {
            return frameCount > 0 ? totalFPS / frameCount : 0;
        }


        public static void SaveLog()
        {
            try
            {
                logContent.Add($"Max FPS: {maxFPS}");
                logContent.Add($"Min FPS: {minFPS}");
                logContent.Add($"Average FPS: {GetAverageFPS()}");

                logContent.Add($"CPU Usage: {GetCpuUsage()}%");
                logContent.Add($"RAM Usage: {GetMemoryUsage()} MB");
   

                File.WriteAllLines(logFilePath, logContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении лога: {ex.Message}");
            }
        }

        private static float GetCpuUsage()
        {
            using (var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
            {
                cpuCounter.NextValue();
                System.Threading.Thread.Sleep(1000);
                float cpuUsage = cpuCounter.NextValue();

                minCpuUsage = Math.Min(minCpuUsage, cpuUsage);
                maxCpuUsage = Math.Max(maxCpuUsage, cpuUsage);
                totalCpuUsage += cpuUsage;
                usageCount++;

                float averageCpuUsage = totalCpuUsage / usageCount;

                DebugLog.Log($"CPU Usage: {cpuUsage}%");
                DebugLog.Log($"Min CPU Usage: {minCpuUsage}%");
                DebugLog.Log($"Max CPU Usage: {maxCpuUsage}%");
                DebugLog.Log($"Average CPU Usage: {averageCpuUsage}%");

                return cpuUsage;
            }
        }

        private static float GetMemoryUsage()
        {
            var process = Process.GetCurrentProcess();
            return process.PrivateMemorySize64 / (1024 * 1024);
        }

        
    }

}
