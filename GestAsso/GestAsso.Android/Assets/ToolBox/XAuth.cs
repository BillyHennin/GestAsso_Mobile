using System;
using System.Json;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Xamarin.Auth;
using GestTool = GestAsso.Assets.ToolBox;

namespace GestAsso.Droid.Assets.ToolBox
{
    internal abstract class XAuth : GestTool.XAuth
    {
        private static readonly TaskScheduler UiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

        private static void DoOAuth(string clientId, string scope, string authUrl, string redirectUrl,
            Activity activity)
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var auth = new OAuth2Authenticator(clientId, scope, new Uri(authUrl), new Uri(redirectUrl));
            auth.Completed += (s, ee) =>
            {
                if (!ee.IsAuthenticated)
                {
                    XMessage.ShowNotification("Erreur", "L'authentification a echouée", activity);
                    return;
                }

                // Now that we're logged in, make a OAuth2 request to get the user's info.
                var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me"), null, ee.Account);
                request.GetResponseAsync()
                    .ContinueWith(t =>
                    {
                        var builder = new AlertDialog.Builder(activity);
                        if (t.IsFaulted)
                        {
                            builder.SetTitle("Error");
                            builder.SetMessage(t.Exception.Flatten().InnerException?.ToString());
                        }
                        else if (t.IsCanceled)
                        {
                            builder.SetTitle("Task Canceled");
                        }
                        else
                        {
                            builder.SetTitle("Logged in");
                            builder.SetMessage("Name: " + JsonValue.Parse(t.Result.GetResponseText())?["name"]);
                        }
                        builder.SetPositiveButton("Ok", (o, e) => { });
                        builder.Create().Show();
                    }, UiScheduler);
            };
            // If authorization succeeds or is canceled, .Completed will be fired.
            activity.StartActivity((Intent) auth.GetUI(activity));
        }

        public static void GoogleAuth(Activity activity)
            => DoOAuth(StrGoogleKey, StrGoogleScope, StrGoogleAuthUrl, StrGoogleRtrUrl, activity);
        //http://www.appliedcodelog.com/2015/08/login-by-google-account-integration-for.html

        public static void FacebookAuth(Activity activity)
            => DoOAuth(StrFacebookKey, StrFacebookScope, StrFacebookAuthUrl, StrFacebookRtrUrl, activity);
    }
}