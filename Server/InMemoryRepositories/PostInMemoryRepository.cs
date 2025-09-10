namespace InMemoryRepositories;

public class PostInMemoryRepository
{
    private List<Post> posts;

    public Task<Post> AddAsync(Post post)
    {
        post.ID = posts.Any()
            ? posts.Max(p => p.ID) + 1
            : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }


    public Task UpdateAsync(Post post)
    {
        Post? existingPost = posts.SingleOrDefault(p => p.ID == post.ID);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{post.ID}' not found");
        }
        posts.Remove(existingPost);
    }
    
    public Task DeleteAsync(int id)
    {
        Post? postToRemove = posts.SingleOrDefault(p => p.ID == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        posts.Remove(postToRemove);
        return Task.CompletedTask;
    }
    
    public Task<Post> GetSingleAsync(int id)
    {
        return Task.FromResult(post);
    }
    
    public IQueryable<Post> GetManyAsync()
    {
        return posts.AsQueryable();
    }
}
        