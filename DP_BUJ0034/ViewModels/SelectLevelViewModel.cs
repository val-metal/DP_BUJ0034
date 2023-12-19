using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.ViewModels
{
    public partial class SelectLevelViewModel :ObservableObject
    {
        [RelayCommand]
        public async Task gotolevelSettings(string type) {
            await Shell.Current.Navigation.PushAsync(new LevelSettings(type));
            // string name=((Button)sender).Text;
        }
    }
}
