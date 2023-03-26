using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.ViewModels
{
    public class Panination <T>
    {
        public List<T> Items { get; set; }
        public int TotalRecords { get; set; }
    }
}
