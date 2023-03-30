using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.ViewModels.Content
{
    public class ReportVm
    {
        public int Id { get; set; }

        public int? KnowledgeBaseId { get; set; }

        public string Content { get; set; }

        public string ReportUserId { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public bool IsProcessed { get; set; }

        public string Type { get; set; }
    }
}
