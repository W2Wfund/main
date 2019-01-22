using System;
using KBTech.Lib.Model.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W2W.ModelKBT.Entities
{
    [Type(Name = "Новость")]
    public class Article: BaseObject
    {
        [Property(Name = "Изображение")]
        public string Image { get; set; }

        [Property(Name = "Анонс")]
        public string Desc { get; set; }

        [Property(Name = "Содержание")]
        public string Text { get; set; }

        [Property(Name = "ДатаПубликации")]
        public DateTime? PublishDate { get; set; }

        [Property(Name = "ДатаЗавершения")]
        public DateTime? FinishDate { get; set; }

        [Property(Name = "НазваниеАнгл")]
        public string ObjectNameEng { get; set; }

        [Property(Name = "АнонсАнгл")]
        public string DescEng { get; set; }

        [Property(Name = "СодержаниеАнгл")]
        public string TextEng { get; set; }
    }
}
