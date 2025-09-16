using RepositoryContracts;
namespace ConsoleApplication.UI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository postRepository;

    public ListPostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }
    
    public void Run()
    {
        Console.WriteLine("Listing posts...");

        var posts = postRepository.GetMany().ToList();

        foreach (var post in posts)
        {
            Console.WriteLine($"#{post.ID} - {post.title}");
        }
    }
}