using Entities;
using RepositoryContracts;
namespace ConsoleApplication.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    
    public async Task RunAsync()
    {
        Console.Write("Username: ");
        string? username = Console.ReadLine();

        Console.Write("Password: ");
        string ? password = Console.ReadLine();

        var user = new User
        {
            username = username ?? string.Empty,
            password = password ?? string.Empty
        };

        await userRepository.AddAsync(user);
        Console.WriteLine($"User #{user.ID} created.");
    }
}