using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBLib.Model.Page
{
    public class Pager
    {
        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 10;

    }
}
