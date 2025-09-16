using RepositoryContracts;
namespace ConsoleApplication.UI.ManageComments;

public class GetCommentView
{
    private readonly ICommentRepository commentRepository;

    public GetCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }
    
    public Task RunAsync()
    {
        Console.Write("Enter commentID to view: ");
        string? input = Console.ReadLine();

        if (!int.TryParse(input, out int commentID))
        {
            Console.WriteLine("Invalid comment ID. Must be a number.");
            return Task.CompletedTask;
        }

        var comment = commentRepository.GetMany().FirstOrDefault(c => c.ID == commentID);

        if (comment == null)
        {
            Console.WriteLine("Comment not found.");
            return Task.CompletedTask;
        }

        Console.WriteLine($"\nID: {comment.ID}");
        Console.WriteLine($"Body: {comment.body}");

        return Task.CompletedTask;
    }
}