using System;
using Android.App;
using GestTool = GestAsso.Assets.ToolBox;

namespace GestAsso.Droid.Assets.ToolBox
{
    internal abstract class XMessage : GestTool.XMessage
    {
        /// <summary>
        ///     Constructeur privé
        /// </summary>
        /// <param name="strTitle">Chaine de caractere qui sera utilisée comme titre</param>
        /// <param name="strMessage">Chaine de caractere qui sera utilisée comme corps de texte</param>
        /// <param name="activity">Activitée en cours</param>
        /// <returns>Retourne un objet de type AlertDialog.Builder qui est prêt à etre modifié et affiché</returns>
        private static AlertDialog.Builder Constructor(string strTitle, string strMessage, Activity activity) 
            => new AlertDialog.Builder(XTools.GetActivity(activity)).SetTitle(strTitle).SetMessage(strMessage);

        /// <summary>
        ///     Affiche l'objet AlertDialog.Builder passé en parametre
        /// </summary>
        /// <param name="builder">Message de type AlertDialog.Builder</param>
        /// <param name="activity"></param>
        private static void ShowAlert(AlertDialog.Builder builder, Activity activity)
            => XUi.ShowOnMainUi(obj => { builder.Create().Show(); }, XTools.GetActivity(activity));

        /// <summary>
        ///     Affiche un message avec "Erreur" comme titre.
        /// </summary>
        /// <param name="idMessage">Resource.String qui correspond au message</param>
        /// <param name="activity">Activitée en cours</param>
        public static void ShowError(int idMessage, Activity activity = null)
            => ShowError(XTools.GetRsrcStr(idMessage, activity), XTools.GetActivity(activity));

        /// <summary>
        ///     Affiche un message avec "Erreur" comme titre
        /// </summary>
        /// <param name="strMessage">Corps du message</param>
        /// <param name="activity">Activitée en cours</param>
        public static void ShowError(string strMessage, Activity activity = null)
            => ShowAlert(Constructor(XTools.GetRsrcStr(Resource.String.ErrorTitle, activity), strMessage, activity),
                XTools.GetActivity(activity));

        /// <summary>
        ///     Affiche un message en tant qu'alerte à l'écran
        /// </summary>
        /// <param name="strTitle">Titre voulu</param>
        /// <param name="strMessage">Corps de texte voulu</param>
        /// <param name="activity">Activitée en cours</param>
        public static void ShowMessage(string strTitle, string strMessage, Activity activity = null)
            => ShowAlert(Constructor(strTitle, strMessage, activity), XTools.GetActivity(activity));

        /// <summary>
        ///     Affiche un message en tant qu'alerte à l'écran qui demande une validation de la part de l'utilisateur pour
        ///     disparaitre
        /// </summary>
        /// <param name="strTitle">Titre voulu</param>
        /// <param name="strMessage">Corps de texte voulu</param>
        /// <param name="activity">Activitée en cours</param>
        public static void ShowNotification(string strTitle, string strMessage, Activity activity = null)
            => ShowAlert(Constructor(strTitle, strMessage, activity).SetPositiveButton("Ok", (s, a) => { }),
                XTools.GetActivity(activity));

        /// <summary>
        ///     Affiche un message en tant qu'alerte à l'écran qui demande un choix de la part de l'utilisateur pour disparaitre
        /// </summary>
        /// <param name="strTitle">Titre voulu</param>
        /// <param name="strMessage">Corps de texte voulu</param>
        /// <param name="actionYes">Action à effectuer si l'utilisateur appuie sur 'Oui'</param>
        /// <param name="actionNo">Action à effectuer si l'utilisateur appuie sur 'Non'</param>
        /// <param name="activity">Activitée en cours</param>
        public static void ShowQuestion(string strTitle, string strMessage, Action<object> actionYes,
            Action<object> actionNo, Activity activity = null) => ShowAlert(
            Constructor(strTitle, strMessage, activity)
                .SetPositiveButton("Oui", (s, a) => { actionYes?.Invoke(null); })
                .SetNegativeButton("Non", (s, a) => { actionNo?.Invoke(null); }),
            XTools.GetActivity(activity));

        /// <summary>
        ///     Affiche un message en tant qu'alerte à l'écran qui demande un choix de la part de l'utilisateur pour disparaitre
        /// </summary>
        /// <param name="iTitle">Identifiant de la ressource qui sera affichée en tant que titre voulu</param>
        /// <param name="iMessage">Identifiant de la ressource qui sera affichée en tant que corps de texte</param>
        /// <param name="aYes">Action à effectuer si l'utilisateur appuie sur 'Oui'</param>
        /// <param name="aNo">Action à effectuer si l'utilisateur appuie sur 'Non'</param>
        /// <param name="activity">Activitée en cours</param>
        public static void ShowQuestion(int iTitle, int iMessage, Action<object> aYes, Action<object> aNo,
            Activity activity = null)
            => ShowQuestion(XTools.GetRsrcStr(iTitle), XTools.GetRsrcStr(iMessage), aYes, aNo, activity);
    }
}