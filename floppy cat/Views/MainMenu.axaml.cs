using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace floppy_cat.Views;

public partial class MainMenu : Window
{
    public MainMenu()
    {
        InitializeComponent();

        playButton.Click += delegate
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Hide();
        };

        StaticData.mainMenu = this;
    }
}