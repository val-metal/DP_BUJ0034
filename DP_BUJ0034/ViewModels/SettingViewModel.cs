using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DP_BUJ0034.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.ViewModels
{
    public partial class SettingViewModel: ObservableObject
    {
        SettingSave setting;
        private readonly AudioPlayerWrapper awp;
        public SettingViewModel(AudioPlayerWrapper apw) 
        { 
            this.awp = apw;
            
        }
        [ObservableProperty]
        public bool enableOffMusic;
        [ObservableProperty]
        public bool enableOnMusic;
        [ObservableProperty]
        public bool hecked;
        public async void updateSettings(object sender, EventArgs e)
        {

            setting = await SettingLoader.load();
            Hecked = setting.moveView;
            if (setting.musicMute)
            {
                EnableOnMusic = true;
                EnableOffMusic = false;
            }
            else
            {
                EnableOnMusic = false;
                EnableOffMusic = true;
            }
        }
        [RelayCommand]
        public void unmuteMusic()
        {
            EnableOnMusic = !EnableOnMusic;
            EnableOffMusic = !EnableOffMusic;
            awp.unmuteMusic();
            

            setting.musicMute = false;
            SettingLoader.save(setting);

        }
        public void switchMove()
        {
            setting.moveView = !setting.moveView;
            SettingLoader.save(setting);
        }
        [RelayCommand]
        public void muteMusic()
        {
            EnableOnMusic = !EnableOnMusic;
            EnableOffMusic = !EnableOffMusic;
            awp.muteMusic();

            setting.musicMute = true;
            SettingLoader.save(setting);
        }
        [RelayCommand]
        public async Task resetStar()
        {
            bool answer = await Shell.Current.DisplayAlert("", "Opravdu chceš resetovat své hvězdičky?", "Ano", "Ne");
            if (answer == true)
            {
                await ScoreLoader.GetInstance().ResetStars();
            }
        }
    }
}
