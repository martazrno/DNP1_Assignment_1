using RepositoryContracts;
namespace ConsoleApplication.UI.ManageUsers;

public class ListUserView
{
    private readonly IUserRepository userRepository;

    public ListUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public void Run()
    {
        Console.WriteLine("Listing users...");

        var users = userRepository.GetMany().ToList();

        foreach (var user in users)
        {
            Console.WriteLine($"#{user.ID} - {user.username}");
        }
    }
}