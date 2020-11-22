using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class OutputData
    {
        public List<int[]> Fields { get; set; }
        public OutputData(List<int[]> fields)
        {
            Fields = fields;
        }
    }
}
