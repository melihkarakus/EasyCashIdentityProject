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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // 'CustomerAccountProcess' sınıfının ilişkilerini yapılandırma
            builder.Entity<CustomerAccountProcess>()
                // Gönderen müşteri ile ilişkiyi tanımlama
                .HasOne(x => x.SenderCustomer)
                // Birçok 'CustomerSender' referansına sahip
                .WithMany(y => y.CustomerSender)
                // 'SenderID' alanını yabancı anahtar olarak kullan
                .HasForeignKey(z => z.SenderID)
                // Eğer ana nesne silinirse, bağlı alt nesneyi null olarak ayarla
                .OnDelete(DeleteBehavior.ClientSetNull);

            // 'CustomerAccountProcess' sınıfının başka bir ilişkisini yapılandırma
            builder.Entity<CustomerAccountProcess>()
                // Alıcı müşteri ile ilişkiyi tanımlama
                .HasOne(x => x.ReceiverCustomer)
                // Birçok 'CustomerReceiver' referansına sahip
                .WithMany(y => y.CustomerReceiver)
                // 'ReceiverID' alanını yabancı anahtar olarak kullan
                .HasForeignKey(z => z.ReceiverID)
                // Eğer ana nesne silinirse, bağlı alt nesneyi null olarak ayarla
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Temel DbContext sınıfının 'OnModelCreating' metodunu çağır
            base.OnModelCreating(builder);
        }
    }
}
