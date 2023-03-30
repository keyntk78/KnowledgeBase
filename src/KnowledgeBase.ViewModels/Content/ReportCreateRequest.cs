using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.ViewModels.Content
{
    public class ReportCreateRequest
    {
        public int? KnowledgeBaseId { get; set; }

        public int? CommentId { get; set; }

        public string Content { get; set; }

        public string ReportUserId { get; set; }
    }
}
