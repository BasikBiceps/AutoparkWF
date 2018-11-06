using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Account
{
    [Serializable]
    public class Account
    {
        private static Car.CarManager carManager;
        private string firstName;
        private string secondName;
        private string login;
        private string password;

        public Account() : this("noname", "noname", "admin", "1111") { }

        static Account()
        {
            carManager = new CarManager("Kek2.dat");
        }

        public Account(Account acc)
        {
            this.firstName = acc.FirstName;
            this.secondName = acc.SecondName;
            this.login = acc.Login;
            this.password = acc.Password;
        }

        public Account(string firstName, string secondName, string login, string password)
        {
            this.firstName = firstName;
            this.secondName = secondName;
            this.login = login;
            this.password = password;
        }

        ~Account() { }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (isCorrectName(value))
                {
                    this.firstName = value;
                }
            }
        }

        public string SecondName
        {
            get { return secondName; }
            set
            {
                if (isCorrectName(value))
                {
                    this.secondName = value;
                }
            }
        }

        public string Login
        {
            get { return login; }
            set { this.login = value; }
        }

        public string Password
        {
            get { return password; }
            set { this.password = value; }
        }

        public CarManager CarManager
        {
            get { return carManager; }
            set { carManager = value; }
        }

        public bool isCorrectName(string name)
        {
            try
            {
                return name.All<char>(isCorrectSymbol);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("The string doesn't has reference.");
                return false;
            }
        }

        static public bool isCorrectSymbol(char symbol)
        {
            if (symbol >= 'A' && symbol <= 'Z')
            {
                return true;
            }
            else if (symbol >= 'a' && symbol <= 'z')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    [Serializable]
    public class Admin : Account
    {
        private static AccountManager accManager;

        public Admin(string firstName, string secondName, string login, string password, AccountManager accManager) : base(firstName, secondName, login, password)
        {
            Admin.accManager = accManager;
        }

        public Admin(Admin admin) : base(admin) { }

        static Admin()
        {
            accManager = null;
        }

        ~Admin() { }

        public AccountManager AccManager
        {
            get { return accManager; }
            set { accManager = value; }
        }

        public Car.Car AddCar(Car.Car car)
        {
            object obj = CarManager.Add(car);

            if (obj == null)
            {
                return null;
            }
            else {
                Car.Car tempCar = (Car.Car)obj;
                return tempCar;
            }
        }

        public void removeCar(string number)
        {
            CarManager.Remove(number);
        }

        public Account addAccount(Account account)
        {
            object acc = accManager.Add(account);

            if (acc == null) {
                return null;
            }
            return acc as Account;
        }

        public void removeAccount(string login)
        {
            accManager.Remove(login);
        }
    }

    [Serializable]
    public class User : Account
    {
        private Car.Car car;
        private int money;

        public User() : base()
        {
            car = new Car.Car();
            money = 0;
        }

        public User(string firstName, string secondName, string login, string password) : base(firstName, secondName, login, password)
        {
            car = new Car.Car();
            money = 0;
        }

        public User(string firstName, string secondName, string login, string password, Car.Car car) : base(firstName, secondName, login, password)
        {
            this.car = new Car.Car(car);
            money = 0;
        }

        public User(User user) : base(user)
        {
            car = new Car.Car(user.Car);
            money = user.Money;
        }

        public Car.Car Car
        {
            get { return car; }
            set
            {
                car = value;
                car.IsFree = false;
            }
        }

        public int Money
        {
            get { return money; }
            set { money = value; }
        }
    }

    public class AccountManagerEventArgs : EventArgs {
        public string Messege {
            get;
            set;
        }

        public Account Account {
            get;
            set;
        }

        public AccountManagerEventArgs(string messege) {
            Messege = messege;
        }

        public AccountManagerEventArgs(string messege, Account acc)
        {
            Messege = messege;
            Account = acc;
        }

        public AccountManagerEventArgs(Account acc) {
            Account = acc;
        }
    }

    public class AccountManager : IManager
    {
        public delegate void AccountManagerHandler(object sender, EventArgs e);

        public event AccountManagerHandler Added;
        public event AccountManagerHandler Removed;

        private List<Account> accountList = new List<Account>(0);
        private BinaryFormatter formatter;
        private string fileName;


        public AccountManager(string fileName)
        {
            this.fileName = fileName;
            formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(this.fileName, FileMode.OpenOrCreate))
            {
                try
                {
                    accountList.AddRange((Account[])formatter.Deserialize(fs));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        ~AccountManager()
        {
            SaveInformation();
        }

        public void Update()
        {
            foreach (var el in accountList)
            {
                if (el as User != null)
                {
                    User tempUser = (User)el;

                    if (tempUser.CarManager.Find(tempUser.Car.Number) != null)
                    {
                        tempUser.Car = (Car.Car)tempUser.CarManager.Find(tempUser.Car.Number);
                    }
                }
            }
        }

        public void SaveInformation()
        {
            using (FileStream fs = new FileStream(this.fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, accountList.ToArray());
            }
        }

        public object Find(object number)
        {
            if (number as string == null)
            {
                return null;
            }

            string tempNumber = (string)number;

            foreach (var el in accountList)
            {
                if (el as User != null)
                {
                    User tempUser = (User)el;

                    if (tempUser.Car.Number.Equals(tempNumber))
                    {
                        return tempUser;
                    }
                }
            }
            return null;
        }

        public object Add(object account)
        {
            if (account as Account == null)
            {
                return null;
            }

            Account tempAccount = (Account)account;

            foreach (var el in accountList)
            {
                if (el.Login.Equals(tempAccount.Login))
                {
                    return null;
                }
            }
            accountList.Add(tempAccount);
            Added?.Invoke(this, new AccountManagerEventArgs(tempAccount));
            return tempAccount;
        }

        public void Remove(object login)
        {
            if (login as string == null)
            {
                return;
            }

            string tempLogim = (string)login;

            foreach (var el in accountList)
            {
                if (el.Login.Equals(tempLogim))
                {
                    accountList.Remove(el);
                    Removed?.Invoke(this, new AccountManagerEventArgs("Account was removed."));
                    return;
                }
            }
            Removed?.Invoke(this, new AccountManagerEventArgs("Account wasn't removed."));
        }

        public Account findAccount(string login)
        {
            foreach (var el in accountList)
            {
                if (el.Login.Equals(login))
                {
                    return el;
                }
            }
            return null;
        }

        public List<Account> AccountList
        {
            get { return accountList; }
        }

        public Account this[int index]
        {
            get { return accountList.ElementAtOrDefault(index); }
            set
            {
                Account account = accountList.ElementAt(index);
                account = value;
                account = null;
            }
        }

        public Account this[string login]
        {
            get
            {
                foreach (var el in accountList)
                {
                    if (el.Login.Equals(login))
                    {
                        return el;
                    }
                }
                return null;
            }
            set
            {
                Account acc;

                foreach (var el in accountList)
                {
                    if (el.Login.Equals(login))
                    {
                        acc = el;
                    }
                }
                acc = value;
            }
        }
    }
}