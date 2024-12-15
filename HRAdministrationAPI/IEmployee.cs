using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAdministrationAPI
{
    public interface IEmployee
    {
        string FirstName { get; set;}
        string LastName { get; set;}
        int Id { get; set;}
        decimal Salary { get; set;}
    }
}
