using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE._gamedata
{
    public static class InputManager
    {
        private static KeyboardState lastKeyboard;
        private static KeyboardState currentKeyboard;

        public static bool KeyPressed(Keys key)
        {
            return currentKeyboard.IsKeyDown(key) && lastKeyboard.IsKeyUp(key);
        }

        public static bool KeyDown(Keys key)
        {
            return currentKeyboard.IsKeyDown(key);
        }

        public static void Update()
        {
            lastKeyboard = currentKeyboard;
            currentKeyboard = Keyboard.GetState();
        }

    }
}
