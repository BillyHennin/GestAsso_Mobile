using System;
using System.Collections.Generic;

namespace GestAsso.Assets.ToolBox
{
    public abstract class XAction
    {
        /// <summary>
        ///     Liste d'action à effectuer
        /// </summary>
        protected static List<ActionStack> ActionsToExecute;

        /// <summary>
        ///     Enleve tous les elements de la pile qui correspondent à la méthode passée en parametre.
        ///     Si aucun parametre n'est utilisé, la pile est entierement vidée
        /// </summary>
        /// <param name="action">Fonction à supprimer de la pile</param>
        public static void RemoveFromStackAll(Action<object> action = null)
        {
            if (action == null)
            {
                ActionsToExecute.Clear();
            }
            else
            {
                ActionsToExecute.RemoveAll(x => x.Method == action);
            }
        }

        /// <summary>
        ///     Enleve le premier element de la pile qui correspond à la méthode passée en parametre
        /// </summary>
        /// <param name="action">Fonction à supprimer de la pile</param>
        public static void RemoveFromStack(Action<object> action)
            => ActionsToExecute.Remove(ActionsToExecute.Find(x => x.Method == action));

        /// <summary>
        ///     Enleve le dernier element de la pile
        /// </summary>
        public static void RemoveLast()
            => ActionsToExecute.RemoveAt(ActionsToExecute.Count - 1);

        public static void RemoveAllFromStack()
            => ActionsToExecute.RemoveAll(method => method.BToRemove);
    }

    public class ActionStack
    {
        public readonly Action<object> Method;
        public bool BToRemove;

        public ActionStack(Action<object> method)
        {
            Method = method;
        }

        public virtual void Execute() { }
    }
}