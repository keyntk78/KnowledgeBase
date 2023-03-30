using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.ViewModels.Content
{
    public class CommentVm
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int KnowledgeBaseId { get; set; }

        public string OwnwerUserId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}
