using Entities;
using RepositoryContracts;
namespace ConsoleApplication.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;

    public CreatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }
    
    public async Task RunAsync()
    {
        Console.Write("Enter user ID: ");
        if (!int.TryParse(Console.ReadLine(), out int userID))
        {
            Console.WriteLine("Invalid user ID. Must be a number.");
            return;
        }

        Console.Write("Write title: ");
        string? title = Console.ReadLine();

        Console.Write("Write body text: ");
        string? body = Console.ReadLine();

        var post = new Post
        {
            userID = userID,
            title = title ?? string.Empty,
            body = body ?? string.Empty
        };

        await postRepository.AddAsync(post);
        Console.WriteLine($"Post #{post.ID} created.");
    }
    
}