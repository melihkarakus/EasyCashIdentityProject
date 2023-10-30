using EasyCashIdentityProject.BusinessLayer.Abstract;
using EasyCashIdentityProject.DataAccessLayer.Abstarct;
using EasyCashIdentityProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.BusinessLayer.Concrete
{
    //ICustomerAccounProcesstServiceden İpmlamente ettik
    public class CustomerAccountProcessManager : ICustomerAccountProcessService
    {
        //IcustomerAccountProcessDal dan generate const. aldık
        private readonly ICustomerAccountProcessDal _customerAccountProcessDal;

        public CustomerAccountProcessManager(ICustomerAccountProcessDal customerAccountProcessDal)
        {
            _customerAccountProcessDal = customerAccountProcessDal;
        }
        //IGenericDaldaki tanımlanan crud işlemleri buraya implemente edildi.
        public void TDelete(CustomerAccountProcess t)
        {
            _customerAccountProcessDal.Delete(t);//DataAccesLayer daki crud işlemlerinden yararlanarak buraya işlendi 
            //amacı ise Databasede bağlı olan kısmı silip burada döndürüp bizim prasentationLayerdaki kısma geçerli olması
        }

        public CustomerAccountProcess TGetByID(int id)
        {
            return _customerAccountProcessDal.GetByID(id);
        }

        public List<CustomerAccountProcess> TGetList()
        {
            return _customerAccountProcessDal.GetList();
        }

        public void TInsert(CustomerAccountProcess t)
        {
            _customerAccountProcessDal.Insert(t);
        }

        public List<CustomerAccountProcess> TMyLastProcess(int id)
        {
            return _customerAccountProcessDal.MyLastProcess(id);
        }

        public void TUpdate(CustomerAccountProcess t)
        {
            _customerAccountProcessDal.Update(t);
        }
    }
}
