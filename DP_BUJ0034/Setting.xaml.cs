using DP_BUJ0034.ViewModels;

namespace DP_BUJ0034;

public partial class Setting : ContentPage
{
	public Setting(SettingViewModel sm)
	{

		InitializeComponent();
		BindingContext = sm;
		this.Appearing += sm.updateSettings;
	}
}