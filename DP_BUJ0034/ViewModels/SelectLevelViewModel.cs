using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DP_BUJ0034.Engine;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.PlatformConfiguration.GTKSpecific;
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
        public async Task insertLevels(Microsoft.Maui.Controls.Grid vsl)
        {
            
            Task t = configPage(vsl);
            t.WaitAsync(CancellationToken.None);
            
        }
        public async void refreshScore(object sender, EventArgs e)
        {
            await loadScores();
        }
        public async Task loadScores()
        {
            (scores,Complete_score) = await ScoreLoader.LoadScore().ConfigureAwait(false);
        }
        public async Task configPage(Microsoft.Maui.Controls.Grid LevelsLoad)
        {
            await loadScores();
            levelInfos = await LevelLoader.LoadAllLevels();
            int count = levelInfos.Length;
            int row_count = (int)Math.Ceiling((double)count / 2);
            for(int i = 0; i < row_count; i++)
            {
                LevelsLoad.RowDefinitions.Add(new RowDefinition());
            }
            LevelsLoad.VerticalOptions= LayoutOptions.CenterAndExpand;

            //LevelsLoad.Add(new BoxView
            //{
            //    Color = Colors.Blue
            //}, 1, 0);
            //LevelsLoad.Add(new BoxView
            //{
            //    Color = Colors.Red
            //}, 0, 1);

            for (int i = 0; i < levelInfos.Length; i++)
            {

                //Microsoft.Maui.Controls.Grid grid = new Microsoft.Maui.Controls.Grid()
                //{
                //    RowDefinitions =  {
                //        new RowDefinition()
                //    },
                //    ColumnDefinitions ={
                //        new ColumnDefinition(),
                //        new ColumnDefinition()
                //    }
                //};
                //    Microsoft.Maui.Controls.BoxView boxView = new Microsoft.Maui.Controls.BoxView { Color = Colors.White };
                //    grid.SetRow(boxView, 2);
                
            
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


                Microsoft.Maui.Controls.Image lockInfo = new Microsoft.Maui.Controls.Image();


                if (levelInfos[i].unlockAt >Complete_score)
                {
                    temp.IsEnabled = false;
                    //lockInfo.Source="lock.png";
                    int y = 0;
                }
                else
                {
                    //lockInfo.Source = "unlock.png";t
                    int x = 0;
                }

                if (i % 2 == 0)
                {
                    LevelsLoad.SetColumn(temp, 0);
                    LevelsLoad.SetRow(temp, (i / 2));
                }
                else
                {
                    LevelsLoad.SetColumn(temp, 1);
                    LevelsLoad.SetRow(temp, ((i - 1) / 2));
                }
               
               

                LevelsLoad.Children.Add(temp);
                LevelsLoad.Children.Add(lockInfo);


            }

        }
        [RelayCommand]
        public async Task gotolevelSettings(string type) {
            await Shell.Current.Navigation.PushAsync(new LevelSettings(type));
            // string name=((Button)sender).Text;
        }
    }
}
