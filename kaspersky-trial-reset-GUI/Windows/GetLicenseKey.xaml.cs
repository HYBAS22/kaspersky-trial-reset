using System.Windows;
namespace kaspersky_trial_reset_GUI.Windows;

public partial class GetLicenseKey : Window
{
    public GetLicenseKey()
    {
        InitializeComponent();
    }

    private void CopyKeyButton_OnClick(object sender, RoutedEventArgs e)
    {
        string key = "EFD3J-68G5M-VM642-1PUPB";
        
        Clipboard.SetText(key);
    }
}