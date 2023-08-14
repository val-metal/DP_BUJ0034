
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
        
        playFrame = new PlayFrame(this.Resources["drawablee"] as Drawable,1);
        //canvas.Drawable=
        canvas.Invalidate();


    }

    private void goBackPop(object sender, EventArgs e)
    {
        Drawable neco = this.Resources["drawablee"] as Drawable;
        playFrame.drawable = neco;
        playFrame.play(neco.height, neco.width);
        canvas.Drawable = playFrame.drawable;
        canvas.Invalidate();
        
    }

    private void GameView_DragInteraction(object sender, TouchEventArgs e)
    {

    }
}