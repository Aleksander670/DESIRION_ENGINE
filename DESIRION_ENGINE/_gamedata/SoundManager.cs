using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE._gamedata
{

    public static class SoundManager
    {
        public static float maxSoundVolume = 1f;
        public static float SoundVolume = 1f;

        private static SoundEffect soundEffect = null;
        private static SoundEffectInstance soundInstance = null;

        static SoundManager()
        {
        }

        public static void SoundPlay(ContentManager contentManager, string soundAssetName, bool loop = false)
        {
            soundEffect = contentManager.Load<SoundEffect>($"Audio/Sound/{soundAssetName}");

            soundInstance = soundEffect.CreateInstance();
            soundInstance.IsLooped = loop;
            soundInstance.Volume = SoundVolume;

            soundInstance.Play();
        }

        public static void PlayDistantSound(ContentManager contentManager, string soundAssetName, Vector2 soundPosition, Vector2 listenerPosition, float maxDistanceInCm, bool loop = false)
        {
            float pixelsPerCentimeter = 10f; // pixels in 1sm
            float maxDistanceInPixels = maxDistanceInCm * pixelsPerCentimeter;

            soundEffect = contentManager.Load<SoundEffect>($"Audio/Sound/{soundAssetName}");

            soundInstance = soundEffect.CreateInstance();
            soundInstance.IsLooped = loop;

            float distance = Vector2.Distance(soundPosition, listenerPosition);

            float volume = MathHelper.Clamp(1 - (distance / maxDistanceInPixels), 0, 1) * SoundVolume;
            soundInstance.Volume = volume;

            float pan = MathHelper.Clamp((soundPosition.X - listenerPosition.X) / maxDistanceInPixels, -1f, 1f);
            soundInstance.Pan = pan;

            soundInstance.Play();
        }


        public static void SetMaxSoundVolume(float newMaxSoundVolume)
        {
            maxSoundVolume = newMaxSoundVolume;
            SoundVolume = newMaxSoundVolume;
            UpdateSoundVolume();
        }

        public static void SetSoundVolume(float newSoundVolume)
        {
            SoundVolume = newSoundVolume;
            UpdateSoundVolume();
        }

        public static float GetMaxSoundVolume()
        {
            return maxSoundVolume;
        }

        public static float GetSoundVolume()
        {
            return SoundVolume;
        }

        private static void UpdateSoundVolume()
        {
            if (soundInstance != null)
            {
                soundInstance.Volume = SoundVolume;
            }
        }

        public static void StopSound()
        {
            if (soundInstance != null)
            {
                soundInstance.Stop();
            }
        }


    }

}
