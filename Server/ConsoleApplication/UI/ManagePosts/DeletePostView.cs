using RepositoryContracts;

namespace ConsoleApplication.UI.ManagePosts;

public class DeletePostView
{
    private readonly IPostRepository postRepository;

    public DeletePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task RunAsync()
    {
        Console.Write("Enter title to delete: ");
        string ? title = Console.ReadLine();

        if (string.IsNullOrEmpty(title))
        {
            Console.WriteLine("Title cannot be empty");
            return;
        }

        var post = postRepository.GetMany().FirstOrDefault(p => p.title == title);
        if (post == null)
        {
            Console.WriteLine("Post not found");
            return;
        }

        await postRepository.DeleteAsync(post.ID);
        Console.WriteLine($"Post ' {post.title}' deleted.");
    }
}