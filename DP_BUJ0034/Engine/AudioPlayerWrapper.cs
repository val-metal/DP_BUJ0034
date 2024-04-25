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
        public async Task Play(string audioName)
        {
            bool saveMusicSet = (await SettingLoader.load()).musicMute;

            if (audioPlayer is not null )
            {
                audioPlayer.Stop();
                if (saveMusicSet == true)
                {
                    muteMusic();
                }
                else
                {
                    await unmuteMusic();
                }
            }
            if (saveMusicSet == false)
            {
                audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(audioName));
                audioPlayer.Play();
                currentAudio = audioName;
                audioPlayer.PlaybackEnded += repeat;
            }
           
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
                throw new InvalidDataException();
            }
        }
        private void repeat(object sender, EventArgs e)
        {
            audioPlayer.Play();
        }

        public void muteMusic()
        {
            if (audioPlayer is not null)
            {
                audioPlayer.Volume = 0;
            }
        }
        public async Task unmuteMusic()
        {
            if (audioPlayer is not null)
            {
                audioPlayer.Volume = 1;
            }
            else
            {
               await PlayRandom();
            }
        }
    }
}

