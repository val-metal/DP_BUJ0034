using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.ViewModels
{
    public partial class LevelSettingsViewModel : ObservableObject
    {
        [ObservableProperty]
        int numPaths;
        private string type;
        private string name;
        public ImageButton path_1;
        public ImageButton path_2;
        public ImageButton path_3;
        public ImageButton path_3_1;
        public ImageButton difficulty_1;
        public ImageButton difficulty_2;
        public ImageButton difficulty_3;
        [RelayCommand]
        public async Task goBack()
        {
            await Shell.Current.Navigation.PopAsync();
        }

        [RelayCommand]
        public void pocetCest(object num)
        {
            NumPaths = Int32.Parse((string)num) ;
            if (NumPaths == 1)
            {
                path_1.Source = "path1_1_check.png";
                path_2.Source = "path2_2.png";
                path_3.Source = "path3_3.png";
                path_3_1.Source = "path3_1.png";
            }
            else if (NumPaths == 2)
            {
                path_1.Source = "path1_1.png";
                path_2.Source = "path2_2_check.png";
                path_3.Source = "path3_3.png";
                path_3_1.Source = "path3_1.png";
            }
            else if (NumPaths == 3)
            {
                path_1.Source = "path1_1.png";
                path_2.Source = "path2_2.png";
                path_3.Source = "path3_3_check.png";
                path_3_1.Source = "path3_1.png";
            }
            else if (NumPaths == 4)
            {
                path_1.Source = "path1_1.png";
                path_2.Source = "path2_2.png";
                path_3.Source = "path3_3.png";
                path_3_1.Source = "path3_1_check.png";
            }
            else {
                throw new ArgumentOutOfRangeException();
            }

        }

        [ObservableProperty]
        public int difficulty;
        [RelayCommand]
        public void num_of_stars(object num_of_star)
        {
            Difficulty = Int32.Parse((string)num_of_star);

            if (Difficulty == 1)
            {
                difficulty_1.Source = "star_1_check.png";
                difficulty_2.Source = "star_2.png";
                difficulty_3.Source = "star_3.png";
            }
            else if (Difficulty == 2)
            {
                difficulty_1.Source = "star_1.png";
                difficulty_2.Source = "star_2_check.png";
                difficulty_3.Source = "star_3.png";
            }
            else if (Difficulty == 3)
            {
                difficulty_1.Source = "star_1.png";
                difficulty_2.Source = "star_2.png";
                difficulty_3.Source = "star_3_check.png";
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }

        }

        [RelayCommand]
        private async Task startGame()
        {
             await Shell.Current.Navigation.PushAsync(new Board(type, NumPaths, Difficulty, name));
        }
        public LevelSettingsViewModel(string type, ImageButton path_1, ImageButton path_2, ImageButton path_3, ImageButton path_3_1, ImageButton difficulty_1, ImageButton difficulty_2, ImageButton difficulty_3,string name) {
            this.type = type;
            this.path_1 = path_1;
            this.path_2 = path_2;
            this.path_3 = path_3;
            this.path_3_1=path_3_1;
            this.difficulty_1= difficulty_1;
            this.difficulty_2= difficulty_2;
            this.difficulty_3= difficulty_3;
            this.path_1.Source = "path1_1_check.png";
            this.difficulty_1.Source = "star_1_check.png";
            Difficulty = 1;
            NumPaths = 1;
            this.name = name;
        }

    }
}
