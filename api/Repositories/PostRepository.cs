using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Data;
using api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Post> CreatePost(Post post)
        {
            _context.Post.Add(post);
            await _context.SaveChangesAsync();
            post.Text = Encoding.ASCII.GetString(post.TextByte);
            return post;
        }

        public void CreatePost1(Post post)
        {
            _context.Post.Add(post);
            _context.SaveChanges();
            post.Text = Encoding.ASCII.GetString(post.TextByte);

        }

        public async Task<IEnumerable<Post>> Get()
        {
            throw new NotSupportedException();
        }

        public async Task<Post> GetPostById(int id)
        {
            var post = await _context.Post.Where(x => x.Id == id).FirstOrDefaultAsync();
            post.Text = Encoding.ASCII.GetString(post.TextByte);
            return post;
        }

        public int GetPostCount()
        {
            return _context.Post.Count();
        }

        public async Task<IEnumerable<Post>> GetPostPerPage(int page, int limit)
        {
            if (page == 0)
                page = 1;

            if (limit == 0)
                limit = int.MaxValue;

            var skip = (page - 1) * limit;

            var posts = await _context.Post
                .Skip(skip)
                .Take(limit)
                .ToListAsync();
            posts.ForEach(p => p.Text = Encoding.ASCII.GetString(p.TextByte));
            return posts;

        }

        public async Task<IEnumerable<Post>> GetPostsByUser(int userId)
        {
            var posts = await _context.Post.Where(x => x.UserId == userId).ToListAsync();

            posts.ForEach(p => p.Text = Encoding.ASCII.GetString(p.TextByte));
            return posts;
        }
    }
}
