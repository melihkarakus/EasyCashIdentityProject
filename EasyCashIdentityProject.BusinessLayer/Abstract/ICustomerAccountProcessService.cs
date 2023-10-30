using EasyCashIdentityProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.BusinessLayer.Abstract
{
   //Eğer farklı method tanımlanırsa DataAcces katmanında repository burada Başına T koyup ayırıp bunuda PresentationLayerda çağırırız
    public interface ICustomerAccountProcessService : IGenericService<CustomerAccountProcess>
    {
        List<CustomerAccountProcess> TMyLastProcess(int id);
    }
}
