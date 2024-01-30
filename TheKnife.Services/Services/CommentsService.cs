using Microsoft.EntityFrameworkCore;
using TheKnife.Entities.Efos;
using TheKnife.EntityFramework;

namespace TheKnife.Services.Services
{
    public interface ICommentsService
    {
        Task<List<CommentsEfo>> GetAllComments();
        Task<CommentsEfo> GetCommentById(int id);
        Task<CommentsEfo> SendComment(CommentsEfo comment);
        Task DeleteComment(int id);
    }

    public class CommentsService : ICommentsService
    {
        private readonly TheKnifeDbContext _context;

        public CommentsService(TheKnifeDbContext context)
        {
            _context = context;
        }

        public async Task<List<CommentsEfo>> GetAllComments()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<CommentsEfo> GetCommentById(int id)
        {
            CommentsEfo comment = await _context.Comments.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                throw new Exception("Comentário não encontrado");
            }

            return comment;
        }

        public async Task<CommentsEfo> SendComment(CommentsEfo comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task DeleteComment(int id)
        {
            CommentsEfo comment = await _context.Comments
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                throw new Exception("Comentário não encontrado");
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}
