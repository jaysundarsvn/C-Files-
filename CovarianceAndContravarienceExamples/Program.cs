using System;
using static CovarianceAndConravarienceExamples.Program;
using System.IO;
using System.Linq;

namespace CovarianceAndConravarienceExamples
{
    class Program
    {
        delegate Car CarFactoryDel(int ID, string name);
        delegate void LogICECarDetails(ICECar car);
        delegate void LogEVCarDetails(EVCar car);

        public static void Main(string[] args)
        {
            CarFactoryDel carFactoryDel = CarFactory.ReturnICECar;  

            Car iceCar = carFactoryDel(1, "Sian");

            //Console.WriteLine($"Object type : {iceCar.GetType()}");
            //Console.WriteLine($"Car Details : {iceCar.GetCarDetails()}");

            carFactoryDel = CarFactory.ReturnEVCar;

            Car evCar = carFactoryDel(2,"Altroz");

            Console.WriteLine();

            LogICECarDetails logICECarDetailsDel = LogCarDetails;
            logICECarDetailsDel(iceCar as ICECar);

            LogEVCarDetails logEVCarDetailsDel = LogCarDetails;
            logEVCarDetailsDel(evCar as EVCar); 

            //Console.WriteLine($"Object type : {evCar.GetType()}");
            //Console.WriteLine($"Car Details : {evCar.GetCarDetails()}");

            Console.ReadKey();
        }

        static void LogCarDetails(Car car)
        {
            if(car is ICECar)
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ICEDetails.txt"), true))
                {
                    sw.WriteLine($"Object type : {car.GetType()}");
                    sw.WriteLine($"Car Details : {car.GetCarDetails()}");
                }
            }

            else if(car is EVCar)
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EVDetails.txt"), true))
                {
                    sw.WriteLine($"Object type : {car.GetType()}");
                    sw.WriteLine($"Car Details : {car.GetCarDetails()}");
                }
            }

            else
            {
                throw new ArgumentException();
            }
        }
        public abstract class Car
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public virtual string GetCarDetails()
            {
                return $"{Id} - {Name}";
            }
        }

        public static class CarFactory
        {
            public static ICECar ReturnICECar(int ID, string name)
            {   
                return new ICECar { Id = ID, Name = name };
            }

            public static EVCar ReturnEVCar(int ID, string name)
            {
                return new EVCar { Id = ID, Name = name };
            }
        }

        public class ICECar : Car
        {
            public override string GetCarDetails()
            {
                return $"{base.GetCarDetails()} - Internal Combustion Engine";
            }
        }

        public class EVCar : Car
        {
            public override string GetCarDetails()
            {
                return $"{base.GetCarDetails()} - Electric motor";
            }
        }
    }
}
