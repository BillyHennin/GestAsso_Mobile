using System.Linq;
using System.Text.RegularExpressions;

namespace GestAsso.Assets.ToolBox
{
    /// <summary>
    ///     Classe static qui s'occupe de gere les champs de saisie.
    /// </summary>
    public abstract class XInputs
    {
        private const string RegexEmail =
        @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        /// <summary>
        ///     Verifie si la chaine passée en parametre est un email valide
        /// </summary>
        /// <param name="strEmail">Email à tester</param>
        /// <returns>Retourne true si l'email est valide.</returns>
        public static bool ValidEmail(string strEmail)
            => Regex.IsMatch(strEmail, RegexEmail, RegexOptions.IgnoreCase);

        /// <summary>
        ///     Recupere un nombre inconnu de chaines de caracteres et renvoie 'true' si aucune d'entre elle n'est vide
        /// </summary>
        /// <param name="argStrings">nombre inconnu de chaine de caracteres</param>
        /// <returns>true si aucune chaine vide, false sinon.</returns>
        public static bool CheckNullOrWhiteSpaceInputs(params string[] argStrings)
            => argStrings.All(s => !string.IsNullOrWhiteSpace(s));

        /// <summary>
        ///     Recupere un nombre inconnu de chaines de caracteres et renvoie 'true' si elles sont toutes identiques
        /// </summary>
        /// <param name="argStrings">nombre inconnu de chaine de caracteres</param>
        /// <returns>true si toutes les chaines sont identiques, false sinon.</returns>
        public static bool CheckEqualsInput(params string[] argStrings)
            => argStrings.Skip(1).All(s => string.Equals(argStrings[0].Trim(), s.Trim()));

        public static bool IsNumeric(string strNumber)
        {
            double n;
            return IsNumeric(strNumber, out n);
        }

        public static bool IsNumeric(string strNumber, out double iNumber)
            => double.TryParse(strNumber.Replace(".", ","), out iNumber);

        public static void IsNumeric(string strNumber, out int iNumber) => int.TryParse(strNumber, out iNumber);
    }
}