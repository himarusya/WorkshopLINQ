using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace lab3
{
    class Program : IDisposable
    {
        static WorkshopContext db;

        static void Main(string[] args)
        {
            int caseSwitch;
            do
            {
                Console.WriteLine("Введите номер запроса:");
                Console.WriteLine("1. Выборка всех данных из таблицы, стоящая в схеме базы данных на стороне отношения «один» ");
                Console.WriteLine("2. Выборка данных из таблицы, стоящая в схеме базы данных нас стороне отношения «один», отфильтрованные по определенному условию, налагающему ограничения на одно или несколько полей");
                Console.WriteLine("3. Выборка данных, сгруппированных по любому из полей данных с выводом какого-либо итогового результата (min, max, avg, сount или др.) по выбранному полю из таблицы, стоящая в схеме базы данных нас стороне отношения «многие»");
                Console.WriteLine("4. Выборка данных из двух полей двух таблиц, связанных между собой отношением «один-ко-многим»");
                Console.WriteLine("5. Выборкa данных из двух таблиц, связанных между собой отношением «один-ко-многим» и отфильтрованным по некоторому условию, налагающему ограничения на значения одного или нескольких полей");
                Console.WriteLine("6. Вставка данных в таблицу, стоящая на стороне отношения «Один» ");
                Console.WriteLine("7. Вставка данных в таблицу, стоящая на стороне отношения «Многие» ");
                Console.WriteLine("8. Удаление данных из таблицы, стоящая на стороне отношения «Один» ");
                Console.WriteLine("9. Удаление данных из таблицы, стоящая на стороне отношения «Многие» ");
                Console.WriteLine("10. Обновление удовлетворяющих определенному условию записей в любой из таблиц базы данных ");
                caseSwitch = Convert.ToInt32(Console.ReadLine());

                switch (caseSwitch)
                {
                    case 1:
                        Console.Clear();
                        PrintOwners();
                        break;
                    case 2:
                        Console.Clear();
                        PrintCarsOnModel("BMW");
                        break;
                    case 3:
                        Console.Clear();
                        PrintCarsCountOnColor();
                        break;
                    case 4:
                        Console.Clear();
                        PrintCarsWithOwners();
                        break;
                    case 5:
                        Console.Clear();
                        PrintYellowCarsWithOwners();
                        break;
                    case 6:
                        Console.Clear();
                        Insert1();
                        break;
                    case 7:
                        Console.Clear();
                        Insert2();
                        break;
                    case 8:
                        Console.Clear();
                        Delete1();
                        break;
                    case 9:
                        Console.Clear();
                        Delete2();
                        break;
                    case 10:
                        Console.Clear();
                        Update();
                        break;
                    default:
                        break;
                }

            } while (caseSwitch != 0);
        }

        static Program()
        {
            db = new WorkshopContext();
        }

        static void Print(IEnumerable list)
        {
            foreach(var item in list)
            {
                Console.WriteLine(item);
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
            
        //1
        static void PrintOwners()
        {
            Print(db.Owners.ToList());
        }

        //2
        static void PrintCarsOnModel(string model)
        {
            Print(db.Cars.Where(p => p.Model == model).ToList());
        }

        //3
        static void PrintCarsCountOnColor()
        {
            Console.WriteLine("Количество автомобилей жёлтого цвета {0}", db.Cars.Where(p => p.Colour == "Yellow")
                .ToList().Count());
        }

        //4
        static void PrintCarsWithOwners()
        {
            var res = db.Cars.Join(db.Owners, f => f.OwnerId, t => t.OwnerId,
                (f, t) => new { f.Model, t.FioOwner }
            );
            Print(res);
        }
                
        //5
        static void PrintYellowCarsWithOwners()
        {
            var res = db.Cars.Where(p => p.Colour == "Yellow").Join(db.Owners, f => f.OwnerId, t => t.OwnerId,
                (f, t) => new { f.CarId, t.FioOwner, f.Model, f.Colour, f.YearOfIssue, f.BodyNumber }
            );
            Print(res);
        }

        //6
        static void Insert1()
        {
            db.Owners.Add(new Owner()
                {
                    Adress = "sdfsdf",
                    DriverLicense = 232424,
                    FioOwner = "qwewr",
                    Phone = 1234567
                }
            );
            db.SaveChanges();
            Console.WriteLine("Запись добавлена!");
        }

        //7
        static void Insert2()
        {
            var zp = db.Mechanics.First();
            var tp = db.Cars.First();
            db.Workroom.Add(
                new Workroom()
                {
                    CarId = tp.CarId,
                    MechanicId = zp.MechanicId,
                    Cost = 500,
                    ReceiptDate = new DateTime(2018, 10, 9)
                }
            );
            db.SaveChanges();
            Console.WriteLine("Запись добавлена!");
        }

        //8
        static void Delete1()
        {
            var tmp = db.Mechanics.First();
            db.Workroom.RemoveRange(db.Workroom.Where(p => p.MechanicId == tmp.MechanicId));
            db.Mechanics.Remove(tmp);
            db.SaveChanges();
            Console.WriteLine("Запись удалена!");
        }

        //9
        static void Delete2()
        {
            db.Workroom.Remove(db.Workroom.First());
            Console.WriteLine("Запись удалена!");
        }

        //10
        static void Update()
        {
            var tmp = db.Mechanics.First();
            tmp.Experience = 555;
            tmp.FioMechanic = "AAAAAAAAAAAAAAAAAA";
            tmp.Qualification = "sdfsdf";
            db.Mechanics.Update(tmp);
            Console.WriteLine("Запись обновлена!");
        }
    }
}
