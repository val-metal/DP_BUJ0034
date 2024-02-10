using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DP_BUJ0034.Engine;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        readonly AudioPlayerWrapper player;
        readonly SelectLevelMenu sllm;
        public MainPageViewModel(AudioPlayerWrapper aw, SelectLevelMenu svm) {
            sllm = svm;
            player = aw;
            player.PlayRandom();
            
        }

        [RelayCommand]
        public async Task goToMenuSelect()
        {
            await Shell.Current.Navigation.PushAsync(sllm);
        }
    }
}
