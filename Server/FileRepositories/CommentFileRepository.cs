using System.Text.Json;
using Entities;
using RepositoryContracts;
namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
	//Here we define the file path. We create a file per entity.
	private readonly string filePath = "comments.json";

	// The constructor ensures there actually is a file.
	// If none exists, a new file is created, with the content of an empty list.
	public CommentFileRepository()
	{
		if (!File.Exists(filePath))
		{
			File.WriteAllText(filePath, "[]");
		}
	}
	
	//method for getting the comments
	private async Task<List<Comment>> LoadCommentsAsync()
	{
		string commentsAsJson = await File.ReadAllTextAsync(filePath);
		return JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
	}

	//method for saving the comments
	private async Task SaveCommentsAsync(List<Comment> comments)
	{
		string commentsAsJson = JsonSerializer.Serialize(comments);
		await File.WriteAllTextAsync(filePath, commentsAsJson);
	}
	
	public async Task<Comment> AddAsync(Comment comment)
	{
		var comments = await LoadCommentsAsync();

		int maxId = comments.Count > 0 ? comments.Max(c => c.ID) : 0;
		comment.ID = maxId + 1;

		comments.Add(comment);
		await SaveCommentsAsync(comments);

		return comment;
	}
	
	public async Task UpdateAsync(Comment comment)
	{
		var comments = await LoadCommentsAsync();

		var existing = comments.FirstOrDefault(c => c.ID == comment.ID);
		if (existing == null)
			throw new KeyNotFoundException($"Comment with ID {comment.ID} not found.");

		// overwrite the existing entity
		int index = comments.IndexOf(existing);
		comments[index] = comment;

		await SaveCommentsAsync(comments);
	}

	public async Task DeleteAsync(int id)
	{
		var comments = await LoadCommentsAsync();

		var existing = comments.FirstOrDefault(c => c.ID == id);
		if (existing == null)
			throw new KeyNotFoundException($"Comment with ID {id} not found.");

		comments.Remove(existing);
		await SaveCommentsAsync(comments);
	}

	public async Task<Comment> GetSingleAsync(int id)
	{
		var comments = await LoadCommentsAsync();
		
		var existing = comments.FirstOrDefault(c => c.ID == id);
		if (existing == null)
			throw new KeyNotFoundException($"Comment with ID {id} not found.");

		return existing;
	}

	public IQueryable<Comment> GetMany()
	{
		string commentsAsJson = File.ReadAllTextAsync(filePath).Result;
		List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
		return comments.AsQueryable();
	}
}