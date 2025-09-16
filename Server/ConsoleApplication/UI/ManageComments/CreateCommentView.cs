using Entities;
using RepositoryContracts;
namespace ConsoleApplication.UI.ManageComments;

public class CreateCommentView
{
    private readonly ICommentRepository commentRepository;

    public CreateCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }
    
    public async Task RunAsync()
    {
        Console.Write("Write comment: ");
        string? body = Console.ReadLine();

        Console.Write("Write userID: ");
        int userID = int.Parse(Console.ReadLine());
        
        Console.Write("Write postID: ");
        int postID = int.Parse(Console.ReadLine());

        var comment = new Comment
        {
            body = body ?? string.Empty,
            postID = postID,
            userID = userID
        };

        await commentRepository.AddAsync(comment);
        Console.WriteLine($"Comment #{comment.ID} created.");
    }
    
}