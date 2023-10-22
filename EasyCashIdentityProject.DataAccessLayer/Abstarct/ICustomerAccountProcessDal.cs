﻿using EasyCashIdentityProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.DataAccessLayer.Abstarct
{
    //Bu sınıf için ayrı özel bir metod oluşturmak istersek burada tanımlayabiliriz.
    public interface ICustomerAccountProcessDal : IGenericDal<CustomerAccountProcess>
    {
    }
}
