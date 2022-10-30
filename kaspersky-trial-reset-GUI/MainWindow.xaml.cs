using System.Diagnostics;
using System.Windows;
using kaspersky_trial_reset_GUI.Windows;

namespace kaspersky_trial_reset_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ResetTrialButton_OnClick(object sender, RoutedEventArgs e)
        {
            ResetTrial trialWindow = new ResetTrial();
            trialWindow.Show();
        }

        private void GetKeyButton_OnClick(object sender, RoutedEventArgs e)
        {
            GetLicenseKey getKey = new GetLicenseKey();
            getKey.Show();
        }

        private void OpenSiteButton_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = $"https://www.kaspersky.ru/downloads",
                UseShellExecute = true
            });
        }
    }
}