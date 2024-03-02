using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1
{
    /// <summary>
    /// Объявление класса установок
    /// </summary>
    public class Unit
    {
        public Unit(int id, string name, string description, int factoryId)
        {
            Id = id;
            Name = name;
            Description = description;
            FactoryId = factoryId;
        }

        public int Id { get; }

        public string Name { get; }

        public string Description { get; set; }

        public int FactoryId { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format("Идентификатор установки: {0};\nИмя установки: {1};\nОписание установки: {2};\nИдентификатор фабрики, к которой относится установка: {3}.",
                Id, Name, Description, FactoryId);
        }
    }
}
