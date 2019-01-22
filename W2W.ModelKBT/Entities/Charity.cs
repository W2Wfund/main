using KBTech.Lib.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W2W.ModelKBT.Entities
{
    [Type(Name = "Благотворительный_фонд")]
    public class Charity:BaseObject
    {
        [Property(Name = "Изображение")]
        public string Image { get; set; }

        [Property(Name = "НазваниеАнгл")]
        public string ObjectNameEng { get; set; }

        [Property(Name = "КомментарийАнгл")]
        public string CommentEng { get; set; }
    }
}
