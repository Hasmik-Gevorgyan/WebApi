using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApi.DataAccess;
using WebApi.Models;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            UserDAL UserDAL = new UserDAL();

            // InsertUser
            for (int i = 1; i < 10; i++)
            {
                UserDAL.InsertUser(
                    new User()
                    { ID = i, Name = "UserName" + i, LastName = "UserLastName" + i, Email = "UserEmail" + i + "@gmail.com", Address= "User " + i + "Address"}
                );

                Console.WriteLine($"ResultText for user {i}: {UserDAL.ResultText}");
            }

            // GetUsersCountScalar
            Console.WriteLine($"\n\nUsers count is: { UserDAL.GetUsersCountScalar()}");
           
            // GetUsersAsGenericList
            List<User> users = UserDAL.GetUsersAsGenericList();

            // 'for' loop used here for demonstrate how use index in the list structure
            Console.WriteLine("\n\nUserId UserName UserLastName UserEmail Address");
            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine($"{users[i].ID}\t\t{users[i].Name}\t{users[i].LastName}\t{users[i].Email}\t{users[i].Address}");
            }

            Console.ReadLine();            
        }
        

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
