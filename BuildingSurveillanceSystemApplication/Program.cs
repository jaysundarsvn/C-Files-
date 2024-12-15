using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime;

//https://chat.openai.com/share/5c0f4f99-f0ea-4eb5-86fd-c52c76794ade

namespace BuildingSurveillanceSystemApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            //creating obj of SecuritySurveillanceHub
            SecuritySurveillanceHub securitySurveillanceHub = new SecuritySurveillanceHub();

            // creating objs of EmployeeNotify and adding all attrbs
            EmployeeNotify employeeNotify1 = new EmployeeNotify(new Employee
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Jones",
                JobTitle = "Development Manager"
            });
            EmployeeNotify employeeNotify2 = new EmployeeNotify(new Employee
            {
                Id = 2,
                FirstName = "Dave",
                LastName = "Kendal",
                JobTitle = "Chief Information Officer"
            });

            //Creating obj of SecurityNotify 
            SecurityNotify securityNotify = new SecurityNotify();

            //EmployeeNotify uses the subscribe method so as to send notifictions
            employeeNotify1.Subscribe(securitySurveillanceHub);
            employeeNotify2.Subscribe(securitySurveillanceHub);
            securityNotify.Subscribe(securitySurveillanceHub);

            //Confirmation of entering
            securitySurveillanceHub.ConfirmExternalVisitorEntersBuilding(1, "Andrew", "Jackson", "The Company", "Contractor", DateTime.Parse("12 May 2020 11:00"), 1);
            securitySurveillanceHub.ConfirmExternalVisitorEntersBuilding(2, "Jane", "Davidson", "Another Company", "Lawyer", DateTime.Parse("12 May 2020 12:00"), 2);

            // employeeNotify.UnSubscribe();
            
            //Confirmation of exiting
            securitySurveillanceHub.ConfirmExternalVisitorExitsBuilding(1, DateTime.Parse("12 May 2020 13:00"));
            securitySurveillanceHub.ConfirmExternalVisitorExitsBuilding(2, DateTime.Parse("12 May 2020 15:00"));

            //Cutoff of entry time
            securitySurveillanceHub.BuildingCutoffEntryTimeReached();

            Console.ReadKey();

        }
    }

    public class Employee : IEmployee
    {
        //Implementation of Employee class with IEmployee interface
        public int Id { get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
    }
    public interface IEmployee
    {
        // Implementing interface Iemployee
        int Id { get; set; } 
        string FirstName { get; set; }
        string LastName { get; set; }   
        string JobTitle { get; set; }
    }

    public abstract class Observer : IObserver<ExternalVisitor>
    {
        //The purpose of this abstract class so that we can follow the principle of dry 
        //OnNext, OnCompleted, OnError are methods of the generic IObserver<ExternalVisitor>
        //IDisposable is also an in built interface with the dispose method
        // we use the abstract method so that we can ovveride these methods in other classes when we inherit the Observer class
        // so when we inherit we can override the methods instead of constantly writing new funcs again

        IDisposable _cancellation;
        protected List<ExternalVisitor> _externalVisitors = new List<ExternalVisitor>();

        public abstract void OnCompleted();

        public abstract void OnError(Exception error);

        public abstract void OnNext(ExternalVisitor value);

        public void Subscribe(IObservable<ExternalVisitor> provider)
        {
            _cancellation = provider.Subscribe(this); // this returns an object that implements the IDisposable interface
        }

        public void UnSubscribe()
        {
            _cancellation.Dispose();
            _externalVisitors.Clear();
        }

    }

    public class EmployeeNotify : Observer
    {
        //EmployeeNotify class to notify the employees
        IEmployee _employee = null;
       
        public EmployeeNotify(IEmployee employee)
        {
            _employee = employee;
        }
        
        public override void OnCompleted()
        {
            //Overridng the methods
            string heading = $"{_employee.FirstName + " " + _employee.LastName} Daily Visitors Report";
            Console.WriteLine();

            Console.WriteLine(heading);
            Console.WriteLine(new string('-', heading.Length));
            Console.WriteLine();

            foreach(var externalVisitor in _externalVisitors)
            {
                //traversing through the list of _externalVisitors
                //we will keep it false first 
                externalVisitor.InBuilding = false;
                Console.WriteLine($"{externalVisitor.Id,-6}{externalVisitor.FirstName,-15}{externalVisitor.LastName,-15}{externalVisitor.EntryDateTime.ToString("dd MMM yyyy hh:mm:ss"),-25}{externalVisitor.ExitDateTime.ToString("dd MMM yyyy hh:mm:ss tt"),-25}");
            }

            Console.WriteLine();
            Console.WriteLine();    
        }

        public override void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public override void OnNext(ExternalVisitor value) //https://chat.openai.com/share/222288c6-445d-4be0-ad7e-c3bd7db01659
        {
            var externalVisitor = value;// if there is an extenal visitor

            if(externalVisitor.EmployeeContactId == _employee.Id)
            {
                // if the assigned employee ID for the external visitor matches the employee id
                var ExternalVisitorListItem = _externalVisitors.FirstOrDefault(e => e.Id == externalVisitor.Id);//Lambda and Linq operation to retrieve the IDs 
                
                if(ExternalVisitorListItem == null)
                {
                    // if nll values added to list
                    _externalVisitors.Add(externalVisitor);

                    // COnsole color themes
                    OutputFormatter.ChangeOutputTheme(OutputFormatter.TextOutputTheme.Employee);


                    Console.WriteLine($"Employee with name of {_employee.FirstName} {_employee.LastName} your visitor has arrived and entered the building with ID {externalVisitor.Id} and the name is {externalVisitor.FirstName} {externalVisitor.LastName}");
                    Console.WriteLine($"Date and time {externalVisitor.EntryDateTime.ToString("dd MM yyyy  hh:mm:ss")}");

                    // Console color themes
                    OutputFormatter.ChangeOutputTheme(OutputFormatter.TextOutputTheme.Normal);


                    Console.WriteLine();
                }

                else
                {
                    if(externalVisitor.InBuilding == false)
                    {
                        // if flase the visiotr is not there in the building
                        ExternalVisitorListItem.InBuilding = false;
                        ExternalVisitorListItem.ExitDateTime = externalVisitor.ExitDateTime;
                        // give exit date time
                    }
                }
            }
        }
    }

    public class SecurityNotify : Observer   
    {

        public override void OnCompleted() // method for the part after notificatins are done
        {
            string heading = "Security Daily Visitor's Report"; 
            Console.WriteLine();

            Console.WriteLine(heading);
            Console.WriteLine(new string('-', heading.Length));
            Console.WriteLine();

            foreach (var externalVisitor in _externalVisitors)
            {
                externalVisitor.InBuilding = false;// first false condition

                Console.WriteLine($"{externalVisitor.Id,-6}{externalVisitor.FirstName,-15}{externalVisitor.LastName,-15}{externalVisitor.EntryDateTime.ToString("dd MMM yyyy hh:mm:ss"),-25}{externalVisitor.ExitDateTime.ToString("dd MMM yyyy hh:mm:ss tt"),-25}");
                //traverse and print          
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        public override void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public override void OnNext(ExternalVisitor value)
        {
            var externalVisitor = value;

            var externalVisitorListItem = _externalVisitors.FirstOrDefault(e => e.Id == externalVisitor.Id);

            if (externalVisitorListItem == null)
            {
                //adds to the list
                _externalVisitors.Add(externalVisitor);

                OutputFormatter.ChangeOutputTheme(OutputFormatter.TextOutputTheme.Security);
                // console color changer

                Console.WriteLine($"Security notification: Visitor Id({externalVisitor.Id}), FirstName({externalVisitor.FirstName}), LastName({externalVisitor.LastName}), entered the building, DateTime({externalVisitor.EntryDateTime.ToString("dd MMM yyyy hh:mm:ss tt")})");

                OutputFormatter.ChangeOutputTheme(OutputFormatter.TextOutputTheme.Normal);
                // console color changer
                Console.WriteLine();
            }
            else
            {
                if (externalVisitor.InBuilding == false)
                {
                    //update local external visitor list item with data from the external visitor object passed in from the observable object
                    externalVisitorListItem.InBuilding = false;
                    externalVisitorListItem.ExitDateTime = externalVisitor.ExitDateTime;

                    Console.WriteLine($"Security notification: Visitor Id({externalVisitor.Id}), FirstName({externalVisitor.FirstName}), LastName({externalVisitor.LastName}), exited the building, DateTime({externalVisitor.ExitDateTime.ToString("dd MMM yyyy hh:mm:ss tt")})");
                    Console.WriteLine();

                }
            }

        }
    }

    public class Unsubscriber<ExternalVisitor> : IDisposable
    {
        //https://chat.openai.com/share/222288c6-445d-4be0-ad7e-c3bd7db01659
        private List<IObserver<ExternalVisitor>> _observers; //List
        private IObserver<ExternalVisitor> _observer; //object

        public Unsubscriber(List<IObserver<ExternalVisitor>> observers, IObserver<ExternalVisitor> observer)
        {
            _observers = observers;
            _observer = observer;
        }
        public void Dispose() //method of IDispoable
        {
            if (_observers.Contains(_observer))
            {
                _observers.Remove(_observer);
            }
        }
    }

    public class SecuritySurveillanceHub : IObservable<ExternalVisitor>
    {
        private List<ExternalVisitor> _externalVisitors;
        private List<IObserver<ExternalVisitor>> _observers;

        public SecuritySurveillanceHub()
        {
            _externalVisitors = new List<ExternalVisitor>();
            _observers = new List<IObserver<ExternalVisitor>>();
        }

        public IDisposable Subscribe(IObserver<ExternalVisitor> observer)
        {
            if(!_observers.Contains(observer))
            {
                _observers.Add(observer);   
            }

            foreach(var externalVisitor in _externalVisitors) //traverses through the list and notifies the observer 
            {
                observer.OnNext(externalVisitor); //Implements the OnNext for every external visitor
            }    

            return new Unsubscriber<ExternalVisitor>(_observers,observer);
        }

        public void ConfirmExternalVisitorEntersBuilding(int id, string firstName, string lastName, string companyName, string jobTitle, DateTime entryDateTime,int employeeContactId)
        {
            ExternalVisitor externalVisitor = new ExternalVisitor
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                CompanyName = companyName,
                JobTitle = jobTitle,
                EntryDateTime = entryDateTime,
                InBuilding = true,
                EmployeeContactId = employeeContactId
            };

            _externalVisitors.Add(externalVisitor);

            foreach (var observer in _observers)
                observer.OnNext(externalVisitor);// IObserver used in implemeting the list 
        }

        public void ConfirmExternalVisitorExitsBuilding(int externalVisitorID, DateTime exitDateTime)
        {
            var externalVisitor = _externalVisitors.FirstOrDefault(e => e.Id == externalVisitorID);
            //Linq function

            if(externalVisitor != null)
            {
                externalVisitor.ExitDateTime = exitDateTime;
                externalVisitor.InBuilding = false;
                
                foreach (var observer in _observers)
                {
                    observer.OnNext(externalVisitor);
                }
            }
        }

        public void BuildingCutoffEntryTimeReached()
        {
            if(_externalVisitors.Where( e=>e.InBuilding == true).ToList().Count == 0)
            {
                foreach(var observer in _observers)
                {
                    observer.OnCompleted();
                }
            }
        }
    }

    public static class OutputFormatter
    {
        //Console text color changing code
        public enum TextOutputTheme
        {
            Security,
            Employee,
            Normal
        }

        public static void ChangeOutputTheme(TextOutputTheme textOutputTheme)
        {
            if(textOutputTheme == TextOutputTheme.Employee)
            {
                Console.BackgroundColor = ConsoleColor.Magenta;
                Console.ForegroundColor = ConsoleColor.White;
            }

            else if(textOutputTheme == TextOutputTheme.Security)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Green;

            }

            else
            {
                Console.ResetColor();
            }

        }
    }

    public class ExternalVisitor
    {
        // ExternalVsitor class 
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public DateTime EntryDateTime { get; set; }
        public DateTime ExitDateTime { get; set; }
        public bool InBuilding { get; set; }
        public int EmployeeContactId { get; set; }  

    }
}


