using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            LinqQueriesDataContext db = new LINQ.LinqQueriesDataContext();

            var allCustomers = db.Customers;

            foreach(Customer cust in allCustomers)
            {
                Console.WriteLine("Company Name: {0}, Contact Name: {1}", cust.CompanyName, cust.ContactName);
            }

            Console.WriteLine();
            Console.WriteLine();

            var certainCustomers = from c in db.Customers
                                   where c.Region == "BC"
                                   select c;

            foreach (Customer cust in certainCustomers)
            {
                Console.WriteLine("Company Name: {0}, Contact Name: {1}", cust.CompanyName, cust.ContactName);
            }

            //NewStuffDataContext newDB = new NewStuffDataContext();

            //var employeeList = newDB.Employees;

            //var emplyList = from e in newDB.Employees
            //                select e;

            //foreach (Employee emp in employeeList)
            //{
            //    Console.WriteLine("Employee Name: {0}", emp.FirstName);
            //}

            //Console.WriteLine();

            //foreach (Employee emp in emplyList)
            //{
            //    Console.WriteLine("Employee Name: {0}", emp.FirstName);
            //}

            var custs = from c in db.Customers
                        where c.Country == "USA" || c.Country == "Canada"
                        orderby c.CompanyName ascending
                        select c;

            foreach (Customer c in custs)
            {
                Console.WriteLine("ID: {0}, Company Name: {1}", c.CustomerID, c.CompanyName);
            }

            var groupedCusts = from c in db.Customers
                               group c by c.CompanyName.Substring(0, 2) into custGroup
                               orderby custGroup.Key
                               select custGroup;

            foreach (var gc in groupedCusts)
            {
                Console.WriteLine(gc.Key);
                foreach (var c in gc)
                {
                    Console.WriteLine(" {0},    {1}", c.CompanyName, c.ContactName);
                }
            }

            var innrJn = from t in db.Territories
                            join r in db.Regions
                            on t.RegionID equals r.RegionID
                            select new
                            {
                                Terr = t.TerritoryDescription,
                                Reg = r.RegionDescription
                            };

            foreach(var ij in innrJn)
            {
                Console.WriteLine("Territory desc: {0}, Region desc: {1}", ij.Terr.Replace(" ", ""), ij.Reg.Replace(" ", ""));
            }

            var peopleInLondon = (from c in db.Customers
                                  where c.City == "London"
                                  select c.ContactName).Concat(from e in db.Employees
                                                               where e.City == "London"
                                                               select e.FirstName + " " + e.LastName);
            foreach (var p in peopleInLondon)
            {
                Console.WriteLine("Name: {0}", p);
            }


            Console.ReadLine();
        }
    }
}
