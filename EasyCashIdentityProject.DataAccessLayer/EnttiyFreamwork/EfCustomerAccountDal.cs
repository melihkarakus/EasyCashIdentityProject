using EasyCashIdentityProject.DataAccessLayer.Abstarct;
using EasyCashIdentityProject.DataAccessLayer.Concrete;
using EasyCashIdentityProject.DataAccessLayer.Repositories;
using EasyCashIdentityProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.DataAccessLayer.EnttiyFreamwork
{
    public class EfCustomerAccountDal : GenericRepository<CustomerAccount>, ICustomerAccountDal
    {
        public List<CustomerAccount> GetCustomerAccountList(int id)
        {
            using var context = new Context(); // Veritabanı bağlamını oluşturur ve otomatik olarak temizler (using kullanımı).
            // Veritabanından belirli bir kullanıcı kimliği ile ilişkilendirilmiş müşteri hesaplarını sorgular ve bir liste olarak alır.
            var values = context.CustomerAccounts.Where(x => x.AppUserID == id).ToList(); 
            return values; // Sonuçları çağıran yere döndürür.
        }
    }
}
