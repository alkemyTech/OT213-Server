using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class CommentBusiness : GenericBusiness<Comment>, ICommentBusiness
    {
        public CommentBusiness(IUnitOfWork uow, ICommentRepository commentRepository) : base(commentRepository, uow)
        {
        }
    }
    
}

