using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;

public class FileStorage
{
	private readonly IAmazonS3 _s3;
	private readonly IConfiguration _config;

	private readonly string[] _allowedTypes = { ".jpg", ".jpeg", ".png", ".webp" };
	private const long MAX_SIZE = 5 * 1024 * 1024; // 5MB

	public FileStorage(IAmazonS3 s3, IConfiguration config)
	{
		_s3 = s3;
		_config = config;
	}

	public async Task<string> UploadImageAsync(
		Stream stream,
		string fileName,
		string folder,
		string contentType,
		string? oldKey = null)
	{
		ValidateFile(stream, fileName);

		var bucket = _config["AWS:BucketName"];
		var region = _config["AWS:Region"];

		var key = $"{folder}/{Guid.NewGuid()}{Path.GetExtension(fileName)}";

		// delete ảnh cũ nếu có
		if (!string.IsNullOrWhiteSpace(oldKey))
		{
			await DeleteAsync(oldKey);
		}

		var request = new PutObjectRequest
		{
			BucketName = bucket,
			Key = key,
			InputStream = stream,
			ContentType = contentType
		};

		await _s3.PutObjectAsync(request);

		return $"https://{bucket}.s3.{region}.amazonaws.com/{key}";
	}

	public async Task DeleteAsync(string key)
	{
		var bucket = _config["AWS:BucketName"];

		await _s3.DeleteObjectAsync(new DeleteObjectRequest
		{
			BucketName = bucket,
			Key = key
		});
	}

	private void ValidateFile(Stream stream, string fileName)
	{
		var ext = Path.GetExtension(fileName).ToLower();

		if (!_allowedTypes.Contains(ext))
			throw new Exception("File type không hợp lệ (chỉ cho jpg, png, webp)");

		if (stream.Length > MAX_SIZE)
			throw new Exception("File quá lớn (max 5MB)");
	}
}