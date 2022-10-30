using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using Microsoft.Win32;

namespace kaspersky_trial_reset_GUI.Windows;

public partial class ResetTrial : Window
{
    public ResetTrial()
    {
        InitializeComponent();
        CheckForEset();
    }

    private void CheckForEset()
    {
        if (File.Exists(@"C:\Program Files\ESET\ESET Security\ecmds.exe"))
        {
            LoadingText.Text = "Простите, но ваш антивирус судя по всему Eset. В этом случае вы можете просто переустановить антивирус, и найти ключ";
        }
        else
        {
            ClearingRegedit();
        }
    }

    private void ClearingRegedit()
    {
        Thread.Sleep(300);

        string loggerSearch, libFolder;
        List<string> errors = new List<string>();
        
        if (Environment.Is64BitOperatingSystem)
        {
            loggerSearch = "SOFTWARE\\Wow6432Node\\Microsoft\\SystemCertificates\\SPC";
            libFolder = "SOFTWARE\\Wow6432Node\\KasperskyLab";
        }
        else
        {
            loggerSearch = "SOFTWARE\\Microsoft\\SystemCertificates\\SPC";
            libFolder = "SOFTWARE\\KasperskyLab";
        }

        try
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(loggerSearch, true))
            {
                key.DeleteSubKeyTree("Certificates", true);
            }
        }
        
        catch (System.Security.SecurityException)
        {
            errors.Add("\nУ вас нету доступа. Запустите программу от имени администратора");
        }
        catch (ArgumentException)
        {
            errors.Add("\nВозможно Certificates уже пуста");
        }
        catch (Exception e)
        {
            errors.Add($"\nНе удалось удалить Certificates, из за - {e}");
        }

        Thread.Sleep(300);

        try
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(libFolder, true))
            {
                key.DeleteSubKeyTree("LicStorage", true);
            }
        }
        
        catch (System.Security.SecurityException) { }
        catch (ArgumentException)
        {
            errors.Add("\nВозможно LicStorage уже пуста");
        }
        catch (Exception e)
        {
            errors.Add($"\nНе удалось удалить LicStorage, из за - {e}");
        }

        Thread.Sleep(300);

        if (errors.Count == 0)
        {
            LoadingText.Text = "Чистка окончена, вы можете вернуться в меню и получить ключ, а так же открыть сайт и пролистать вниз, что бы переустановить антивирус";
            OpenBrowserButton.Visibility = Visibility.Visible;
        }
        else
        { 
            LoadingText.Text = "Чистка окончена не успешно, вывожу ошибки (если проблема в отсутствии файлов, то можно игнорировать): " + string.Join("\n", errors);
            ErrorImage.Visibility = Visibility.Visible;
        }
    }

    private void OpenBrowser_OnClick(object sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = $"https://www.kaspersky.ru/downloads",
            UseShellExecute = true
        });
    }
}