using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Android.Util;
using Plugin.Connectivity;
using GestTool = GestAsso.Assets.ToolBox;

namespace GestAsso.Droid.Assets.ToolBox
{
    internal abstract class XAction : GestTool.XAction
    {
        /// <summary>
        ///     Initialise la liste d'action et active l'evenement de changement de connection
        /// </summary>
        public static void Init()
        {
            ActionsToExecute = new List<GestTool.ActionStack>();
            CrossConnectivity.Current.ConnectivityChanged += XNetwork.ConnectivityChangedEvent;
        }

        /// <summary>
        ///     Permet d'ajouter une fonction au stack d'attente
        /// </summary>
        /// <param name="action">Fonction à ajouter à la pile</param>
        public static void AddToStack(Action<object> action)
        {
            if (Equals(ActionsToExecute, null))
            {
                Init();
            }
            ActionsToExecute?.Add(new ActionStack(action));
        }

        /// <summary>
        ///     Effectue toutes les fonctions en attente
        /// </summary>
        public static void ExecuteStack()
        {
            foreach (var action in ActionsToExecute)
            {
                //On empeche les fonctions STRICTEMENT identiques de se lancer plusieurs fois.
                //Si une fonction apparait deux fois dans la liste, mais qu'elle possede des valeurs de parametres differents, elle sera executée.
                if (ActionsToExecute.Any(x => x.Method == action.Method))
                {
                    continue;
                }
                action.Execute();
            }
            //On enleve toutes les fonctions qui ont été effectuées de la liste
            RemoveAllFromStack();
        }
    }

    public class ActionStack : GestTool.ActionStack
    {
        public ActionStack(Action<object> method) : base(method) { }

        /// <summary>
        ///     Lance la fonction et indique si elle à été effectuée ou non.
        /// </summary>
        public override void Execute()
        {
            try
            {
                if (BToRemove)
                {
                    return;
                }
                Method.Invoke(null);
                BToRemove = true;
            }
            catch
            {
                var error =
                    $"La méthode '{Method.GetMethodInfo().Name}' a rencontrée une erreur lors de son execution.";
                XLog.Write(LogPriority.Error, error);
                XUi.SetSnackMessage(error);
            }
        }
    }
}