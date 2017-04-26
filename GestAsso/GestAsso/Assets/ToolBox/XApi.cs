using System;
using System.Collections.Generic;
using GestAsso.Assets.Users;
using Newtonsoft.Json;

namespace GestAsso.Assets.ToolBox
{
    /// <summary>
    ///     Classe gerant tous les appels à l'API
    /// </summary>
    public abstract class XApi
    {
        //Url de l'API
        private const string StrApiUrl = "";

        /// <summary>
        ///     Envoie les informations necessaires pour l'authentification d'un utilisateur
        /// </summary>
        /// <param name="strEmail">Mail pour l'authentification</param>
        /// <param name="strPassword">Mot de passe pour l'authentification</param>
        /// <returns>Retourne un utilisateur connecté ou un objet null si l'authentification à echouée</returns>
        public static UserProfile ApiConnect(string strEmail, string strPassword)
            => new UserProfile(XTools.JsonToDict(XNetwork
                .GetJsonFromWeb($"{StrApiUrl}/login/{strEmail}/{strPassword}").Result));

        /// <summary>
        ///     Envoie les informations necessaires pour l'authentification d'un utilisateur
        /// </summary>
        /// <param name="dict">Dictionnaire contenant une entrée "Email" et "Password"</param>
        /// <returns>Retourne un utilisateur connecté ou un objet null si l'authentification à echouée</returns>;
        public static UserProfile ApiConnect(IReadOnlyDictionary<string, string> dict)
            => ApiConnect(dict["Email"], dict["Password"]);

        /// <summary>
        ///     Met à jour un utilisateur 
        /// </summary>
        /// <param name="idUser">Guid de l'utilisateur à mettre à jour</param>
        /// <param name="strUpdateType">Type de mise à jour (Mail, Role, Mot de passe, ...) </param>
        /// <param name="newValue">Nouvelle valeur</param>
        /// <returns>Renvoie un boolean correspondant au succes de la mise a jour</returns>
        public static bool UpdateUser(Guid idUser, string strUpdateType, object newValue)
            => XNetwork.GetJsonFromWeb($"{StrApiUrl}/User/{idUser}/Update/{strUpdateType}/{newValue}").Result["Result"];

        public static bool RegisterVerifyEmail(string strEmail)
            => XNetwork.GetJsonFromWeb($"{StrApiUrl}/Register/VerifyEmail/{strEmail}").Result["Result"];

        public static UserProfile RegisterNewUser(IReadOnlyDictionary<string, object> dict) 
            => RegisterNewUser(dict["Email"], dict["Password"], dict["FirstName"], dict["LastName"], dict["Role"]);

        public static UserProfile RegisterNewUser(object email, object password, object firstName, object lastName, object role)
            => new UserProfile(XTools.JsonToDict(XNetwork
                .GetJsonFromWeb($"{StrApiUrl}/Register/Add/{email}/{password}/{firstName}/{lastName}/{role}").Result));
    }

}
