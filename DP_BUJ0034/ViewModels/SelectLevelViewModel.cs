using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DP_BUJ0034.Engine;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.PlatformConfiguration.GTKSpecific;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace DP_BUJ0034.ViewModels
{
    public partial class SelectLevelViewModel : ObservableObject
    {
        
        public LevelInfo[] levelInfos;
        public int[] scores;
        [ObservableProperty]
        public int complete_score;
        public SelectLevelViewModel() {
        }
        [RelayCommand]
        public async Task goBack()
        {
            await Shell.Current.Navigation.PopAsync();
        }
        public async Task insertLevels(Microsoft.Maui.Controls.Grid vsl)
        {
            await loadScores();
            levelInfos = await LevelLoader.LoadAllLevels();
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

        public Microsoft.Maui.Controls.Grid AddStarOrLockImage(Microsoft.Maui.Controls.Grid grid, int i)
        {
            Microsoft.Maui.Controls.Grid grid_for_img_star = new Microsoft.Maui.Controls.Grid()
            {
                RowDefinitions =  {
                        new RowDefinition()
                    },
                ColumnDefinitions ={
                        new ColumnDefinition(),
                        new ColumnDefinition()
                    }
            };
            Microsoft.Maui.Controls.Image star_img = new Microsoft.Maui.Controls.Image();
                if (levelInfos[i].unlockAt > Complete_score)
                {

                    Microsoft.Maui.Controls.Image lock_img = new Microsoft.Maui.Controls.Image();
                    //boxView.IsEnabled = false;
                    lock_img.Source="lock.png";
                    grid.SetRow(lock_img, 1);
                    grid.SetColumn(lock_img, 2);
                    lock_img.MaximumHeightRequest = 30;
                    lock_img.VerticalOptions = LayoutOptions.StartAndExpand;
                    lock_img.HorizontalOptions = LayoutOptions.CenterAndExpand;
                    grid.Add(lock_img);
                    
                    star_img.Source = "star_1.png";
                    star_img.VerticalOptions = LayoutOptions.CenterAndExpand;
                    star_img.HorizontalOptions = LayoutOptions.CenterAndExpand;
                    grid_for_img_star.SetRow(star_img, 0);
                    grid_for_img_star.SetColumn(star_img, 1);
                    grid_for_img_star.Add(star_img);

                    Label label_star = new Label();
                    label_star.Text = levelInfos[i].unlockAt.ToString();
                    label_star.TextColor = Color.FromHex("#4a22d2");
                    label_star.FontSize = 16;
                    label_star.VerticalOptions = LayoutOptions.CenterAndExpand;
                    label_star.HorizontalOptions = LayoutOptions.CenterAndExpand;
                    grid_for_img_star.SetRow(label_star, 0);
                    grid_for_img_star.SetColumn(label_star, 0);
                    grid_for_img_star.Add(label_star);

                    grid.SetRow(grid_for_img_star, 0);
                    grid.SetColumn(grid_for_img_star, 2);
                    grid_for_img_star.VerticalOptions = LayoutOptions.EndAndExpand;
                    grid_for_img_star.HorizontalOptions = LayoutOptions.EndAndExpand;
                    grid.Add(grid_for_img_star);

                }
                else
                {
                    if (scores[i] == 0)
                    {
                        star_img.Source = "star_0.png";
                    }
                    else if (scores[i] == 1){
                        star_img.Source = "star_1.png";
                    }
                    else if (scores[i] == 2){
                        star_img.Source = "star_2.png";
                    }
                    else if (scores[i] == 3)
                    {
                        star_img.Source = "star_3.png";
                    }

                    star_img.MaximumHeightRequest = 50;
                    star_img.VerticalOptions = LayoutOptions.CenterAndExpand;
                    star_img.HorizontalOptions = LayoutOptions.CenterAndExpand;
                    grid.SetRowSpan(star_img, 2);
                    grid.SetRow(star_img, 0);
                    grid.SetColumn(star_img, 2);
                    grid.Add(star_img);

                }
            return grid;
        }

        public async Task configPage(Microsoft.Maui.Controls.Grid LevelsLoad)
        {
            
            int count = levelInfos.Length;
            int row_count = (int)Math.Ceiling((double)count / 2);
            for(int i = 0; i < row_count; i++)
            {
                LevelsLoad.RowDefinitions.Add(new RowDefinition());
            }
            LevelsLoad.VerticalOptions= LayoutOptions.CenterAndExpand;
            LevelsLoad.HorizontalOptions = LayoutOptions.CenterAndExpand;

           

            for (int i = 0; i < levelInfos.Length; i++)
            {

                Microsoft.Maui.Controls.Grid grid = new Microsoft.Maui.Controls.Grid()
                {
                    Margin=5,
                    RowDefinitions =  {
                        new RowDefinition(),
                        new RowDefinition()
                    },
                    ColumnDefinitions ={
                        new ColumnDefinition(new GridLength(0.3, GridUnitType.Star)),
                        new ColumnDefinition(),
                        new ColumnDefinition(new GridLength(0.3, GridUnitType.Star))
                    },
                    RowSpacing = 10,
                    ColumnSpacing = 3,
                };

                Microsoft.Maui.Controls.BoxView boxView_for_view = new Microsoft.Maui.Controls.BoxView { Color = Colors.White, Opacity = 0.6, CornerRadius = 10 };

                grid.SetRowSpan(boxView_for_view, 2);
                grid.SetRow(boxView_for_view, 0);
                grid.SetColumnSpan(boxView_for_view, 3);
                grid.SetColumn(boxView_for_view, 0);
                grid.Add(boxView_for_view);

                Microsoft.Maui.Controls.BoxView boxView = new Microsoft.Maui.Controls.BoxView { Color = Colors.White, Opacity = 0,CornerRadius =10};
                
                
                Microsoft.Maui.Controls.Image img = new Microsoft.Maui.Controls.Image();
                img.Source = levelInfos[i].pathForRes;
                img.HorizontalOptions = LayoutOptions.StartAndExpand;
                img.Margin = 3;
                grid.SetRowSpan(img, 2);
                grid.SetRow(img, 0);  
                grid.SetColumn(img, 0);
                grid.Add(img);


                Label label=new Label();
                label.Text = levelInfos[i].nameOfButton;
                label.TextColor = Color.FromHex("#4a22d2");
                label.FontSize = 15;
                label.VerticalOptions = LayoutOptions.CenterAndExpand;
                label.HorizontalOptions = LayoutOptions.CenterAndExpand;
                grid.SetRowSpan(label, 2);
                grid.SetRow(label, 0);
                grid.SetColumn(label, 1);
                grid.Add(label);

                if (levelInfos[i].unlockAt > Complete_score)
                {
                    boxView.IsEnabled = false;
                }

                grid =AddStarOrLockImage(grid, i);

                
                if (i % 2 == 0)
                {
                    LevelsLoad.SetColumn(grid, 0);
                    LevelsLoad.SetRow(grid, (i / 2));
                }
                else
                {
                    LevelsLoad.SetColumn(grid, 1);
                    LevelsLoad.SetRow(grid, ((i - 1) / 2));
                }
                grid.SetRowSpan(boxView, 2);
                grid.SetRow(boxView, 0);
                grid.SetColumnSpan(boxView, 3);
                grid.SetColumn(boxView, 0);
                grid.Add(boxView);

                TapGestureRecognizer tgs = new TapGestureRecognizer();
                string type = levelInfos[i].name;
                string name = levelInfos[i].nameOfButton;
                tgs.CommandParameter = levelInfos[i].name;
                tgs.Command = new Command(async () => { await Shell.Current.Navigation.PushAsync(new LevelSettings(type, name)); });
                boxView.GestureRecognizers.Add(tgs);
                LevelsLoad.Children.Add(grid);

            }

        }

        [RelayCommand]
        public async Task gotolevelSettings(string type) {
            if (levelInfos is null)
            {
                throw new NullReferenceException();
            }
            foreach(LevelInfo levelInfo in levelInfos)
            {
                if (levelInfo.name == type)
                {
                    await Shell.Current.Navigation.PushAsync(new LevelSettings(type, levelInfo.name));
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
