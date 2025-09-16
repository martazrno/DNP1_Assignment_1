using RepositoryContracts;
namespace ConsoleApplication.UI.ManagePosts;

public class GetPostView
{
    private readonly IPostRepository postRepository;

    public GetPostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }
    
    public Task RunAsync()
    {
        Console.Write("Enter title to view: ");
        string? title = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Invalid title.");
            return Task.CompletedTask;
        }

        var post = postRepository.GetMany().FirstOrDefault(p => p.title == title);

        if (post == null)
        {
            Console.WriteLine("Post not found.");
            return Task.CompletedTask;
        }

        Console.WriteLine($"\nID: {post.ID}");
        Console.WriteLine($"Title: {post.title}");
        Console.WriteLine($"Body: {post.body}");

        return Task.CompletedTask;
    }
}