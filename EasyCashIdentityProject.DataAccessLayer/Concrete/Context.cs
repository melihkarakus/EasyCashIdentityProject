using EasyCashIdentityProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.DataAccessLayer.Concrete
{
    public class Context : IdentityDbContext<AppUser,AppRole,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=LAPTOP-VLOIQ6M3; initial catalog=EasyCashDb; integrated security=true");
        }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; } //Database Tabloyu yansıtmak için tanımlanan kod
        public DbSet<CustomerAccountProcess> CustomerAccountProcesses { get; set; }
    }
}
