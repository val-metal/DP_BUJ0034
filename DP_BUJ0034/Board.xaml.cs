
using DP_BUJ0034.Drawables;
using DP_BUJ0034.Engine;
using DP_BUJ0034.ViewModels;
using Microsoft.Maui.Graphics;



namespace DP_BUJ0034;

public partial class Board : ContentPage
{
    PlayFrame playFrame;
    string type;
    string name;
	public Board()
    {
		InitializeComponent();
        
        playFrame = new PlayFrame(this.Resources["drawablee"] as Drawable,3,1);
        playFrame.drawable.textureProvider.loadByName("SteamSky");
        //canvas.Drawable=
       idn();


    }
    public Board(string type,int num_paths, int difficulty,string name)
    {
        InitializeComponent();

        playFrame = new PlayFrame(this.Resources["drawablee"] as Drawable, num_paths,difficulty);
        playFrame.drawable.textureProvider.loadByName(type);
        this.type = type;
        //canvas.Drawable=
        this.name = name;
        idn();


    }
    private async void idn()
    {
        await Task.Delay(150);
        canvas.Invalidate();
        LevelInfo li = await LevelLoader.LoadLevelByName(name);
        popisek_levelu.Text = li.name;
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
        if (playFrame.gameEnds)
        {
            double percentage=playFrame.countMovePercentage();
            this.Navigation.PushAsync(new FinishPage(playFrame.num_paths,playFrame.num_difficulty,type,playFrame.stopwatch.ElapsedMilliseconds,name,percentage));
        }
        canvas.Invalidate();
    }

    private void Back_Clicked(object sender, EventArgs e)
    {
        this.Navigation.PopAsync();
    }
}