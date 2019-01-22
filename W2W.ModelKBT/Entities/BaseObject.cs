using System;
using KBTech.Lib.Model.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W2W.ModelKBT.Entities
{
    [Type(Name = "object")]
    public class BaseObject
    {
        [Property(Name = "ДатаСоздания")]
        public DateTime? CreationDate { get; set; }

        [Property(Name = "Комментарий")]
        public string Comment { get; set; }

        [Property(Name = "Название")]
        public string ObjectName { get; set; }

        [Property(Name = "Сортировка.Позиция")]
        public int? SortPosition { get; set; }

        [Property(Name = "Сортировка.Ключ")]
        public string SortKey { get; set; }

        [Property(Name = "РедакторОбъекта")]
        public string Editor { get; set; }

        [Property(Name = "id_object")]
        public uint id_object { get; set; }

        [Property(Name = "id_parent")]
        public uint id_parent { get; set; }

        [Property(Name = "hash")]
        public string hash { get; set; }

        [Property(Name = "type")]
        public string type { get; set; }
    }
}
