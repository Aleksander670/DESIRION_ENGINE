using DESIRION_ENGINE._gamedata.Render.GameElements;
using DESIRION_ENGINE._gamedata.Render.UiElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE._gamedata.Render
{
    public class UiCanvasData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<UiElementData> UiElements { get; set; }

    }
}
