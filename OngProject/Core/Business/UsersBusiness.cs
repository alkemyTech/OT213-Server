using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class UsersBusiness : GenericBusiness<User>, IUsersBusiness
    {

        public UsersBusiness(IUnitOfWork uow, IUsersRepository usersRepository) : base(usersRepository, uow)
        {

        }      

        
    }
}
