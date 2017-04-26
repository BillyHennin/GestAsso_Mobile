using Android.OS;
using Android.Views;
using Android.Widget;
using GestAsso.Assets.ToolBox;
using GestAsso.Droid.Assets;

namespace GestAsso.Droid.Fragments
{
    public class FragmentAccount : FragmentMain
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var user = XTools.User;
            var view = InitView(Resource.String.AccountTitle, this, Resource.Layout.FragmentLayoutAccount, inflater,
                container);
            view.FindViewById<TextView>(Resource.Id.UtilisateurText).Text = user.FullName;
            view.FindViewById<TextView>(Resource.Id.EmailText).Text = user.Email;
            view.FindViewById<TextView>(Resource.Id.PhoneText).Text = user.Phone;
            view.FindViewById<TextView>(Resource.Id.RoleText).Text = XTools.SplitCamelCase(user.Role.ToString());
            return view;
        }
    }
}