using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReorderDatabase.Data.Options
{
    public class ApplicationOptions
    {
        internal ApplicationOptions()
        {
        }

        public string? ConnectionString { get; set; }
    }
}
