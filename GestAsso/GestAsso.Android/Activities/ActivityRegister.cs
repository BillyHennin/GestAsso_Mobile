using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using GestAsso.Assets.Users;
using GestAsso.Droid.Assets;
using GestAsso.Droid.Assets.ToolBox;

namespace GestAsso.Droid.Activities
{
    [Activity(Label = "@string/Register", Theme = "@style/MyTheme")]
    public class ActivityRegister : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ActivityLayoutRegister);
            FindViewById<Button>(Resource.Id.ValidateRegisterButton).Click += RegisterCompte;
        }

        private void RegisterCompte(object sender, EventArgs args)
        {
            if (!XNetwork.CheckNetwork(this))
            {
                return;
            }
            var isValid = true;
            var strNom = FindViewById<EditText>(Resource.Id.Nom).Text.Trim();
            var strPrenom = FindViewById<EditText>(Resource.Id.Prenom).Text.Trim();
            var editTextEmail = FindViewById<EditText>(Resource.Id.Email);
            var strEmail = editTextEmail.Text.Trim();
            var editTextpassword = FindViewById<EditText>(Resource.Id.Password);
            var strPassword = editTextpassword.Text.Trim();
            var strPasswordConfirm = FindViewById<EditText>(Resource.Id.PasswordConfirm).Text.Trim();
            if (XInputs.CheckNullOrWhiteSpaceInputs(strNom, strPrenom, strEmail, strPassword, strPasswordConfirm))
            {
                if (!XInputs.CheckNullOrWhiteSpaceInputs(strPassword, strPasswordConfirm))
                {
                    XInputs.SetEditTextError(editTextpassword, Resource.String.ErrorPassword, this);
                    isValid = false;
                }
                if (!XInputs.ValidEmail(strEmail))
                {
                    XInputs.SetEditTextError(editTextEmail, Resource.String.ErrorMail, this);
                    isValid = false;
                }
                if (!isValid)
                {
                    return;
                }
                var dUser = new Dictionary<string, string>
                {
                    {"Email", strEmail},
                    {"Password", strPassword},
                    {"LastName", strNom},
                    {"FirstName", strPrenom},
                    {"Role" , UserRole.EnAttente.ToString()}
                };
                Authentication.SimpleRegister(dUser, this);
            }
            else
            {
                XMessage.ShowError(Resource.String.ErrorMail, this);
            }
        }
    }
}