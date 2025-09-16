using Entities;
using RepositoryContracts;
using System.Linq;

namespace ConsoleApplication.UI.ManageUsers;

public class UpdateUserView
{
    private readonly IUserRepository userRepository;

    public UpdateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    
    public async Task RunAsync()
    {
        Console.Write("Enter username to update: ");
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

        Console.Write($"New username (current: {user.username}): ");
        string? newUsername = Console.ReadLine();

        Console.Write($"New password (current: {user.password}): ");
        string? newPassword = Console.ReadLine();
        
        user.username = string.IsNullOrWhiteSpace(newUsername) ? user.username : newUsername;
        user.password = string.IsNullOrWhiteSpace(newPassword) ? user.password : newPassword;

        await userRepository.UpdateAsync(user);

        Console.WriteLine($"User '{user.username}' updated.");
    }

}