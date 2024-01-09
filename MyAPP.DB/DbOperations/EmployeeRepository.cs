using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Model;

namespace MyAPP.DB.DbOperations
{
    public class EmployeeRepository
    {
        public int AddEmployee(EmployeeModel model)
        {
            using (var context = new NewMyAppDBEntities1())
            {
                Employee emp = new Employee()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Code = model.Code,               
                };

                if(model.Address != null)
                {
                    emp.Address = new Address()
                    {
                        Details = model.Address.Details,
                        State = model.Address.State,
                        Country = model.Address.Country
                    };
                }
                context.Employee.Add(emp);
                context.SaveChanges();
                return emp.Id;
            }
        }

        public List<EmployeeModel> GetAllEmployees()
        {
            using (var context = new NewMyAppDBEntities1())
            {
                var result = context.Employee
                    .Select(x=>new EmployeeModel()
                {
                    Id = x.Id,
                    AddressId = x.AddressId,
                    Code = x.Code,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Address = new AddressModel()
                    {
                        Id = x.Address.Id,
                        Details = x.Address.Details,
                        Country = x.Address.Country,
                        State = x.Address.State
                    }
                }).ToList();
                return result;
                
            }
        }

        public EmployeeModel GetEmployee(int id)
        {
            using (var context = new NewMyAppDBEntities1())
            {
                var result = context.Employee
                    .Where(x => x.Id == id)
                    .Select(x => new EmployeeModel()
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        AddressId = x.AddressId,
                        LastName = x.LastName,
                        Email = x.Email,
                        Code = x.Code,
                        Address = new AddressModel()
                        {
                            Id = x.Address.Id,
                            Details = x.Address.Details,
                            Country = x.Address.Country,
                            State = x.Address.State
                        }

                    }).FirstOrDefault();


                return result;
            }
        }

        public bool UpdateEmployee(int id , EmployeeModel model)
        {
            using (var context = new NewMyAppDBEntities1())
            {
                var employee = context.Employee.FirstOrDefault(x=>x.Id == id);
                if (employee != null)
                {
                    employee.FirstName = model.FirstName;
                    employee.LastName = model.LastName;
                    employee.Email = model.Email;
                    employee.Code = model.Code;
                }
                context.SaveChanges();
                return true;
                
            }
        }


        public bool DeleteEmployee(int id)
        {
            using(var context = new NewMyAppDBEntities1())
            {
                var emp = new Employee()
                {
                    Id= id
                };
                context.Entry(emp).State
                //var employee = context.Employee.FirstOrDefault(x=> x.Id==id);
                //if(employee != null)
                //{
                //    context.Employee.Remove(employee);
                //    context.SaveChanges();
                //    return true;
                //}
                return false;
            }
        }
    }

}
