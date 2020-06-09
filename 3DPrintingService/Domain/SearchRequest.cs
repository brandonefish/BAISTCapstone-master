using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3DPrintingService.Domain
{
    public class SearchRequest
    {
        public string Description { get; set; }
        public string MultiPart { get; set; }

        public List<SearchRequest> Requests { get; }
    }
}
