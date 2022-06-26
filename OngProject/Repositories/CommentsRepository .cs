using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentsRepository
    {
        public CommentRepository (OngProjectDbContext context) : base(context)
        {            
        }
    }

}

