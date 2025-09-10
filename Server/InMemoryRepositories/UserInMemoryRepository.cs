using Entities;
using RepositoryContracts;
namespace InMemoryRepositories;
public class UserInMemoryRepository : IUserRepository
{
    private List<User> users;

    public UserInMemoryRepository()
    {
        users = new List<User>();
    }

    public Task<User> AddAsync(User user)
    {
        user.ID = users.Any()
            ? users.Max(p => p.ID) + 1
            : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        User? existingUser = users.SingleOrDefault(p => p.ID == user.ID);
        if (existingUser is null)
            throw new InvalidOperationException($"User with ID '{user.ID}' not found");

        users.Remove(existingUser);
        users.Add(user);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        User? userToRemove = users.SingleOrDefault(p => p.ID == id);
        if (userToRemove is null)
            throw new InvalidOperationException($"User with ID '{id}' not found");

        users.Remove(userToRemove);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        User? user = users.SingleOrDefault(p => p.ID == id);
        if (user is null)
            throw new InvalidOperationException($"User with ID '{id}' not found");

        return Task.FromResult(user);
    }

    public IQueryable<User> GetMany()
    {
        return users.AsQueryable();
    }
        
}