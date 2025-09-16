using RepositoryContracts;
namespace ConsoleApplication.UI.ManageUsers;

public class GetUserView
{
    private readonly IUserRepository userRepository;

    public GetUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public Task RunAsync()
    {
        Console.Write("Enter username to view: ");
        string? username = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(username))
        {
            Console.WriteLine("Invalid username.");
            return Task.CompletedTask;
        }

        var user = userRepository.GetMany().FirstOrDefault(u => u.username == username);

        if (user == null)
        {
            Console.WriteLine("User not found.");
            return Task.CompletedTask;
        }

        Console.WriteLine($"\nID: {user.ID}");
        Console.WriteLine($"Username: {user.username}");
        Console.WriteLine($"Password: {user.password}");

        return Task.CompletedTask;
    }
}