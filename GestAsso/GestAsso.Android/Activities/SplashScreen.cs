using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Support.V7.App;
using GestAsso.Droid.Assets.ToolBox;

namespace GestAsso.Droid.Activities
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashScreen : AppCompatActivity
    {
        protected override void OnResume()
        {
            base.OnResume();
            SetContentView(Resource.Layout.ActivityLayoutSplash);
            new Task(async () =>
            {
                var isConnected = Equals(XProfile.Get(), null);
                for (var i = 0; i < 100; i++)
                {
                    await Task.Delay(10);
                }
                StartActivity(new Intent(Application.Context, isConnected
                    ? typeof(ActivityLogin)
                    : typeof(ActivityMain)));
                Finish();
            }).Start();
        }
    }
}