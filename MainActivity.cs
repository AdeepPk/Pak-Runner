using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace PakRunner;

[Activity(
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
    ScreenOrientation = ScreenOrientation.Landscape
)]
public class MainActivity : Activity
{
    private Button jumpButton;
    private ImageView player;
    private bool isGrounded = true;
    private float playerSpeed = 5f;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        
        // Set game view
        SetContentView(Resource.Layout.game_layout);
        
        // Initialize controls
        jumpButton = FindViewById<Button>(Resource.Id.jumpButton);
        player = FindViewById<ImageView>(Resource.Id.playerSprite);
        
        // Game loop
        var timer = new System.Timers.Timer(16); // ~60fps
        timer.Elapsed += (s, e) => RunOnUiThread(UpdateGame);
        timer.Start();
        
        jumpButton.Click += (s, e) => Jump();
    }

    private void UpdateGame()
    {
        // Horizontal movement
        player.TranslationX += playerSpeed;
        
        // TODO: Add obstacle spawning logic
    }

    private void Jump()
    {
        if (isGrounded)
        {
            player.Animate().TranslationYBy(-100f).SetDuration(300).Start();
            isGrounded = false;
            
            // Return to ground
            player.PostDelayed(() => {
                player.Animate().TranslationYBy(100f).SetDuration(300).Start();
                isGrounded = true;
            }, 500);
        }
    }
}
