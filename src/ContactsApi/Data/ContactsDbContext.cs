using Microsoft.EntityFrameworkCore;
using ContactsApi.Models;

namespace ContactsApi.Data
{
    public class ContactsDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public ContactsDbContext(DbContextOptions<ContactsDbContext> options)
            : base(options)
        {

        }
    }
}