using KBTech.Lib.Model.Attributes;
using System;

namespace W2W.ModelKBT.Entities
{
    [Type(Name = "Уведомление")]
    public class Notice:BaseObject
    {
        [Property(Name = "Прочитано")]
        public bool? IsReaded { get; set; }

        [Property(Name = "СсылкаНаКонтрагента")]
        public uint? PartnerId { get; set; }
    }
}
