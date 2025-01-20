using DESIRION_ENGINE._gamedata.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE._gamedata.Render.UiElements
{
    public class UiElementData
    {
        public TypeUI Type { get; set; }
        public string Content { get; set; }
        public string Source { get; set; }
        public string Font { get; set; }
        public string ActiveTexture { get; set; }
        public string InactiveTexture { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float SizeX { get; set; }
        public float SizeY { get; set; }
        public OriginElement Origin { get; set; }
        public string ColorHex { get; set; }
        public List<string> Script { get; set; }
    }




}
