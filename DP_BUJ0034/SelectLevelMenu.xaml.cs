using DP_BUJ0034.ViewModels;

namespace DP_BUJ0034;

public partial class SelectLevelMenu : ContentPage
{
	public SelectLevelMenu()
	{
		InitializeComponent();
		BindingContext = new SelectLevelViewModel();
	}
}