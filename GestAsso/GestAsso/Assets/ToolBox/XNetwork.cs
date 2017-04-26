using System;
using System.Threading.Tasks;
using ModernHttpClient;
using System.Json;
using System.Net.Http;

namespace GestAsso.Assets.ToolBox
{
    /// <summary>
    ///     Classe static regroupant toutes les methodes liées au reseau
    /// </summary>
    public abstract class XNetwork
    {
        /// <summary>
        ///     Recupere une reponse Web et la transforme en objet Json
        /// </summary>
        /// <param name="url">String</param>
        /// <returns>Objet Json</returns>
        public static async Task<JsonValue> GetJsonFromWeb(string url)
        {
            var httpClient = new HttpClient(new NativeMessageHandler()) { BaseAddress = new Uri(url) };
            using (var response = httpClient.GetStreamAsync(new Uri(url)).Result)
            {
                return await Task.Run(() => JsonValue.Load(response));
            }
        }
    }
}