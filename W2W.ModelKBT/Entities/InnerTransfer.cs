using System;
using KBTech.Lib.Model.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W2W.ModelKBT.Entities
{
    [Type(Name = "ВнутреннийПеревод")]
    public class InnerTransfer : Payment
    {
        [Property(Name = "НазваниеСчета")]
        public string AccountName { get; set; }
    }
}
