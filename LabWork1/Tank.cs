using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1
{
    /// <summary>
    /// Объявление класса резервуаров
    /// </summary>
    public class Tank
    {
        public Tank(int id, string name, string description, int volume, int maxVolume, int unitId)
        {
            Id = id;
            Name = name;
            Description = description;
            Volume = volume;
            MaxVolume = maxVolume;
            UnitId = unitId;
        }

        public int Id { get; }

        public string Name { get; }

        public string Description { get; set; }

        public int Volume { get; set; }

        public int MaxVolume { get; set; }

        public int UnitId { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format(
                "Идентификатор резервуара: {0};\nИмя резервуара: {1};\nОписание резервуара: {2};\nОбъём резервуара: {3};\nМаксимальное значение объёма резервуара: {4};\nИдентификатор установки, к которой относится резервуар: {5}.",
                Id, Name, Description, Volume, MaxVolume, UnitId);
        }
    }
}
