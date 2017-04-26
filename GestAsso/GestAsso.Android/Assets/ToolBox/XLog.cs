using System;
using System.Collections.Generic;
using System.Linq;
using Android.OS;
using Android.Util;
using GestTool = GestAsso.Assets.ToolBox;

namespace GestAsso.Droid.Assets.ToolBox
{
    /// <summary>
    ///     Classe static qui permet de normaliser le systeme de log
    /// </summary>
    internal abstract class XLog : GestTool.XLog
    {
        public static List<GestTool.GestLog> Logs;

        public static void Write(LogPriority logPrio, string strMessage)
        {
            var xlog = new GestTool.GestLog(strMessage);
            if (Equals(Logs, null))
            {
                Logs = new List<GestTool.GestLog>();
            }
            Logs.Add(xlog);
            Log.WriteLine(logPrio, xlog.CallerMemberName, strMessage);
        }

        public static string GetLogs(List<GestTool.GestLog> listXadiaLog = null)
            => (listXadiaLog ?? Logs).Aggregate(GetPhoneInfos(), (current, log) => current + "\n" + log.GetItem());

        public static List<GestTool.GestLog> GetXadiaLogs(Predicate<GestTool.GestLog> conditionPredicate)
            => Logs.FindAll(conditionPredicate);

        private static string GetPhoneInfos() =>
            $"Android version : \"{Build.VERSION.Release}\".\n Téléphone : {Build.Brand.ToUpper()} {Build.Manufacturer} {Build.Model}\n\r";
    }

    
}