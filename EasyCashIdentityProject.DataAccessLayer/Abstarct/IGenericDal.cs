using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.DataAccessLayer.Abstarct
{
    //her bir sınıf için değil bir tek sınıf için tanımlandı her sınıfta direk çağırmak için
    public interface IGenericDal<T> where T : class 
    {
        void Insert(T t);//ekleme , silme , güncelleme , id ile getirme ve listeleme metodları tanımlandı
        void Delete(T t);
        void Update(T t);
        T GetByID (int id);
        List<T> GetList();
    }
}
