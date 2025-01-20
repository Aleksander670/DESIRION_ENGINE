using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE._gamedata.API
{
    public static class ScriptLoader
    {
        public static Script LoadScript(string scriptName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string fullClassName = $"DESIRION_ENGINE.Content.Assets.Scripts.{scriptName}";

            Type scriptType = assembly.GetType(fullClassName);
            if (scriptType != null && typeof(Script).IsAssignableFrom(scriptType))
            {
                return (Script)Activator.CreateInstance(scriptType);
            }

            return null;
        }
    }
}
