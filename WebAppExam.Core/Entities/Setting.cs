using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppExam.Core.Entities.Commons;

namespace WebAppExam.Core.Entities
{
    public class Setting : BaseAuditableEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
