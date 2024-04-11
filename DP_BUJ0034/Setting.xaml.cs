using DP_BUJ0034.ViewModels;

namespace DP_BUJ0034;

public partial class Setting : ContentPage
{
    SettingViewModel sm;

    public Setting(SettingViewModel sm)
	{
        this.sm = sm;
		InitializeComponent();
		BindingContext = sm;
		this.Appearing += sm.updateSettings;
	}

    private void checkchange(object sender, CheckedChangedEventArgs e)
    {
        sm.switchMove();
    }
}