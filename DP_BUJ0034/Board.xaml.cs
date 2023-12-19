
using DP_BUJ0034.Drawables;
using DP_BUJ0034.Engine;
using Microsoft.Maui.Graphics;



namespace DP_BUJ0034;

public partial class Board : ContentPage
{
    PlayFrame playFrame;
	public Board()
    {
		InitializeComponent();
        
        playFrame = new PlayFrame(this.Resources["drawablee"] as Drawable,3,1);
        playFrame.drawable.textureProvider.loadByName("SteamSky");
        //canvas.Drawable=
       idn();


    }
    public Board(string type,int num_paths, int difficulty)
    {
        InitializeComponent();

        playFrame = new PlayFrame(this.Resources["drawablee"] as Drawable, num_paths,difficulty);
        playFrame.drawable.textureProvider.loadByName(type);
        //canvas.Drawable=
        idn();


    }
    private async void idn()
    {
        await Task.Delay(150);
        canvas.Invalidate();
        Drawable neco = this.Resources["drawablee"] as Drawable;
        playFrame.drawable = neco;
        playFrame.play(neco.height, neco.width);
        canvas.Drawable = playFrame.drawable;
        canvas.Invalidate();

    }

  
    

    private void GameView_DragInteraction(object sender, TouchEventArgs e)
    {
        var touch = e.Touches.First();
        //Application.Current.MainPage.DisplayAlert("Upozornìní", "Toto je ukázkové upozornìní."+touch.X+" "+playFrame.gameBoard.player.position.X, "OK");
        playFrame.movePlayer(touch.X, touch.Y);
        canvas.Invalidate();
    }

    private void Back_Clicked(object sender, EventArgs e)
    {
        this.Navigation.PopAsync();
    }
}