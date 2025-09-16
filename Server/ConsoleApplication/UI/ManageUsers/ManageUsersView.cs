using RepositoryContracts;
namespace ConsoleApplication.UI.ManageUsers;

public class ManageUserView
{
    private readonly IUserRepository userRepository;

    public ManageUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine("--Choose an option--");
            Console.WriteLine("1) Add user");
            Console.WriteLine("2) Update user");
            Console.WriteLine("3) Delete user");
            Console.WriteLine("4) Get a user");
            Console.WriteLine("5) Get all users");
            Console.WriteLine("6) Exit program.");
            Console.WriteLine("Choice: ");
            string ? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await new CreateUserView(userRepository).RunAsync();
                    break;
                case "2":
                    await new UpdateUserView(userRepository).RunAsync();
                    break;
                case "3":
                    await new DeleteUserView(userRepository).RunAsync();
                    break;
                case "4":
                    await new GetUserView(userRepository).RunAsync();
                    break;
                case "5":
                    new ListUserView(userRepository).Run();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
    }
    
    
    
    
}