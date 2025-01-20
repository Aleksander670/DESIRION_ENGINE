using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE._gamedata.Render.GameElements
{
    public class GameObjectData
    {

        public string Type { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public string TexturePath { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<string> Script { get; set; }

    }


}
