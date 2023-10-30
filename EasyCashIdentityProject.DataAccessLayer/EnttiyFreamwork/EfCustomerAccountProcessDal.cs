using EasyCashIdentityProject.DataAccessLayer.Abstarct;
using EasyCashIdentityProject.DataAccessLayer.Concrete;
using EasyCashIdentityProject.DataAccessLayer.Repositories;
using EasyCashIdentityProject.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.DataAccessLayer.EnttiyFreamwork
{
    //Özel tanımlamaları buradan çekiyor ve işleme sokuyor olacağız
    public class EfCustomerAccountProcessDal : GenericRepository<CustomerAccountProcess>, ICustomerAccountProcessDal
    {
        public List<CustomerAccountProcess> MyLastProcess(int id)
        {
            // "using" ifadesi, IDisposable arabirimini uygulayan bir nesneyi otomatik olarak temizler.
            // "Context" sınıfından bir örnek oluşturulur ve kullanıldıktan sonra bellekten otomatik olarak temizlenir.
            using var context = new Context();

            // "CustomerAccountProcesses" veri tabanı tablosundan verileri almak için Entity Framework kullanılır.
            // Bu sorgu, belirtilen "id" değeri ile ilişkilendirilmiş "CustomerAccountProcess" nesnelerini alır.
            // "SenderCustomer" ilişkisini de dahil eder, böylece "SenderCustomer" nesnelerine erişebilirsiniz.
            var values = context.CustomerAccountProcesses
                .Include(y => y.SenderCustomer) // "SenderCustomer" ilişkisini yükler
                .Where(x => x.ReceiverID == id || x.SenderID == id) // Belirtilen "id" ile eşleşen nesneleri filtreler
                .ToList(); // Sonuçları bir liste olarak alır

            // Sorgu sonuçları "values" listesi olarak döndürülür.
            return values;
        }
    }
}
