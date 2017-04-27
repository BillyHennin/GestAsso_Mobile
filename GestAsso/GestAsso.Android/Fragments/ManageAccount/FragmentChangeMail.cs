using Android.OS;
using Android.Views;
using Android.Widget;
using GestAsso.Droid.Assets;
using GestAsso.Droid.Assets.ToolBox;

namespace GestAsso.Droid.Fragments.ManageAccount
{
    public class FragmentChangeMail : FragmentMain
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = InitView(Resource.String.ChangeMail, this, Resource.Layout.FragmentLayoutChangeMail, inflater, container);
            view.FindViewById<Button>(Resource.Id.ChangeMailButtonFragment).Click += (s, a) => { ChangeMail(view); };
            return view;
        }

        private static void ChangeMail(View view)
        {
            XUi.HideKeyBoard();
            var newMail = view.FindViewById<EditText>(Resource.Id.NewEmailText);
            var newMailConfirm = view.FindViewById<EditText>(Resource.Id.NewEmailConfirmText);
            if (XInputs.CheckNullOrWhiteSpaceInputs(newMail, newMailConfirm))
            {
                if (XInputs.ValidEmail(newMail.Text))
                {
                    if (XInputs.CheckEqualsInput(newMail, newMailConfirm))
                    {
                        if (!XTools.User.UpdatePassword(newMail.Text))
                        {
                            XMessage.ShowError(Resource.String.ErrorNetwork);
                            return;
                        }
                        XMessage.ShowMessage(XTools.GetRsrcStr(Resource.String.SuccessTitle), XTools.GetRsrcStr(Resource.String.SuccessEmail));
                        newMail.Text = string.Empty;
                        newMailConfirm.Text = string.Empty;
                    }
                    else
                    {
                        XInputs.SetEditTextError(newMailConfirm, Resource.String.ErrorNewMail);
                    }
                }
                else
                {
                    XInputs.SetEditTextError(newMail, Resource.String.ErrorMail);
                }
            }
            else
            {
                XMessage.ShowError(Resource.String.ErrorEmpty);
            }
        }
    }
}