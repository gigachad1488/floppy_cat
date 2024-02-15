using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace floppy_cat.Views;

public partial class EndScreen : Window
{
    public EndScreen()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        okButton.Click += delegate
        {
            StaticData.mainMenu.Show();
            Close(null);
        };
    }

    public EndScreen(int res)
    {
        InitializeComponent();

        resText.Text = res.ToString();
    }
}