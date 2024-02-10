﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DP_BUJ0034.Engine;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace DP_BUJ0034.ViewModels
{
    public partial class SelectLevelViewModel : ObservableObject
    {
        LevelInfo[] levelInfos;
        int[] scores;
        [ObservableProperty]
        int complete_score;
        public SelectLevelViewModel() {
        }
        public async Task insertLevels(VerticalStackLayout vsl)
        {
            Task t = configPage(vsl);
            t.WaitAsync(CancellationToken.None);
            loadScores();
        }
        public async Task loadScores()
        {
            (scores,Complete_score) = await ScoreLoader.LoadScore().ConfigureAwait(false);
        }
        public async Task configPage(VerticalStackLayout LevelsLoad)
        {
            levelInfos = await LevelLoader.LoadAllLevels();
            for(int i = 0; i < levelInfos.Length-1; i++)
            {
                Button temp = new Button();
                string name = levelInfos[i].name;
                temp.Text = levelInfos[i].nameOfButton;


                temp.VerticalOptions = LayoutOptions.CenterAndExpand;
                temp.Padding = new Thickness(0,0,0,20);
                temp.ImageSource = levelInfos[i].pathForRes;
                temp.MaximumHeightRequest = 64;
                temp.MaximumWidthRequest = 150;
                temp.CommandParameter = levelInfos[i].name;
                temp.Command = new Command(async () => { await Shell.Current.Navigation.PushAsync(new LevelSettings(name)); });



                LevelsLoad.Children.Add(temp);
                
            }

        }
        [RelayCommand]
        public async Task gotolevelSettings(string type) {
            await Shell.Current.Navigation.PushAsync(new LevelSettings(type));
            // string name=((Button)sender).Text;
        }
    }
}
