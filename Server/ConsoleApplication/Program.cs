using ConsoleApplication.UI.ManageComments;
using ConsoleApplication.UI.ManageUsers;
using InMemoryRepositories;

class Program
{
    static async Task Main()
    {
        var userRepository = new UserInMemoryRepository();
        var postRepository = new PostInMemoryRepository();
        var commentRepository = new CommentInMemoryRepository();

        var manageUsers = new ManageUserView(userRepository);
        var managePosts = new ManagePostsView(postRepository);
        var manageComments = new ManageCommentsView(commentRepository);

        while (true)
        {
            Console.WriteLine("1) Manage users");
            Console.WriteLine("2) Manage posts");
            Console.WriteLine("3) Manage comments");
            Console.WriteLine("4) Exit");
            Console.Write("Choice: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await manageUsers.RunAsync();
                    break;
                case "2":
                    await managePosts.RunAsync();
                    break;
                case "3":
                    await manageComments.RunAsync();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}