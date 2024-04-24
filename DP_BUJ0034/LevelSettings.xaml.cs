using DP_BUJ0034.ViewModels;

namespace DP_BUJ0034;

public partial class LevelSettings : ContentPage
{


    public LevelSettings(string type,string name)
	{
		InitializeComponent();
		
		BindingContext=new LevelSettingsViewModel(type,path_1,path_2,path_3,path_3_1,difficulty_1,difficulty_2,difficulty_3,type);
	}
}