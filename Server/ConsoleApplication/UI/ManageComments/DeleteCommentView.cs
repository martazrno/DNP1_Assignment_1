using RepositoryContracts;
namespace ConsoleApplication.UI.ManageComments;

public class DeleteCommentView
{
    private readonly ICommentRepository commentRepository;

    public DeleteCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task RunAsync()
    {
        Console.Write("Enter comment ID to delete: ");
        string? input = Console.ReadLine();

        if (!int.TryParse(input, out int commentID))
        {
            Console.WriteLine("Invalid comment ID. Must be a number.");
            return;
        }

        var comment = commentRepository.GetMany().FirstOrDefault(c => c.ID == commentID);
        if (comment == null)
        {
            Console.WriteLine("Comment not found");
            return;
        }

        await commentRepository.DeleteAsync(comment.ID);
        Console.WriteLine($"Comment ' {comment.ID}' deleted.");
    }
}