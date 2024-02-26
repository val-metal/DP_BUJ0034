using DP_BUJ0034.ViewModels;

namespace DP_BUJ0034;

public partial class FinishPage : ContentPage
{
	public FinishPage(int num_paths, int difficulty,string type,long time)
	{
		InitializeComponent();
		BindingContext = new FinishPageViewModel(num_paths,difficulty,type,time);
	}
}