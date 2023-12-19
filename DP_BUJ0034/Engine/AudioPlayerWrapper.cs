using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using Plugin.Maui.Audio;
using System.Runtime.CompilerServices;

namespace DP_BUJ0034.Engine
{
    class AudioPlayerWrapper
    {
        private static AudioPlayerWrapper instance;
        private IAudioPlayer audioPlayer;
        private string currentAudio;
        private AudioPlayerWrapper()
        {
        }
        public async void Play(string audioName)
        {
            if (audioPlayer is not null)
            {
                audioPlayer.Stop();
            }
            audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(audioName));
            audioPlayer.Play();
            currentAudio = audioName;
            audioPlayer.PlaybackEnded += repeat;
        }
        private void repeat(object sender, EventArgs e)
        {
            audioPlayer.Play();
        }
        public static AudioPlayerWrapper GetInstance()
        {
            if (instance == null)
            {
                instance = new AudioPlayerWrapper();
            }

            return instance;
        }

    }
}

