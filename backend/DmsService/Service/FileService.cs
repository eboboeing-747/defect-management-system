namespace DmsService.Service;

using System.Net;
using DmsDb.Repository;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using DmsDb.Entity;

public class FileService
{
    private readonly FileOptions _fileOptions;
    private readonly EstateObjectImageRepository _eoImageRepository;

    public FileService(
        FileOptions fileOptions,
        EstateObjectImageRepository imageRepository
    ) {
        this._fileOptions = fileOptions;
        this._eoImageRepository = imageRepository;
    }

    public bool CheckRootStorage()
    {
        string fullDirPath = _fileOptions.RootStore;

        if (!Directory.Exists(fullDirPath))
        {
            Console.WriteLine($"[{GetType().Name}] attempting to create root store at '{fullDirPath}'");
            Directory.CreateDirectory(fullDirPath);
        }
        else
            return true;

        if (Directory.Exists(fullDirPath))
            Console.WriteLine($"[{GetType().Name}] created root store at '{fullDirPath}'");
        else
        {
            Console.WriteLine($"[{GetType().Name}] failed to create root store at '{fullDirPath}'");
            return false;
        }

        return true;
    }

    public HttpStatusCode Validate(List<IFormFile> files)
    {
        foreach (IFormFile file in files)
        {
            HttpStatusCode status = Validate(file);

            if (status != HttpStatusCode.OK)
                return status;
        }

        return HttpStatusCode.OK;
    }

    public HttpStatusCode Validate(IFormFile file)
    {
        if (file.Length > _fileOptions.MaxFileSizeBytes)
            return HttpStatusCode.RequestEntityTooLarge;

        if (!_fileOptions.IsAllowed(file))
            return HttpStatusCode.UnsupportedMediaType;

        return HttpStatusCode.OK;
    }

    public async Task<HttpStatusCode> CreateFiles(Guid estateObjectId, List<IFormFile> files)
    {
        List<EstateObjectImageEntity> newImages = [];

        foreach (IFormFile file in files) {
            string newFilePath = await CreatePhysicalFile(file);

            EstateObjectImageEntity imageEntity = new() {
                Id = Guid.NewGuid(),
                Path = newFilePath,
                Name = null
            };

            newImages.Add(imageEntity);
        }

        await _eoImageRepository.Create(estateObjectId, newImages);
        return HttpStatusCode.OK;
    }

    public async Task<string> CreatePhysicalFile(IFormFile fileToCreate)
    {
        string dateTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
        string newFileName = $"[{dateTime}]{Path.GetRandomFileName()}{Path.GetExtension(fileToCreate.FileName)}";
        string fullPath = GetFullPath(newFileName);

        using (FileStream fs = File.Create(fullPath))
            await fileToCreate.CopyToAsync(fs);

        return newFileName;
    }

    public string GetFullPath(string filename)
    {
        return Path.Combine(_fileOptions.RootStore, filename);
    }

    public static bool Exists(string filename)
    {
        return Path.Exists(filename);
    }
}
