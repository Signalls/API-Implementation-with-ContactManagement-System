using ContactData.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContactData
{
    public class DbContactContext : IdentityDbContext<AppUser>
    {


        public DbContactContext(DbContextOptions<DbContactContext> options) : base(options)
        {

        }


        public DbSet<Contact> Contacts { get; set; }

    }
}