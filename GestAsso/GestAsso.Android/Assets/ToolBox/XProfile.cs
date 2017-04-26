using System.Collections.Generic;
using Android.App;
using Android.Content;
using GestAsso.Assets.Users;
using GestAsso.Droid.Activities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GestTool = GestAsso.Assets.ToolBox;

namespace GestAsso.Droid.Assets.ToolBox
{
    internal abstract class XProfile : GestTool.XProfile
    {
        public static UserProfile Get()
        {
            var strAuth = XTools.GetSharedPreferences().GetString("user", null);
            if (string.IsNullOrEmpty(strAuth) || strAuth.Equals("null"))
            {
                return null;
            }
            try
            {
                var strJson = JObject.Parse(strAuth);
                if (strJson != null)
                {
                    var user = new UserProfile(XTools.JsonToDict(strJson.ToString()));
                    if (Equals(XTools.User, null))
                    {
                        XTools.User = user;
                    }
                    return user;
                }
            }
            catch
            {
                //XMessage.ShowError(Resource.String.ErrorUnknow);
            }
            return null;
        }

        public static void UpdateConnection(bool toConnect) => XTools.GetSharedPreferences()
            .Edit()
            .PutString("user", toConnect ? JsonConvert.SerializeObject(XTools.User) : string.Empty)
            .Commit();

        public static void Deconnexion(Activity atvi)
        {
            //Detruire les infos de l'utilisateur
            XTools.User = null;
            UpdateConnection(false);
            //Rediriger
            atvi.StartActivity(new Intent(Application.Context, typeof(ActivityLogin)));
            atvi.Finish();
        }
    }
}