using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using GestAsso.Droid.Activities;
using GestTool = GestAsso.Assets.ToolBox;

namespace GestAsso.Droid.Assets.ToolBox
{
    internal abstract class XTools : GestTool.XTools
    {
        /// <summary>
        ///     Activitée principale de l'application, celle qui sera appelée lorsqu'aucune activitée ne sera explicitement
        ///     utilisée
        /// </summary>
        public static ActivityMain MainAct;

        /// <summary>
        ///     Affiche un message d'attente pendant tout le temps ou l'action passée en parametre s'effectue
        /// </summary>
        /// <param name="action">Action à effectuer</param>
        /// <param name="activity">Activitée en cours</param>
        public static async void AwaitAction(Func<Task> action, Activity activity = null)
            => await PrivateAwaitAction(action, activity);

        /// <summary>
        ///     Affiche un message d'attente pendant tout le temps ou l'action passée en parametre s'effectue
        /// </summary>
        /// <param name="action">Action à effectuer</param>
        /// <param name="activity">Activitée en cours</param>
        public static async void AwaitAction(Action<object> action, Activity activity = null)
            => await PrivateAwaitAction(action, activity);

        private static async Task PrivateAwaitAction(object action, Activity activity)
        {
            var progressDialog = XUi.ShowWaitProgressDialog(activity);
            await ProtectedAwaitAction(action);
            progressDialog.Dismiss();
        }

        /// <summary>
        ///     Permet de recuperer les variables stockéees et partagées
        ///     Peut être utiliser pour stocker un profil utilisateur, des options, ...
        /// </summary>
        /// <returns>Retourne les variables stockées.</returns>
        public static ISharedPreferences GetSharedPreferences()
            => Application.Context.GetSharedPreferences("GestAsso", FileCreationMode.Private);

        /// <summary>
        ///     Obtiens le string qui correspond à la l'id envoyé en parametre
        /// </summary>
        /// <param name="idMessage">Resource.String qui correspond au message</param>
        /// <param name="atvi">Activitée en cours</param>
        /// <returns>Retourne le string qui correspond à la valeur de "idMessage".</returns>
        public static string GetRsrcStr(int idMessage, Activity atvi = null)
            => GetActivity(atvi).Resources.GetString(idMessage);

        /// <summary>
        ///     Determine si l'activitée passée en parametre est null ou non, si c'est le cas, l'activitée Main est retournée
        /// </summary>
        /// <param name="activity">nullable</param>
        /// <returns>Retourne l'Activitée en cours</returns>
        public static Activity GetActivity(Activity activity = null) => activity ?? MainAct;

        /// <summary>
        ///     Permet de changer le fragment qui est affiché dans l'activité Main
        /// </summary>
        /// <param name="fragment" nullable="false">Fragment qui va être affiché</param>
        /// <param name="bAddToBackStack">Ajouter le fragment au back stack ou non (mettre false pour le premier fragment)</param>
        public static void ChangeFragment(FragmentMain fragment, bool bAddToBackStack = true)
        {
            //Si le fragment n'est pas initialisé, ne rien faire.
            if (fragment == null)
            {
                return;
            }
            //L'Activitée Main peut changer en cas de rotation de l'écran
            //Changer le Fragment qui apparait en fond et l'afficher
            XUi.HideKeyBoard();
            using (var trans = MainAct.SupportFragmentManager.BeginTransaction())
            {
                //Si le fragment par defaut est ajouté au backStack et qu'un utilisateur utilise 
                //le bouton "retour" en bas de son ecran (bouton natif Android), alors
                //un ecran blanc sans vue sera présenté à l'utilisateur
                if (bAddToBackStack)
                {
                    trans.AddToBackStack(null);
                }
                trans.Replace(Resource.Id.HomeFrameLayout, fragment).Commit();
            }
        }
    }
}