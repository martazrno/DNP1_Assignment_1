using Entities;
using RepositoryContracts;
using System.Linq;
namespace ConsoleApplication.UI.ManageUsers;

public class DeleteUserView
{
    private readonly IUserRepository userRepository;

    public DeleteUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task RunAsync()
    {
        Console.Write("Enter username to delete: ");
        string? username = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(username))
        {
            Console.WriteLine("Invalid username.");
            return;
        }
        
        var user = userRepository.GetMany().FirstOrDefault(u => u.username == username);
        if (user == null)
        {
            Console.WriteLine("User not found.");
            return;
        }
        
        await userRepository.DeleteAsync(user.ID);

        Console.WriteLine($"User '{user.username}' deleted successfully!");
    }
}