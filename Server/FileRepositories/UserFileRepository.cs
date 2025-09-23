using System.Text.Json;
using Entities;
using RepositoryContracts;
namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
	//Here we define the file path. We create a file per entity.
	private readonly string filePath = "users.json";

	// The constructor ensures there actually is a file.
	// If none exists, a new file is created, with the content of an empty list.
	public UserFileRepository()
	{
		if (!File.Exists(filePath))
		{
			File.WriteAllText(filePath, "[]");
		}
	}
	
	//method for getting the users
	private async Task<List<User>> LoadUsersAsync()
	{
		string usersAsJson = await File.ReadAllTextAsync(filePath);
		return JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
	}

	//method for saving the users
	private async Task SaveUsersAsync(List<User> users)
	{
		string usersAsJson = JsonSerializer.Serialize(users);
		await File.WriteAllTextAsync(filePath, usersAsJson);
	}
	
	public async Task<User> AddAsync(User user)
	{
		var users = await LoadUsersAsync();

		int maxId = users.Count > 0 ? users.Max(c => c.ID) : 0;
		user.ID = maxId + 1;

		users.Add(user);
		await SaveUsersAsync(users);

		return user;
	}
	
	public async Task UpdateAsync(User user)
	{
		var users = await LoadUsersAsync();

		var existing = users.FirstOrDefault(c => c.ID == user.ID);
		if (existing == null)
			throw new KeyNotFoundException($"User with ID {user.ID} not found.");

		// overwrite the existing entity
		int index = users.IndexOf(existing);
		users[index] = user;

		await SaveUsersAsync(users);
	}

	public async Task DeleteAsync(int id)
	{
		var users = await LoadUsersAsync();

		var existing = users.FirstOrDefault(c => c.ID == id);
		if (existing == null)
			throw new KeyNotFoundException($"User with ID {id} not found.");

		users.Remove(existing);
		await SaveUsersAsync(users);
	}

	public async Task<User> GetSingleAsync(int id)
	{
		var users = await LoadUsersAsync();
		
		var existing = users.FirstOrDefault(c => c.ID == id);
		if (existing == null)
			throw new KeyNotFoundException($"User with ID {id} not found.");

		return existing;
	}

	public IQueryable<User> GetMany()
	{
		string usersAsJson = File.ReadAllTextAsync(filePath).Result;
		List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
		return users.AsQueryable();
	}
}