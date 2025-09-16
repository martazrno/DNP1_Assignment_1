using RepositoryContracts;
using ConsoleApplication.UI.ManageUsers;
namespace ConsoleApplication.UI;

public class CliApp
{
    private readonly CreateUserView createUserView;
    // you can add other views later (CreatePostView, etc.)

    public CliApp(IUserRepository userRepo,
        IPostRepository postRepo,
        ICommentRepository commentRepo)
    {
        createUserView = new CreateUserView(userRepo);
        // save postRepo / commentRepo for later if needed
    }

    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("\n--- Forum CLI ---");
            Console.WriteLine("1) Create user");
            Console.WriteLine("0) Exit");
            Console.Write("> ");

            switch (Console.ReadLine())
            {
                case "1":
                    await createUserView.RunAsync();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Unknown choice");
                    break;
            }
        }
    }
}