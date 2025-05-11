using Microsoft.Maui;
using Microsoft.Maui.Hosting;

class MauiProgram
{
    public static MauiApp CreateMauiApp() => 
        MauiApp.CreateBuilder().UseMauiApp<App>().Build();
}
