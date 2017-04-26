using System;
using System.Collections.Generic;

namespace GestAsso.Assets.Events
{
    public class Events
    {
        public string Title { get; set; }
        public string Theme { get; set; }
        public string Description { get; set; }
        public double EntryPrice { get; set; }
        public DateTime StartTime { get; set; }
        public List<object> FoodList { get; set; }
        public List<EventActivities> Activities { get; set; }
        public List<EventExternal> EventExternals { get; set; }
    }
}