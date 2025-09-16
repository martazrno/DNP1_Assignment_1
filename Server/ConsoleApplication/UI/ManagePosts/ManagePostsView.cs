using ConsoleApplication.UI.ManagePosts;
using RepositoryContracts;
namespace ConsoleApplication.UI.ManageUsers;

public class ManagePostsView
{
    private readonly IPostRepository postRepository;

    public ManagePostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine("--Choose an option--");
            Console.WriteLine("1) Add post");
            Console.WriteLine("2) Update post");
            Console.WriteLine("3) Delete post");
            Console.WriteLine("4) Get a post");
            Console.WriteLine("5) Get all posts");
            Console.WriteLine("6) Exit program.");
            Console.WriteLine("Choice: ");
            string ? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await new CreatePostView(postRepository).RunAsync();
                    break;
                case "2":
                    await new UpdatePostView(postRepository).RunAsync();
                    break;
                case "3":
                    await new DeletePostView(postRepository).RunAsync();
                    break;
                case "4":
                    await new GetPostView(postRepository).RunAsync();
                    break;
                case "5":
                    new ListPostsView(postRepository).Run();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
    }
    
    
    
    
}