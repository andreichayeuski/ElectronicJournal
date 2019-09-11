using System;

namespace SHARED.Models
{
    [Serializable]
    public class FullCalendarItem
    {
        public string id { get; set; }

        public string title { get; set; }

        public string tooltip { get; set; }

        public bool allDay { get; set; }

        public string start { get; set; }

        public string end { get; set; }

        public string url { get; set; }

        public string className { get; set; }

        public bool editable { get; set; }

        //public string color { get; set; }

        public string backgroundColor { get; set; }

        //public string borderColor { get; set; }

        public string textColor { get; set; }
        public bool overlap { get; set; }
        public string rendering { get; set; }
    }
}
