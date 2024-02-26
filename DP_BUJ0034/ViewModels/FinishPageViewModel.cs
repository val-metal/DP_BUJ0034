using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.ViewModels
{
    public partial class FinishPageViewModel: ObservableObject
    {
        public int num_paths;
        public int difficulty;

        [ObservableProperty]
        public string imgName;
        [ObservableProperty]
        public string timeString;
        public FinishPageViewModel(int num_paths,int difficulty,string type,long time) { 

            this.num_paths = num_paths;
            this.difficulty = difficulty;
            TimeString = Math.Floor(TimeSpan.FromMilliseconds(time).TotalMinutes)+":"+ Math.Floor(TimeSpan.FromMilliseconds(time).TotalSeconds);


            if (difficulty == 1 ) {
                imgName = "star_1.png";
            }
            else if( difficulty == 2 )
            {
                imgName = "star_2.png";
            }
            else if (difficulty == 3)
            {
                imgName = "star_3.png";
            }
            addScore(type);

        }
        public async Task showScore(int num_paths, int difficulty, string type)
        { 
            
        }
        public async Task addScore(string type)
        {
           await ScoreLoader.GetInstance().AddStars(difficulty,type);
        }
        [RelayCommand]

        public async void GoToMenu()
        {
            await Shell.Current.Navigation.PopToRootAsync();
            await Shell.Current.Navigation.PushAsync(new SelectLevelMenu(new SelectLevelViewModel()));
        }
    }
}
