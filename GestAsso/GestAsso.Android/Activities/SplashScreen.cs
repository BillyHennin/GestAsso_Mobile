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
            var startupWork = new Task(async () =>
            {
                for (var i = 0; i < 100; i++)
                {
                    await Task.Delay(10);
                }
            });
            startupWork.ContinueWith(t =>
            {
                StartActivity(new Intent(Application.Context, Equals(XProfile.Get(), null)
                    ? typeof(ActivityLogin)
                    : typeof(ActivityMain)));
                Finish();
            }, TaskScheduler.FromCurrentSynchronizationContext());
            startupWork.Start();
        }
    }
}