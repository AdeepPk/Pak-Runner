using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace PakRunner;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        return builder.Build();
    }
}
