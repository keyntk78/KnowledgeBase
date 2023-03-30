using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.ViewModels.Systems
{
    public class FunctionCreateRequest
    {

        public string Id { get; set; }

        public string Name { get; set; }


        public string Url { get; set; }


        public int SortOrder { get; set; }

        public string? ParentId { get; set; }
    }
}
