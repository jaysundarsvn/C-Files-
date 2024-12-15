using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAdministrationAPI
{
    // Generic FactoryPattern class definition
    public class FactoryPattern<K, T> where T : class, K, new()
    {
        // Method to create an instance of type T and return it as type K
        public static K GetInstance()
        {
            K objk;  // Declare a variable of type K
            objk = new T();  // Create a new instance of type T
            return objk;  // Return the instance of type T as type K
        }
    }
}
/*where T : class, K, new(): This is a generic type constraint that specifies requirements for the types K and T.

T : class: It enforces that T must be a reference type (i.e., a class).
T: K: It enforces that T must be assignable to K. This means that T must be a class that derives from or implements the type K.
T : new(): It requires that T has a parameterless constructor. This allows an instance of T to be created using new T().
public static K GetInstance(): This is a public static method named GetInstance which returns an object of type K.

K objk;: This declares a variable objk of type K. This variable will be used to hold the instance of T which will be created.

objk = new T();: This line creates a new instance of the type T using new T().Since T is constrained to have a parameterless constructor (new()), this is valid.

return objk;: This line returns the newly created instance of T as a type K.*/

