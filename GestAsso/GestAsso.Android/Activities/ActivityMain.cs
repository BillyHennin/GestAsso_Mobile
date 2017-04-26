using System.Net;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Widget;
using GestAsso.Droid.Assets;
using GestAsso.Droid.Assets.ToolBox;
using GestAsso.Droid.Fragments;
using static Android.Support.Design.Widget.NavigationView;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace GestAsso.Droid.Activities
{
    [Activity(Theme = "@style/MyTheme", NoHistory = true, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class ActivityMain : AppCompatActivity
    {
        private static NavigationView _navigationView;
        private static readonly FragmentHome FragmentHome = new FragmentHome();
        private static readonly FragmentTalk FragmentTalk = new FragmentTalk(); 
        private static readonly FragmentAccount FragmentAccount = new FragmentAccount(); 
        private static readonly FragmentAbout FragmentAbout = new FragmentAbout();

        public DrawerLayout DrawerLayout;

        protected override void OnCreate(Bundle b)
        {
            base.OnCreate(b);
            //Initialisation de quelques valeurs statics
            XTools.MainAct = this;
            ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };
            XAction.Init();
            SetContentView(Resource.Layout.ActivityLayoutMain);
            XTools.ChangeFragment(FragmentHome, false);

            // Initialisation de la bar d'action
            var toolbar = FindViewById<Toolbar>(Resource.Id.action_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(false);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            // Ajout de la fonction 'NavigationView_NavigationItemSelected' é l'evenement 'NavigationItemSelected'.
            _navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            _navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
            _navigationView.GetHeaderView(0).FindViewById<TextView>(Resource.Id.txtViewTitle).Text = XTools.User.FullName;
            _navigationView.GetHeaderView(0).FindViewById<TextView>(Resource.Id.txtViewSubTitle).Text = XTools.User.Role.ToString();

            // Creation d'un bouton ActionBarDrawerToggle et ajout é la bar d'action.
            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            var drawerToggle = new ActionBarDrawerToggle(this, DrawerLayout, toolbar, Resource.String.MenuOpened,
                Resource.String.MenuHome);
            DrawerLayout.AddDrawerListener(drawerToggle);
            drawerToggle.SyncState();
        }

        /// <summary>
        ///     Fonction appelée lorsque l'utilisateur choisi un item dans la vue de navigation (Menu lateral gauche)
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void NavigationView_NavigationItemSelected(object s, NavigationItemSelectedEventArgs e)
        {
            switch (e.MenuItem.ItemId)
            {
                case Resource.Id.nav_home:
                    XTools.ChangeFragment(FragmentHome);
                    break;
                case Resource.Id.nav_talk:
                    XTools.ChangeFragment(FragmentTalk);
                    break;
                case Resource.Id.nav_account:
                    XTools.ChangeFragment(FragmentAccount);
                    break;
                case Resource.Id.nav_about:
                    XTools.ChangeFragment(FragmentAbout);
                    break;
                case Resource.Id.nav_disconnect:
                    XMessage.ShowQuestion(Resource.String.MenuDisconnect, Resource.String.DialogDisconnect,
                        aYes => XProfile.Deconnexion(this), null);
                    break;
                default:
                    XUi.SetSnackMessage(Resource.String.ErrorMenuItem);
                    break;
            }
            // Une fois la selection faites, on cache le menu
            DrawerLayout.CloseDrawers();
        }

        #region Set Values
        /// <summary>
        ///     Change le titre qui apparait en haute é gauche de l'application (Dans la bar d'action)
        /// </summary>
        /// <param name="fragment"></param>
        public void SetTitle(FragmentMain fragment)
        {
            SupportActionBar.Title = fragment.StrTitle;
            //Verification du fragment a afficher, si il apparait dans le menu lateral, alors la ligne correspondante est mise en avant
            var index = GetIndex(fragment);
            var menuITem = _navigationView.Menu.GetItem(index >= 0 ? index : 0).SetChecked(true);
            //S'il le fragment n'est pas dans la liste, aucune ligne n'est mise en avant.
            if (index == -1)
            {
                menuITem.SetChecked(false);
            }
        }
        #endregion

        #region Get Values
        /// <summary>
        ///     Recuperation du fragment servant d'accueil, si aucun n'est disponnible, on utilise celui par defaut
        /// </summary>
        /// <returns>Fragment é d'accueil</returns>
        public static FragmentHome GetHome() => FragmentHome ?? new FragmentHome();

        /// <summary>
        ///     Recuperation de l'index suivant le fragment passé en parametre
        /// </summary>
        /// <param name="fragment">Fragment</param>
        /// <returns>Numéro dans la liste (Menu lateral)</returns>
        private static int GetIndex(FragmentMain fragment)
        {
            int index;
            switch (fragment.IdTitle)
            {
                case Resource.String.HomeTitle:
                    index = 0;
                    break;
                case Resource.String.TalkTitle:
                    index = 1;
                    break;
                case Resource.String.AccountTitle:
                    index = 2;
                    break;
                default:
                    index = -1;
                    break;
            }
            return index;
        }
        #endregion
    }
}