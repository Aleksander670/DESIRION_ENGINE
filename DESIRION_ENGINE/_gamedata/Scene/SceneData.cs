using DESIRION_ENGINE._gamedata.API;
using DESIRION_ENGINE._gamedata.Render.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE._gamedata.Scene
{
    public class SceneData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<string> CameraScript { get; set; }
        public BackgroundData Background { get; set; }
        public List<GameObjectData> Objects { get; set; }
        public List<string> Scripts { get; set; }
    }

}
