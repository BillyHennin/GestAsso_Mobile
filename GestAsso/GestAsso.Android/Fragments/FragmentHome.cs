using Android;
using Android.OS;
using Android.Views;
using Android.Widget;
using GestAsso.Assets.ToolBox;
using GestAsso.Droid.Assets;

namespace GestAsso.Droid.Fragments
{
    public class FragmentHome : FragmentMain
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var user = XTools.User;
            var view = InitView(Resource.String.HomeTitle, this, Resource.Layout.FragmentLayoutHome, inflater,
                container);
            view.FindViewById<TextView>(Resource.Id.UtilisateurText).Text = user.FullName;
            view.FindViewById<TextView>(Resource.Id.EmailText).Text = user.Email;
            return view;
        }
    }
}