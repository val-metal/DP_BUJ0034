

using DP_BUJ0034.Engine;
using DP_BUJ0034.ViewModels;
using Plugin.Maui.Audio;

namespace DP_BUJ0034;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
        
        InitializeComponent();
	}

	async private void OnCounterClicked(object sender, EventArgs e)
	{
        AudioPlayerWrapper.GetInstance().Play("happy1.mp3");
        await Navigation.PushAsync(new Board());

	}

    private void GoToMenuSelect(object sender, EventArgs e)
    {
		this.Navigation.PushAsync(new SelectLevelMenu());
    }

    private void SpustitHruClicked(object sender, EventArgs e)
    {
    }

}

