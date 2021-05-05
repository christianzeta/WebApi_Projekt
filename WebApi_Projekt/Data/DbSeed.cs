using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_Projekt.Models;

namespace WebApi_Projekt.Data
{
    public class DbSeed
    {
        public static void Seeder(Context context)
        {

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var users = new List<MyUser>()
            {
                new MyUser { FirstName="Johan", LastName="Svensson", Token="DemoToken"},
                new MyUser { FirstName="Erik", LastName="Johansson", Token="TokenDemo"},
                new MyUser { FirstName="Daniel", LastName="Heder", Token="HejHejhej"}
            };

            context.MyUsers.AddRange(users);
            context.SaveChanges();
        }
    }
}
