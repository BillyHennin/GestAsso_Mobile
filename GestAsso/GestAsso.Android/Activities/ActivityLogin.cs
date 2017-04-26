using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using GestAsso.Assets.Users;
using GestAsso.Droid.Assets;
using GestAsso.Droid.Assets.ToolBox;

namespace GestAsso.Droid.Activities
{
    [Activity(Label = "@string/LoginTitle", Theme = "@style/MyTheme")]
    public class ActivityLogin : AppCompatActivity
    {
        protected override void OnCreate(Bundle b)
        {
            base.OnCreate(b);
            SetContentView(Resource.Layout.ActivityLayoutAuth);
            XNetwork.CheckNetwork(this);
            FindViewById<Button>(Resource.Id.AuthButton).Click += SimpleAuthButtonClick;
            FindViewById<Button>(Resource.Id.RegisterButton).Click += (s, a)
                => { StartActivity(new Intent(Application.Context, typeof(ActivityRegister))); };
            FindViewById<Button>(Resource.Id.FacebookAuthButton).Click += (s, a)
                => { Authentication.OAuth("Facebook", this); };
            FindViewById<Button>(Resource.Id.GoogleAuthButton).Click += (s, a)
                => { Authentication.OAuth("Google", this); };
            FindViewById<Button>(Resource.Id.TwitterAuthButton).Click += (s, a)
                => { Authentication.OAuth("Twitter", this); };

            //Autologin
            var dict = new Dictionary<string, string>()
            {
                {"ID", new Guid().ToString()},
                {"Email", "test@test.com"},
                {"FirstName", "Billy"},
                {"LastName", "Hennin"},
                {"Phone", "0102030405" },
                {"UserRole", UserRole.Adherent.ToString()}
            };
            Authentication.Authenticate(new UserProfile(dict), this);
        }

        private void SimpleAuthButtonClick(object s, EventArgs a)
        {
            if (!XNetwork.CheckNetwork(this))
            {
                return;
            }
            var email = FindViewById<EditText>(Resource.Id.EmailText);
            var strEmail = email.Text.Trim();
            var strPassword = FindViewById<EditText>(Resource.Id.PasswordText).Text.Trim();
            if (XInputs.CheckNullOrWhiteSpaceInputs(strEmail, strPassword))
            {
                if (XInputs.ValidEmail(strEmail))
                {
                    XTools.AwaitAction(
                        obj => Authentication.SimpleAuth(
                            new Dictionary<string, string> {{"Mail", email.Text}, {"Pwd", strPassword}}, this), this);
                }
                else
                {
                    XInputs.SetEditTextError(email, Resource.String.ErrorMail, this);
                }
            }
            else
            {
                XMessage.ShowError(Resource.String.ErrorEmpty, this);
            }
        }
    }
}