using RepositoryContracts;
using System.Linq;

namespace ConsoleApplication.UI.ManageComments;

public class UpdateCommentView
{
    private readonly ICommentRepository commentRepository;

    public UpdateCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public Task RunAsync()
    {
        var comments = commentRepository.GetMany().ToList();

        if (!comments.Any())
        {
            Console.WriteLine("No comments found.");
            return Task.CompletedTask;
        }

        Console.WriteLine("\nAll Comments: ");
        foreach (var comment in comments)
        {
            Console.WriteLine($"ID: {comment.ID} | Body: {comment.body} | User ID: {comment.userID} | Post ID: {comment.postID}");
        }

        return Task.CompletedTask;
    }
}