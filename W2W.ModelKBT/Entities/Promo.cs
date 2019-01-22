using System;
using KBTech.Lib.Model.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W2W.ModelKBT.Entities
{
    [Type(Name = "Рекламный_материал")]
    public class Promo : BaseObject
    {
        [Property(Name = "Изображение")]
        public string Image { get; set; }

        [Property(Name = "Путь")]
        public string DownloadFile { get; set; }

        [Property(Name = "НазваниеАнгл")]
        public string ObjectNameEng { get; set; }

        [Property(Name = "КомментарийАнгл")]
        public string CommentEng { get; set; }
    }
}
