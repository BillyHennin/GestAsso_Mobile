using System.Collections.Generic;
using Android.App;
using Android.Content;
using GestAsso.Assets.Users;
using GestAsso.Droid.Activities;
using GestAsso.Droid.Assets.ToolBox;

namespace GestAsso.Droid.Assets
{
    public static class Authentication
    {
        public static void OAuth(string sProvider, Activity activity)
        {
            switch (sProvider)
            {
                case "Facebook":
                    XAuth.FacebookAuth(activity);
                    break;
                case "Twitter":
                    break;
                default:
                    XAuth.GoogleAuth(activity);
                    break;
            }
        }

        public static void SimpleAuth(Dictionary<string, string> dict, Activity activity)
        {
            var tempUser = XApi.ApiConnect(dict);
            if (tempUser == null)
            {
                XMessage.ShowError(Resource.String.ErrorInfos, activity);
            }
            else
            {
                Authenticate(tempUser, activity);
            }
        }

        public static void SimpleRegister(Dictionary<string, object> dict, Activity activity)
        {
            if (XApi.RegisterVerifyEmail(dict["Email"].ToString()))
            {
                var tempUser = XApi.RegisterNewUser(dict);
                if (tempUser == null)
                {
                    XMessage.ShowError(Resource.String.ErrorCreateAccount, activity);
                }
                else
                {
                    Authenticate(tempUser, activity);
                }
            }
            else
            {
                XMessage.ShowError(Resource.String.ErrorMailUsed, activity);
            }
        }

        public static void Authenticate(UserProfile user, Activity activity)
        {
            XTools.User = user;
            XProfile.UpdateConnection(true);
            activity.StartActivity(new Intent(Application.Context, typeof(ActivityMain)));
            activity.Finish();
        }
    }
}