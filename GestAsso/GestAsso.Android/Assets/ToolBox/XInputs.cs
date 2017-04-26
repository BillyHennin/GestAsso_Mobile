using System;
using Android.App;
using Android.Widget;
using GestTool = GestAsso.Assets.ToolBox;

namespace GestAsso.Droid.Assets.ToolBox
{
    internal abstract class XInputs : GestTool.XInputs
    {
        /// <summary>
        ///     Recupere un nombre inconnu d'EditText et renvoie 'true' si aucune d'entre elle n'est vide
        /// </summary>
        /// <param name="argEditTexts">nombre inconnu d'EditText</param>
        /// <returns>true si aucun EditText vide, false sinon.</returns>
        public static bool CheckNullOrWhiteSpaceInputs(params EditText[] argEditTexts)
            => CheckNullOrWhiteSpaceInputs(Array.ConvertAll(argEditTexts, x => x.Text.Trim()));

        /// <summary>
        ///     Recupere un nombre inconnu d'EditText et renvoie 'true' si ils sont tous identiques
        /// </summary>
        /// <param name="argEditTexts">nombre inconnu de chaine de caracteres</param>
        /// <returns>true si tous les EditText sont identiques, false sinon.</returns>
        public static bool CheckEqualsInput(params EditText[] argEditTexts)
            => CheckEqualsInput(Array.ConvertAll(argEditTexts, x => x.Text.Trim()));

        /// <summary>
        ///     Met le focus sur l'EditText + affiche un message d'erreur dessous ce dernier.
        /// </summary>
        /// <param name="argEditText">EditText qui sera focus et recevera un message dd'erreur si besoin</param>
        /// <param name="idMessage">Message d'erreur à afficher si besoin</param>
        /// <param name="atvi">Activitée en cours</param>
        public static void SetEditTextError(EditText argEditText, int idMessage, Activity atvi = null)
        {
            argEditText.SetError(XTools.GetRsrcStr(idMessage, atvi), null);
            argEditText.RequestFocus();
        }
    }
}