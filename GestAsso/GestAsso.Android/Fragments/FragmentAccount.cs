using Android.OS;
using Android.Views;
using Android.Widget;
using GestAsso.Droid.Assets.ToolBox;
using GestAsso.Droid.Assets;
using GestAsso.Droid.Fragments.ManageAccount;

namespace GestAsso.Droid.Fragments
{
    public class FragmentAccount : FragmentMain
    {
        private FragmentChangeMail _fragmentChangeMail;
        private FragmentChangePwd _fragmentChangePwd;

        public FragmentAccount()
        {
            FragmentPassword = new FragmentChangePwd();
            FragmentMail = new FragmentChangeMail();
        }

        private FragmentChangeMail FragmentMail
        {
            set
            {
                if (value != null)
                {
                    _fragmentChangeMail = value;
                }
            }
            get { return _fragmentChangeMail ?? new FragmentChangeMail(); }
        }

        private FragmentChangePwd FragmentPassword
        {
            set
            {
                if (value != null)
                {
                    _fragmentChangePwd = value;
                }
            }
            get { return _fragmentChangePwd ?? new FragmentChangePwd(); }
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var user = XTools.User;
            var view = InitView(Resource.String.AccountTitle, this, Resource.Layout.FragmentLayoutAccount, inflater,
                container);
            view.FindViewById<TextView>(Resource.Id.UtilisateurText).Text = user.FullName;
            view.FindViewById<TextView>(Resource.Id.EmailText).Text = user.Email;
            view.FindViewById<TextView>(Resource.Id.PhoneText).Text = user.Phone;
            view.FindViewById<TextView>(Resource.Id.RoleText).Text = XTools.SplitCamelCase(user.Role.ToString());
            view.FindViewById<Button>(Resource.Id.ChangeMailButton).Click += (s, a) => { XTools.ChangeFragment(FragmentMail); };
            view.FindViewById<Button>(Resource.Id.ChangePWDButton).Click += (s, a) => { XTools.ChangeFragment(FragmentPassword); };
            return view;
        }
    }
}