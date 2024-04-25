using DP_BUJ0034.ViewModels;

namespace DP_BUJ0034;

public partial class SelectLevelMenu :ContentPage
{
	public SelectLevelMenu(SelectLevelViewModel svm)
	{
		InitializeComponent();
		svm.insertLevels(LevelsLoad);
		this.NavigatedTo += svm.refreshScore;
		BindingContext = svm;
		
	}
}