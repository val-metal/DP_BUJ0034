using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using Plugin.Maui.Audio;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace DP_BUJ0034.Engine
{
    public class AudioPlayerWrapper
    {
        private IAudioPlayer audioPlayer;
        private string currentAudio;
        public AudioPlayerWrapper()
        {
        }
        public async Task Play(string audioName)
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
        public async Task PlayRandom()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("MusicFiles.json");
            using var reader = new StreamReader(stream);

            string contents = reader.ReadToEnd();
            MusicFile[] mfs = JsonSerializer.Deserialize<MusicFile[]>(contents);
            if (mfs.Length > 0)
            {
                Random random = new Random();
                int randomIndex = random.Next(0, mfs.Length); 

               await Play(mfs[randomIndex].Name); 

            }
            else
            {
                // TODO :Exp
            }

        }
        private void repeat(object sender, EventArgs e)
        {
            audioPlayer.Play();
        }
        //public static AudioPlayerWrapper GetInstance()
        //{
        //    if (instance == null)
        //    {
        //        instance = new AudioPlayerWrapper();
        //    }

        //    return instance;
        //}

    }
}

