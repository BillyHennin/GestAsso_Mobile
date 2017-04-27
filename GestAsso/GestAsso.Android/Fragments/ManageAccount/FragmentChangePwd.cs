using Android.OS;
using Android.Views;
using Android.Widget;
using GestAsso.Droid.Assets;
using GestAsso.Droid.Assets.ToolBox;

namespace GestAsso.Droid.Fragments.ManageAccount
{
    public class FragmentChangePwd : FragmentMain
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = InitView(Resource.String.ChangePWD, this, Resource.Layout.FragmentLayoutChangePassword, inflater, container);
            view.FindViewById<Button>(Resource.Id.ChangePasswordButtonFragment).Click += (s, a) => { ChangePwd(view); };
            return view;
        }

        private static void ChangePwd(View view)
        {
            XUi.HideKeyBoard();
            var newPassword = view.FindViewById<EditText>(Resource.Id.NewPasswordText);
            var newPasswordConfirm = view.FindViewById<EditText>(Resource.Id.PasswordConfirm);
            if (XInputs.CheckNullOrWhiteSpaceInputs(newPassword, newPasswordConfirm))
            {
                if (XInputs.CheckEqualsInput(newPassword, newPasswordConfirm))
                {
                    if (!XTools.User.UpdatePassword(newPassword.Text))
                    {
                        XMessage.ShowError(Resource.String.ErrorNetwork);
                        return;
                    }
                    XMessage.ShowMessage(XTools.GetRsrcStr(Resource.String.SuccessTitle), XTools.GetRsrcStr(Resource.String.SuccessPassword));
                    newPassword.Text = string.Empty;
                    newPasswordConfirm.Text = string.Empty;
                }
                else
                {
                    XInputs.SetEditTextError(newPasswordConfirm, Resource.String.ErrorPassword);
                }
            }
            else
            {
                XMessage.ShowError(Resource.String.ErrorEmpty);
            }
        }
    }
}