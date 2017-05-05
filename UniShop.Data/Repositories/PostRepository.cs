using System.Collections.Generic;
using System.Linq;
using UniShop.Data.Infrastructure;
using UniShop.Model.Models;

namespace UniShop.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> GetAllByTagPaging(string tag, int pageIndex, int pageSize, out int totalRow);
    }

    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Post> GetAllByTagPaging(string tag, int pageIndex, int pageSize, out int totalRow)
        {
            var query = from p in DbContext.Posts
                join pt in DbContext.PostTags
                    on p.ID equals pt.PostID
                where pt.TagID == tag && p.Status
                orderby p.CreatedDate descending
                select p;
            totalRow = query.Count();

            // lấy item theo trang
            query = query.Skip((pageIndex - 1)*pageSize).Take(pageSize);

            return query;
        }
    }
}