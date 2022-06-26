using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class CommentsBusiness : GenericBusiness<Comment>, ICommentsBusiness
    {
        public CommentsBusiness(IUnitOfWork uow, ICommentsRepository commentRepository) : base(commentRepository, uow)
        {
        }
    }
    
}

