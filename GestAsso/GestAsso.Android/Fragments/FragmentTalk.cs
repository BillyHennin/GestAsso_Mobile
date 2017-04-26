using Android.OS;
using Android.Views;
using Android.Widget;
using GestAsso.Droid.Assets;
using GestAsso.Droid.Assets.ToolBox;

namespace GestAsso.Droid.Fragments
{
    public class FragmentTalk : FragmentMain
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var user = XTools.User;
            var view = InitView(Resource.String.TalkTitle, this, Resource.Layout.FragmentLayoutTalk, inflater, container);
            return view;
        }
    }
}