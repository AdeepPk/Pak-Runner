using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace PakRunner;

[Activity(
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation
)]
public class MainActivity : Activity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        
        // Enable nullable context
        #nullable enable
        
        // Create UI programmatically (temporary workaround)
        var layout = new LinearLayout(this)
        {
            Orientation = Orientation.Vertical
        };

        var player = new ImageView(this)
        {
            Id = Resource.Id.playerSprite
        };
        player.SetImageResource(Resource.Drawable.player_icon);

        var jumpButton = new Button(this)
        {
            Id = Resource.Id.jumpButton,
            Text = "JUMP"
        };

        layout.AddView(player);
        layout.AddView(jumpButton);
        SetContentView(layout);
    }
}
