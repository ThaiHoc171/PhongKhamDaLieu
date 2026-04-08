using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml.Style;

public class FileStorage
{
	private readonly IAmazonS3 _s3;
	private readonly IConfiguration _config;

	private readonly string[] _allowedImages = { ".jpg", ".jpeg", ".png", ".webp" };
	private readonly string[] _allowedFiles =
{
	".jpg",".jpeg",".png",".webp",
	".pdf",
	".doc",".docx",
	".xls",".xlsx"
};
	private const long MAX_IMAGE_SIZE = 5 * 1024 * 1024; // 5MB
	private const long MAX_FILE_SIZE = 20 * 1024 * 1024; // 20MB

	public FileStorage(IAmazonS3 s3, IConfiguration config)
	{
		_s3 = s3;
		_config = config;
	}

	public async Task<string> UploadImageAsync(Stream stream, string fileName, string folder, string contentType, string? oldKey = null)
	{
		ValidateImage(stream, folder, fileName);

		return await UploadInternal(stream, fileName, folder, contentType, oldKey);
	}

	public async Task<string> UploadFileAsync(Stream stream,string fileName,string folder,string contentType,string? oldKey = null)
	{
		ValidateFile(stream, folder, fileName);

		return await UploadInternal(stream, fileName, folder, contentType, oldKey);
	}
	private async Task<string> UploadInternal(Stream stream, string fileName, string folder, string contentType, string? oldKey)
	{
		var bucket = _config["AWS:BucketName"];
		var region = _config["AWS:Region"];

		var ext = Path.GetExtension(fileName);
		var key = $"{folder}/{Guid.NewGuid()}{ext}";

		if (!string.IsNullOrWhiteSpace(oldKey))
			await DeleteAsync(oldKey);

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
	private void ValidateImage(Stream stream,string folder, string fileName)
	{
		var ext = Path.GetExtension(fileName).ToLower();
		if (string.IsNullOrWhiteSpace(folder))
			throw new Exception("Folder không hợp lệ");
		if (!_allowedImages.Contains(ext))
			throw new Exception("Chỉ cho phép ảnh jpg, png, webp");

		if (stream.Length > MAX_IMAGE_SIZE)
			throw new Exception("Ảnh quá lớn (max 5MB)");
	}
	private void ValidateFile(Stream stream, string folder, string fileName)
	{
		if (string.IsNullOrWhiteSpace(folder))
			throw new Exception("Folder không hợp lệ");
		var ext = Path.GetExtension(fileName).ToLower();

		if (!_allowedFiles.Contains(ext))
			throw new Exception("File không được hỗ trợ");

		if (stream.Length > MAX_FILE_SIZE)
			throw new Exception("File quá lớn (max 20MB)");
	}
}