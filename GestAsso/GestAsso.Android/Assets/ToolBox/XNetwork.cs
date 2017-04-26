using System.Json;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Android.App;
using Android.Graphics;
using Android.Net;
using Android.Util;
using ModernHttpClient;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using GestTool = GestAsso.Assets.ToolBox;

namespace GestAsso.Droid.Assets.ToolBox
{
    /// <summary>
    ///     Classe static regroupant toutes les methodes liées au reseau
    /// </summary>
    internal abstract class XNetwork : GestTool.XNetwork
    {
        /// <summary>
        ///     Permet de verifier que l'hote est joignable
        /// </summary>
        /// <param name="hostNameOrAddress">Nom de l'hote ou adress IP que l'on souhaite tester</param>
        /// <returns>Retourne true si l'hote est joinable</returns>
        public static bool PingRequest(string hostNameOrAddress)
            => new Ping().Send(hostNameOrAddress).Status == IPStatus.Success;

        /// <summary>
        ///     Permet de verifier que l'hote est joignable et affiche un message le cas contraire
        /// </summary>
        /// <param name="hostNameOrAddress">Nom de l'hote ou adress IP que l'on souhaite tester</param>
        /// <param name="idMessage">Identifiant de la ressource qui sera affichée en cas d'erreur</param>
        /// <param name="activity">Activitée en cours</param>
        /// <returns>Retourne true si l'hote est joinable</returns>
        public static bool PingRequest(string hostNameOrAddress, int idMessage, Activity activity = null)
        {
            var bPing = PingRequest(hostNameOrAddress);
            if (!bPing)
            {
                XMessage.ShowError(idMessage, activity);
            }
            return bPing;
        }

        /// <summary>
        ///     Verifie si l'appareil est connecté à Internet.
        ///     Necessite la permission de l'appareil.
        ///     Affiche un message en cas d'erreur.
        /// </summary>
        /// <param name="activity">Activitée en cours</param>
        /// <returns>Retourne true si l'appareil est connecté à Internet.</returns>
        public static bool CheckNetwork(Activity activity = null)
        {
            var atvi = XTools.GetActivity(activity);
            if (((ConnectivityManager) atvi.GetSystemService("connectivity")).ActiveNetworkInfo.IsConnected)
            {
                return true;
            }
            XMessage.ShowError(Resource.String.ErrorNetwork, atvi);
            return false;
        }

        /// <summary>
        ///     Permet de recuperer un objet de type Bitmap via l'url d'une image
        /// </summary>
        /// <param name="url">url de l'image</param>
        /// <returns>Image du type Bitmap</returns>
        public static Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;
            using (var httpClient = new HttpClient(new NativeMessageHandler()))
            {
                var imageBytes = httpClient.GetByteArrayAsync(url).Result;
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }
            return imageBitmap;
        }

        /// <summary>
        ///     Fonction appelée lorsque l'état de connection change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ConnectivityChangedEvent(object sender, ConnectivityChangedEventArgs e)
        {
            var isConnected = CrossConnectivity.Current.IsConnected;
            var strMessage = "La connection à été";
            if (isConnected)
            {
                strMessage += "retrouvée.";
                //Affiche un message en bas de l'écran indiquant que la connection est revenue
                XUi.SetSnackMessage(strMessage);
                //Effectuer toutes les actions mise en attente
                XAction.ExecuteStack();
            }
            else
            {
                strMessage += "perdue.";
                XMessage.ShowNotification(strMessage, "Toute action sera effectuée à la reprise de la connection");
            }
            XLog.Write(LogPriority.Info, strMessage);
        }
    }
}