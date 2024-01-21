using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppExam.Business.ViewModels.Commons;

namespace WebAppExam.Business.ViewModels.Service
{
    public class UpdateServiceVM : BaseEntityVM
    {
        public string IconUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SubTitle { get; set; }
    }
}
