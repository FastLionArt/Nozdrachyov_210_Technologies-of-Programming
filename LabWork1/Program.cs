using System.Threading.Tasks;

namespace LabWork1
{
    class Program
    {
        static void Main(string[] args)
        {
            var factories = GetFactories();
            var tanks = GetTanks();
            var units = GetUnits();

            Console.WriteLine("Список заводов:");
            PrintObjectArray(factories);
            Console.WriteLine("\n");

            Console.WriteLine("Список установок:");
            PrintObjectArray(units);
            Console.WriteLine("\n");

            Console.WriteLine("Список резервуаров:");
            PrintObjectArray(tanks);
            

            while (true)
            {
                Console.WriteLine("\n");
                Console.WriteLine("1 - вывести сумму текущего объёма всех резервуаров;");
                Console.WriteLine("2 - найти фабрику по установке;");
                Console.WriteLine("3 - найти установку по названию резервуара;");
                Console.WriteLine("4 - вывести информацию обо всех резервуарах, включая название фабрики и установки");
                Console.WriteLine("0 - выход");
                Console.Write("Введите, что хотите сделать >>> ");
                var input = Console.ReadLine();
                if (!int.TryParse(input, out var option))
                {
                    Console.WriteLine("Вы ошиблись при вводе, введите номер");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        {
                            Console.WriteLine("\n");
                            var totalVolume = GetTotalVolume(tanks);
                            if (totalVolume.HasValue)
                            {
                                Console.WriteLine($"Сумма текущего объёма всех резервуаров: {totalVolume}");
                            }
                            else
                            {
                                Console.WriteLine("\nМассив пуст");
                            }

                            Console.WriteLine("\n");
                            continue;
                        }
                    case 2:
                        {
                            Console.WriteLine("\n");
                            Console.WriteLine("Найти фабрику по установке: ");
                            Console.Write("Введите номер установки >>> ");
                            string? unitIdString = Console.ReadLine();
                            Console.WriteLine("\n");
                            if (!int.TryParse(unitIdString, out var unitId))
                            {
                                Console.WriteLine("Вы ошиблись при вводе, введите номер");
                                continue;
                            }

                            if (unitId-1 > units.Length)
                            {
                                Console.WriteLine("Такой установки нет");
                                continue;
                            }

                            Console.WriteLine("Установка, которую ищем: ");
                            Console.WriteLine(units[unitId-1].ToString() + "\n");
                            var factory = FindFactory(factories, units[unitId-1]);
                            if (factory != null)
                            {
                                Console.WriteLine("Найденная фабрика: ");
                                Console.WriteLine(factory.ToString());
                            }
                            else
                            {
                                Console.WriteLine("Такой фабрики нет");
                            }

                            Console.WriteLine("\n");
                            continue;
                        }
                    case 3:
                        {
                            Console.WriteLine("\n");
                            Console.WriteLine("Найти установку по названию резервуара: ");
                            Console.Write("Введите название резервуара, мы найдем вам установку >>> ");
                            string? tankName1 = Console.ReadLine();
                            if (tankName1 == null)
                            {
                                Console.WriteLine("А вы что-то вводили? Кажется было получено пустое значение");
                                continue;
                            }

                            var unit1 = FindUnit(units, tanks, tankName1);
                            if (unit1 == null)
                            {
                                Console.WriteLine($"Резервуар {tankName1} не был найден.\n");
                            }
                            else
                            {
                                Console.WriteLine($"Резезвуар {tankName1} относится к установке с идентификатором {unit1.Id}\n");
                                Console.WriteLine("Информация об установке:");
                                Console.WriteLine(unit1.ToString());
                            }

                            continue;
                        }
                    case 4:
                        {
                            Console.WriteLine("\n");
                            Console.WriteLine("Информация о каждом резервуаре, включая название установки и фабрики: ");
                            Console.WriteLine("\n");
                            foreach (var tank in tanks)
                            {
                                var unit = FindUnit(units, tanks, tank.Name);
                                if (unit == null)
                                {
                                    continue;
                                }

                                var factory = FindFactory(factories, unit);
                                if (factory == null)
                                {
                                    continue;
                                }

                                Console.WriteLine(tank.ToString());
                                Console.WriteLine($"Имя установки: {unit.Name}; Имя фабрики: {factory.Name}. \n");
                            }

                            Console.WriteLine("\n");
                            continue;
                        }
                    case 0:
                        Console.WriteLine("Всего доброго");
                        break;
                    default:
                        Console.WriteLine("Такого выбора нет...");
                        continue;
                }

                break;
            }
        }

        /// <summary>
        /// создает массив Tanks с определенными объектами (по условию)
        /// </summary>
        /// 
        /// <returns>
        /// массив
        /// </returns>
        public static Tank[] GetTanks()
        {
            Tank[] tanks =
            {
                new(1, "Резервуар 1", "Надземный - вертикальный", 1500, 2000, 1),
                new(2, "Резервуар 2", "Надземный - горизонтальный", 2500, 3000, 1),
                new(3, "Дополнительный резервуар 24", "Надземный - горизонтальный", 3000, 2000, 2),
                new(4, "Резервуар 35", "Надземный - вертикальный", 3000, 3000, 2),
                new(5, "Резервуар 47", "Надземный - двуственный", 4000, 5000, 2),
                new(6, "Резервуар 256", "Подводный", 500, 500, 3),
            };
            return tanks;
        }

        /// <summary>
        /// создает массив Units с определенными объектами (по условию)
        /// </summary>
        /// 
        /// <returns>
        /// массив
        /// </returns>
        public static Unit[] GetUnits()
        {
            Unit[] units =
            {
                new(1, "ГФУ-2", "Газофракционирующая установка", 1),
                new(2, "АВТ-6", "Атмосферно-вакуумная трубчатка", 1),
                new(3, "АВТ-10", "Атмосферно-вакуумная трубчатка", 2),
            };
            return units;
        }

        /// <summary>
        /// создает массив Factory с определенными объектами (по условию)
        /// </summary>
        /// 
        /// <returns>
        /// массив
        /// </returns>
        public static Factory[] GetFactories()
        {
            Factory[] factories =
            {
                new(1, "НПЗ№1", "Первый нефтеперерабатывающий завод"),
                new(2, "НПЗ№2", "Вторый нефтеперерабатывающий завод"),
            };
            return factories;
        }

        /// <summary>
        /// печатает массив типа T в консоли
        /// </summary>
        /// <param name="array">передаётся массив, который нужно вывести</param>
        /// <typeparam name="T">тип Массив</typeparam>
        public static void PrintObjectArray<T>(T[] array)
        {
            if (array.Length == 0)
            {
                return;
            }

            foreach (var item in array)
            {
                if (item != null)
                {
                    Console.WriteLine(item.ToString());
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// находит установку в массиве Units по названию резевуара
        /// </summary>
        /// <param name="units">передаётся массив, в котором нужно найти искомую установку</param>
        /// <param name="tanks">передаётся массив, в котором находится резервуар</param>
        /// <param name="tankName">передаётся имя резервуара, которое требуется найти</param>
        /// <returns>найденный объект Unit или null, если такого мы не нашли</returns>
        public static Unit? FindUnit(Unit[] units, Tank[] tanks, string tankName)
        {
            var tank = FindTank(tanks, tankName);
            if (tank == null)
            {
                return null;
            }

            foreach (var unit in units)
            {
                if (unit.Id == tank.UnitId)
                {
                    return unit;
                }
            }

            return null;
        }

        /// <summary>
        /// находит резервуар в массиве Tanks по названию резервуара
        /// </summary>
        /// <param name="tanks">массив, в котором нужно найти резервуар</param>
        /// <param name="tankName">имя резервуара, который требуется найти</param>
        /// <returns>найденный объект Tank или null, если мы не нашли такой резервуар</returns>
        public static Tank? FindTank(Tank[] tanks, string tankName)
        {
            foreach (var tank in tanks)
            {
                if (tank.Name == tankName)
                {
                    return tank;
                }
            }

            return null;
        }

        /// <summary>
        ///  находит фабрику в массиве фабрик по Unit'у
        /// </summary>
        /// <param name="factories">Массив фабрик (где и будем искать фабрику)</param>
        /// <param name="unit">Объект Unit,который требуется для поиска фабрики</param>
        /// <returns>найденный объект Factory или null, если мы не нашли фабрику</returns>
        public static Factory? FindFactory(Factory[] factories, Unit unit)
        {
            if (factories.Length == 0)
            {
                return null;
            }

            for (int i = 0; i < factories.Length; i++)
            {
                switch (unit.FactoryId == factories[i].Id)
                {
                    case true:
                        return factories[i];
                    case false:
                        continue;
                }
            }

            return null;
        }

        /// <summary>
        /// подсчёт суммы объёмов всех резервуаров
        /// </summary>
        /// <param name="tanks">массив с резервуарами, объём которых нужно сложить</param>
        /// <returns>сумма объёмов или null, если она равна нулю</returns>
        public static int? GetTotalVolume(Tank[] tanks)
        {
            if (tanks.Length == 0)
            {
                return null;
            }

            int res = 0;
            foreach (var tank in tanks)
            {
                res += tank.Volume;
            }

            return res;
        }
    }
}