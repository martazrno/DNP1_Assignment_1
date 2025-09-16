using RepositoryContracts;
namespace ConsoleApplication.UI.ManageComments;

public class ListCommentsView
{
    private readonly ICommentRepository commentRepository;

    public ListCommentsView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }
    
    public void Run()
    {
        Console.WriteLine("Listing comments...");

        var comments = commentRepository.GetMany().ToList();

        foreach (var comment in comments)
        {
            Console.WriteLine($"#{comment.ID} - {comment.body}");
        }
    }
}