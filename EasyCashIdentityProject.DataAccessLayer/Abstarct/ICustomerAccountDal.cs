using EasyCashIdentityProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.DataAccessLayer.Abstarct
{
    //Eğer bu sınıfa ayrı bir özel metod tanımlamak istersem burada tanımlayabilirim.
    public interface ICustomerAccountDal : IGenericDal<CustomerAccount>
    {
        List<CustomerAccount> GetCustomerAccountList(int id);
    }
}
