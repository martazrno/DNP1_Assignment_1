using ConsoleApplication.UI.ManagePosts;
using RepositoryContracts;
namespace ConsoleApplication.UI.ManageComments;

public class ManageCommentsView
{
    private readonly ICommentRepository commentRepository;

    public ManageCommentsView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine("--Choose an option--");
            Console.WriteLine("1) Add comment");
            Console.WriteLine("2) Update comment");
            Console.WriteLine("3) Delete comment");
            Console.WriteLine("4) Get a comment");
            Console.WriteLine("5) Get all comment");
            Console.WriteLine("6) Exit program.");
            Console.WriteLine("Choice: ");
            string ? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await new CreateCommentView(commentRepository).RunAsync();
                    break;
                case "2":
                    await new UpdateCommentView(commentRepository).RunAsync();
                    break;
                case "3":
                    await new DeleteCommentView(commentRepository).RunAsync();
                    break;
                case "4":
                    await new GetCommentView(commentRepository).RunAsync();
                    break;
                case "5":
                    new ListCommentsView(commentRepository).Run();
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