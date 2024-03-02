using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1
{
    /// <summary>
    /// Объявление класса фабрик
    /// </summary>
    public class Factory
    {
        public Factory(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public int Id { get; }

        public string Name { get; }

        public string Description { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format("Идентификатор фабрики: {0};\nИмя фабрики: {1};\nОписание фабрики: {2}", Id, Name,
                Description);
        }
    }
}
