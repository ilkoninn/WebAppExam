using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppExam.Core.Entities.Commons;

namespace WebAppExam.Core.Entities
{
    public class Service : BaseAuditableEntity
    {
        public string IconUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SubTitle { get; set; }
    }
}
