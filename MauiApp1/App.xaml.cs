using MauiApp1.Pages;
using Microsoft.Maui.Controls;

namespace MauiApp1;

public partial class App : Application
{
    public App(ClientsPage clientsPage)
    {
        InitializeComponent();

        // Set the main page to the ClientsPage
        MainPage = new NavigationPage(clientsPage);
    }
}
