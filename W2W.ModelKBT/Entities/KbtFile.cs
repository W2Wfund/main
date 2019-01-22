using System;
using KBTech.Lib.Model.Attributes;

namespace W2W.ModelKBT.Entities
{
    [Type(Name = "Файл")]
    public class KbtFile: BaseObject
    {
        [Property(Name = "Изображение")]
        public string Image { get; set; }

        [Property(Name = "Путь")]
        public string Path { get; set; }
    }
}
