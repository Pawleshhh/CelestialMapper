namespace CelestialMapper.UI;

using CelestialMapper.Common;
using System;
using System.Reflection;
using System.Windows;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static IServiceProvider ServiceProvider { get; private set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        ServiceProvider = Boot();

        LoadTheme();

        MainWindow = new MainWindow();
        MainWindow.DataContext = GetMainViewModel();

        MainWindow.Show();
    }

    private static IServiceProvider Boot()
    {
        return Bootstrapper.Boot(Assembly.GetExecutingAssembly());
    }

    private static void LoadTheme()
    {
        ResourceDictionary colorsDictionary = new ResourceDictionary();
        colorsDictionary.Source = new Uri("/CelestialMapper.UI;component/Resources/Themes/Colors.Light.xaml", UriKind.Relative);

        // Add the loaded ResourceDictionary to the application's resources
        Application.Current.Resources.MergedDictionaries.Add(colorsDictionary);
    }

    private static object GetMainViewModel()
        => new object();
}