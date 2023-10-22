using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.BusinessLayer.Abstract
{
    //Tek seferlik bir sınıfta crud işlemleri tanımladık.
    public interface IGenericService<T> where T : class
    {
        //Burada IGenericDal gibi oradan ayınısını alıp burada Presentation katmanındada bunları tanımlarız.
        void TInsert(T t);//ekleme , silme , güncelleme , id ile getirme ve listeleme metodları tanımlandı
        void TDelete(T t);
        void TUpdate(T t);
        T TGetByID(int id);
        List<T> TGetList();
    }
}
