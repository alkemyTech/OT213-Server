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
    public class NewsBusiness : GenericBusiness<New>, INewsBusiness
    {
        private IUnitOfWork _uow;
        private INewsRepository _newsRepository;
        public NewsBusiness(IUnitOfWork uow, INewsRepository newsRepository) : base(newsRepository)
        {
            this._uow = uow;
            this._newsRepository = newsRepository;
        }
    }
}
