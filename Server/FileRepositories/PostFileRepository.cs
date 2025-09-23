using System.Text.Json;
using Entities;
using RepositoryContracts;
namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
	//Here we define the file path. We create a file per entity.
	private readonly string filePath = "posts.json";

	// The constructor ensures there actually is a file.
	// If none exists, a new file is created, with the content of an empty list.
	public PostFileRepository()
	{
		if (!File.Exists(filePath))
		{
			File.WriteAllText(filePath, "[]");
		}
	}
	
	//method for getting the posts
	private async Task<List<Post>> LoadPostsAsync()
	{
		string postsAsJson = await File.ReadAllTextAsync(filePath);
		return JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
	}

	//method for saving the posts
	private async Task SavePostsAsync(List<Post> posts)
	{
		string postsAsJson = JsonSerializer.Serialize(posts);
		await File.WriteAllTextAsync(filePath, postsAsJson);
	}
	
	public async Task<Post> AddAsync(Post post)
	{
		var posts = await LoadPostsAsync();

		int maxId = posts.Count > 0 ? posts.Max(c => c.ID) : 0;
		post.ID = maxId + 1;

		posts.Add(post);
		await SavePostsAsync(posts);

		return post;
	}
	
	public async Task UpdateAsync(Post post)
	{
		var posts = await LoadPostsAsync();

		var existing = posts.FirstOrDefault(c => c.ID == post.ID);
		if (existing == null)
			throw new KeyNotFoundException($"Post with ID {post.ID} not found.");

		// overwrite the existing entity
		int index = posts.IndexOf(existing);
		posts[index] = post;

		await SavePostsAsync(posts);
	}

	public async Task DeleteAsync(int id)
	{
		var posts = await LoadPostsAsync();

		var existing = posts.FirstOrDefault(c => c.ID == id);
		if (existing == null)
			throw new KeyNotFoundException($"Post with ID {id} not found.");

		posts.Remove(existing);
		await SavePostsAsync(posts);
	}

	public async Task<Post> GetSingleAsync(int id)
	{
		var posts = await LoadPostsAsync();
		
		var existing = posts.FirstOrDefault(c => c.ID == id);
		if (existing == null)
			throw new KeyNotFoundException($"Posts with ID {id} not found.");

		return existing;
	}

	public IQueryable<Post> GetMany()
	{
		string postsAsJson = File.ReadAllTextAsync(filePath).Result;
		List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
		return posts.AsQueryable();
	}
}