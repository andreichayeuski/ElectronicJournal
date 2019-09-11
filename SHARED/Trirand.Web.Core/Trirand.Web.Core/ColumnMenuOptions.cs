using System;
using System.Collections.Generic;
using System.Text;

namespace Trirand.Web.Core.Trirand.Web.Core
{
    public class ColumnMenuOptions
    {
        public ColumnMenuOptions()
        {
            Columns = true;
            Sorting = true;
        }
        public bool Sorting { get; set; }
        public bool Columns { get; set; }
        public bool Filtering { get; set; }
        public bool Searching { get; set; }
        public bool Grouping { get; set; }
        public bool Freezing { get; set; }
    }
}
