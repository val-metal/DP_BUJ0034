

using DP_BUJ0034.Engine;
using DP_BUJ0034.ViewModels;
using Plugin.Maui.Audio;

namespace DP_BUJ0034;

public partial class MainPage : ContentPage
{
	int count = 0;
    AudioPlayerWrapper audioPlayer;

	public MainPage(MainPageViewModel mv)
	{
        BindingContext = mv;
        InitializeComponent();
	}

	async private void OnCounterClicked(object sender, EventArgs e)
	{
        
        await Navigation.PushAsync(new Board());

	}


    private void SpustitHruClicked(object sender, EventArgs e)
    {
    }

}

