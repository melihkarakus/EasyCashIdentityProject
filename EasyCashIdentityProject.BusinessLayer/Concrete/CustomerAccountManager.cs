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
    //ICustomerAccountServiceden İpmlamente ettik 
    public class CustomerAccountManager : ICustomerAccountService
    {
        //IcustomerAccountDal dan generate const. aldık
        private readonly ICustomerAccountDal _customerAccountDal;

        public CustomerAccountManager(ICustomerAccountDal customerAccountDal)
        {
            _customerAccountDal = customerAccountDal;
        }
        //IGenericDaldaki tanımlanan crud işlemleri buraya implemente edildi.
        public void TDelete(CustomerAccount t)
        {
            _customerAccountDal.Delete(t);//DataAccesLayer daki crud işlemlerinden yararlanarak buraya işlendi 
            //amacı ise Databasede bağlı olan kısmı silip burada döndürüp bizim prasentationLayerdaki kısma geçerli olması
        }

        public CustomerAccount TGetByID(int id)
        {
            return _customerAccountDal.GetByID(id);
        }

        public List<CustomerAccount> TGetList()
        {
            return _customerAccountDal.GetList();
        }

        public void TInsert(CustomerAccount t)
        {
            _customerAccountDal.Insert(t);
        }

        public void TUpdate(CustomerAccount t)
        {
            _customerAccountDal.Update(t);
        }
    }
}
