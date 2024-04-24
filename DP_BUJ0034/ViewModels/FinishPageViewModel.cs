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
        string type;

        [ObservableProperty]
        public string imgName;
        [ObservableProperty]
        public string timeString;
        [ObservableProperty]
        public string levelName;
        [ObservableProperty]
        public string percentage;
        [ObservableProperty]
        public bool isContinueShow;
        public FinishPageViewModel(int num_paths,int difficulty,string type,long time,string levelName,double percentage) {

            this.type = type;
            LevelName = levelName;
            Percentage = percentage.ToString()+"%";
            this.num_paths = num_paths;
            this.difficulty = difficulty;
            double minuty=Math.Floor(TimeSpan.FromMilliseconds(time).TotalMinutes);
            double sekundy= (Math.Floor(TimeSpan.FromMilliseconds(time).TotalSeconds)) % 60;
            if (sekundy < 10)
            {
                TimeString = minuty + ":0" + sekundy;
            }
            else
            {
                TimeString = minuty + ":" + sekundy;
            }

            if (percentage < 80)
            {
                imgName = "star_0.png";
                isContinueShow = true;
            }
            else
            {
                if (difficulty == 1)
                {
                    imgName = "star_1.png";
                }
                else if (difficulty == 2)
                {
                    imgName = "star_2.png";
                }
                else if (difficulty == 3)
                {
                    imgName = "star_3.png";
                }
                addScore(type);
            }
            

        }
        public async Task showScore(int num_paths, int difficulty, string type)
        { 
            
        }
        public async Task addScore(string type)
        {
           await ScoreLoader.GetInstance().AddStars(difficulty,type);
        }
        [RelayCommand]

        public async Task GoToMenu()
        {
            await Shell.Current.Navigation.PopToRootAsync();
            await Shell.Current.Navigation.PushAsync(new SelectLevelMenu(new SelectLevelViewModel()));
        }
        [RelayCommand]

        public async Task Repeat()
        {
            await Shell.Current.Navigation.PopToRootAsync();
            await Shell.Current.Navigation.PushAsync(new Board(type,num_paths,difficulty,LevelName));
        }
    }
}
