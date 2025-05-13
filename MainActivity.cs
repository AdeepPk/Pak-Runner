using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using Android.Views;

namespace PakRunner;

[Activity(
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
    Theme = "@style/Maui.SplashTheme"
)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        
        // Programmatic UI as fallback
        var layout = new LinearLayout(this)
        {
            Orientation = Orientation.Vertical,
            LayoutParameters = new ViewGroup.LayoutParams(
                ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.MatchParent)
        };

        var player = new ImageView(this)
        {
            LayoutParameters = new LinearLayout.LayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent)
        };
        player.SetImageResource(Resource.Drawable.player_icon);

        var jumpButton = new Button(this)
        {
            Text = "JUMP"
        };

        layout.AddView(player);
        layout.AddView(jumpButton);
        SetContentView(layout);
    }
}
