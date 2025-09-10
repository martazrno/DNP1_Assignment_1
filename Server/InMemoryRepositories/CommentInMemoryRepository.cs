using Entities;
using RepositoryContracts;
namespace InMemoryRepositories;
public class CommentInMemoryRepository : ICommentRepository
{
    private List<Comment> comments;

    public CommentInMemoryRepository()
    {
        comments = new List<Comment>();
    }

    public Task<Comment> AddAsync(Comment comment)
    {
        comment.ID = comments.Any()
            ? comments.Max(p => p.ID) + 1
            : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comment comment)
    {
        Comment? existingComment = comments.SingleOrDefault(p => p.ID == comment.ID);
        if (existingComment is null)
            throw new InvalidOperationException($"Comment with ID '{comment.ID}' not found");

        comments.Remove(existingComment);
        comments.Add(comment);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Comment? commentToRemove = comments.SingleOrDefault(p => p.ID == id);
        if (commentToRemove is null)
            throw new InvalidOperationException($"User with ID '{id}' not found");

        comments.Remove(commentToRemove);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        Comment? comment = comments.SingleOrDefault(p => p.ID == id);
        if (comment is null)
            throw new InvalidOperationException($"Comment with ID '{id}' not found");

        return Task.FromResult(comment);
    }

    public IQueryable<Comment> GetMany()
    {
        return comments.AsQueryable();
    }
        
}