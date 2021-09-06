using Assignment_One.Models;
using Assignment_One.Models.Context;
using Assignment_One.Services;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Assignment_One
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Assignment 1");

            try
            {

                DB_Context context = new DB_Context();
                CategoryService category = new CategoryService(context);
                ProductService product = new ProductService(context);
                VendorService vendor = new VendorService(context);

                List<Category> categories = new List<Category>()
                {
                    new Category { CategoryName = "Fruits" },
                    new Category { CategoryName = "Dry-Fruits" },
                    new Category { CategoryName = "Vegetables" }
                };


                List<Product> products = new List<Product>()
                {
                    new Product { ProductName = "Apple",Price=40, CategoryId= 1},
                    new Product { ProductName = "Mango",Price=20, CategoryId= 1},
                    new Product { ProductName = "Kiwi",Price=90, CategoryId= 1},
                    new Product { ProductName = "Pista",Price=120, CategoryId= 2},
                    new Product { ProductName = "Almond",Price=250, CategoryId= 2},
                    new Product { ProductName = "Potato",Price=20, CategoryId= 3},
                    new Product { ProductName = "Tomato",Price=10, CategoryId= 3},
                    new Product { ProductName = "Lemon",Price=5, CategoryId= 3},
                };

                List<Vendor> vendors = new List<Vendor>()
                {
                    new Vendor { VendorName = "Ritesh Khatri" },
                    new Vendor { VendorName = "Mahesh Sabnis" },
                };


                foreach (Category cat in categories)
                {
                    var result = await category.CreateAsync(cat);
                    Console.WriteLine(JsonSerializer.Serialize(result));
                }

                foreach (Product pro in products)
                {
                    var result = await product.CreateAsync(pro);
                    Console.WriteLine(JsonSerializer.Serialize(new Product { ProductId = result.ProductId, ProductName = result.ProductName, Price = result.Price, CategoryId = result.CategoryId } ));
                }
                foreach (Vendor ven in vendors)
                {
                    var result = await vendor.CreateAsync(ven);
                    Console.WriteLine(JsonSerializer.Serialize(result));
                }



                //DepartmentService dServ = new DepartmentService(ctx);
                //var depts = await dServ.GetAsync();
                //Console.WriteLine($"Result = {JsonSerializer.Serialize(depts)}");

                //for (int i = 0; i < 10; i++)
                //{
                //    Console.WriteLine("Caller");
                //}

                //var dept = new Department()
                //{
                //    DeptNo = 30,
                //    DeptName = "TRG",
                //    Location = "Pune[WEST]"
                //};
                //var newDept = await dServ.CreateAsync(dept);
                //Console.WriteLine($"Result After Add= {JsonSerializer.Serialize(newDept)}");

                //dept = await dServ.UpdateAsync(dept.DeptNo,dept);

                //Console.WriteLine($"Result After Update= {JsonSerializer.Serialize(dept)}");

                //await dServ.DeleteAsync(30);
                //depts = await dServ.GetAsync();
                //Console.WriteLine($"Result After Delete= {JsonSerializer.Serialize(depts)}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured {ex.ToString()}");
            }

            Console.ReadLine();
        }
    }
}
