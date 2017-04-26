using System;
using System.Collections.Generic;
using System.Json;
using System.Threading.Tasks;
using GestAsso.Assets.Users;
using Newtonsoft.Json;

namespace GestAsso.Assets.ToolBox
{
    public abstract class XTools
    {
        /// <summary>
        ///     Objet UserProfile qui sera utilisé dans tout le cycle de vie de l'application
        /// </summary>
        public static UserProfile User;

        /// <summary>
        ///     Affiche un message d'attente pendant tout le temps ou l'action passée en parametre s'effectue
        /// </summary>
        /// <param name="action">Action à effectuer</param>
        /// <returns>Retourne un objet Task</returns>
        protected static async Task ProtectedAwaitAction(object action)
        {
            //Determiner le type d'execution en fonction du type de l'objet action
            if (action.GetType() == typeof(Action<object>))
            {
                await Task.Run(delegate { ((Action<object>) action).Invoke(null); });
            }
            else if (action.GetType() == typeof(Func<Task>))
            {
                await Task.Run(async delegate { await ((Func<Task>) action)(); });
            }
        }

        public static Dictionary<string, string> JsonToDict(JsonValue json)
            => JsonToDict(json.ToString());

        public static Dictionary<string, string> JsonToDict(string strJson)
            => JsonConvert.DeserializeObject<Dictionary<string, string>>(strJson);
    }
}