using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Car
{
    public interface IManager
    {
        void SaveInformation();

        object Find(object obj);

        object Add(object obj);

        void Remove(object obj);
    }

    [Serializable]
    public class Car
    {
        private bool isFree;
        private string number;
        private string model;
        private int pricePerDay;

        public Car() : this("nonumber", "nomodel") { }

        public Car(string number, string model)
        {
            this.number = number;
            this.model = model;
            isFree = true;
            this.Days = 0;
            this.pricePerDay = 0;
        }

        public Car(string number, string model, int pricePerDay)
        {
            this.number = number;
            this.model = model;
            isFree = true;
            this.Days = 0;
            this.pricePerDay = pricePerDay;
        }

        public Car(Car car)
        {
            this.number = car.number;
            this.model = car.model;
            this.pricePerDay = car.pricePerDay;
            this.Days = car.Days;
            this.isFree = car.isFree;
        }

        ~Car() {}

        public int PricePerDay
        {
            get { return pricePerDay; }
            set { pricePerDay = value; }
        }

        public bool IsFree
        {
            get { return isFree; }
            set { isFree = value; }
        }

        public int Days
        {
            get;
            set;
        }

        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        public string Model
        {
            get { return model; }
            set { model = value; }
        }

    }

    public class CarAlreadyExistsException : Exception
    {
        public string Massage { get; set; }

        public CarAlreadyExistsException(string Massage)
        {
            this.Massage = Massage;
        }
        public CarAlreadyExistsException()
        {
            this.Massage = "Car already exist";
        }
    }

    public class CarIsNotFoundException : Exception
    {
        public string Massage
        {
            get;
            set;
        }

        public CarIsNotFoundException(string Massage)
        {
            this.Massage = Massage;
        }

        public CarIsNotFoundException()
        {
            this.Massage = "Car is not found";
        }
    }

    public class CarManagerEventArgs : EventArgs
    {
        public string Messege
        {
            get;
            set;
        }

        public Car Car
        {
            get;
            set;
        }

        public CarManagerEventArgs(Car car)
        {
            Car = car;
        }

        public CarManagerEventArgs(string messege, Car car)
        {
            Messege = messege;
            Car = car;
        }

        public CarManagerEventArgs(string messege)
        {
            Messege = messege;
        }
    }

    public class CarManager : IManager
    {
        public delegate void CarManagerHandler(object sender, EventArgs e);

        public event CarManagerHandler Added;
        public event CarManagerHandler Removed;

        private List<Car> carList = new List<Car>(0);
        private BinaryFormatter formatter;
        private string fileName;

        public CarManager(string fileName)
        {
            this.fileName = fileName;
            formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                try
                {
                    carList.AddRange((Car[])formatter.Deserialize(fs));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void SaveInformation()
        {
            using (FileStream fs = new FileStream(this.fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, carList.ToArray());
            }
        }

        ~CarManager()
        {
            SaveInformation();
        }

        public List<Car> CarList
        {
            get { return carList; }
        }

        public object Add(object car)
        {
            if (car as Car == null)
            {
                return null;
            }
            Car temp = (Car)car;

            foreach (var el in carList)
            {
                if (string.Equals(el.Number, temp.Number))
                {
                    throw new CarAlreadyExistsException();
                }
            }
            carList.Add(temp);
            Added?.Invoke(this, new CarManagerEventArgs(temp));
            return temp;
        }

        public object Find(object number)
        {
            if (number as string == null) {
                return null;
            }

            string tempNumber = (string)number;

            foreach (var el in carList)
            {
                if (el.Number.Equals(tempNumber))
                {
                    return el;
                }
            }
            return null;
        }

        public void Remove(object number)
        {
            if (number as string == null) {
                return;
            }

            string tempNumber = (string)number;

            bool flag = true;

            foreach (var el in carList)
            {
                if (string.Equals(el.Number, tempNumber))
                {
                    carList.Remove(el);
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                Removed?.Invoke(this, new CarManagerEventArgs("Car wasn't deleted."));
                throw new CarIsNotFoundException();
            }
            Removed?.Invoke(this, new CarManagerEventArgs("Car was deleted."));
        }

        public Car this[int index]
        {
            get { return CarList.ElementAtOrDefault(index); }
            set
            {
                Car car = CarList.ElementAt(index);
                car = value;
            }
        }
    }
}
