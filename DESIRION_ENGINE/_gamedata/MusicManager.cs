using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE._gamedata
{
    public static class MusicManager
    {
        public static float maxMusicVolume = 1f;
        public static float MusicVolume = 1f;

        public static Song MusicAmbient = null;


        static MusicManager()
        {

        }


        public static void MusicPlay(ContentManager contentManager, string MusicAssetName)
        {
            MusicAmbient = contentManager.Load<Song>($"Audio/Music/{MusicAssetName}");
            MediaPlayer.Volume = MusicVolume;
            MediaPlayer.Play(MusicAmbient);
        }

        public static void SetMaxMusicVolume(float newMaxMusicVolume)
        {
            maxMusicVolume = newMaxMusicVolume;
            MusicVolume = newMaxMusicVolume;
            MediaPlayer.Volume = MusicVolume;
        }

        public static void SetMusicVolume(float newMusicVolume)
        {
            MusicVolume = newMusicVolume;
            MediaPlayer.Volume = MusicVolume;
        }

        public static void StopMusic()
        {
            if (MusicAmbient != null)
            {
                MediaPlayer.Stop();
            }
        }

        public static float GetMaxMusicVolume()
        {
            return maxMusicVolume;
        }

        public static float GetMusicVolume()
        {
            return MusicVolume;
        }


    }
}
