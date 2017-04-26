using System;
using System.Runtime.CompilerServices;

namespace GestAsso.Assets.ToolBox
{
    public abstract class XLog { }

    public class GestLog
    {
        private readonly DateTime _date;
        private readonly string _message;
        private readonly int _callerLineNumber;
        private readonly string _callerFilePath;

        public readonly string CallerMemberName;

        public GestLog(string strMessage, [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            _date = DateTime.Now;
            _message = strMessage;
            CallerMemberName = callerMemberName;
            _callerFilePath = callerFilePath;
            _callerLineNumber = callerLineNumber;
        }

        public string GetItem()
            => $"{_date} - : From \"{CallerMemberName}\" - \"{_callerFilePath}\" - line {_callerLineNumber} \n Message : {_message} \n\r";
    }
}