using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.ViewModels.Content
{
    public class VoteCreateRequest
    {
        public int KnowledgeBaseId { get; set; }
        public string UserId { get; set; }
    }
}
