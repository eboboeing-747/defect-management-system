namespace DmsDb.Service;

using System.Net;
using DmsDb.Repository;
using Microsoft.AspNetCore.Http;

public class FileService
{
    private readonly FileOptions _fileOptions;
    private readonly ImageRepository _imageRepository;

    public FileService(
        FileOptions fileOptions,
        ImageRepository imageRepository
    ) {
        this._fileOptions = fileOptions;
        this._imageRepository = imageRepository;
    }

    public bool CheckRootStorage()
    {
        string fullDirPath = _fileOptions.RootStore;

        if (!Directory.Exists(fullDirPath))
        {
            Console.WriteLine($"[{this.GetType().Name}] attempting to create root store at '{fullDirPath}'");
            Directory.CreateDirectory(fullDirPath);
        }
        else
            return true;

        if (Directory.Exists(fullDirPath))
            Console.WriteLine($"[{this.GetType().Name}] created root store at '{fullDirPath}'");
        else
        {
            Console.WriteLine($"[{this.GetType().Name}] failed to create root store at '{fullDirPath}'");
            return false;
        }

        return true;
    }

    public async Task<HttpStatusCode> Validate(List<IFormFile> files)
    {
        foreach (IFormFile file in files)
        {
            HttpStatusCode status = await Validate(file);

            if (status != HttpStatusCode.OK)
                return status;
        }

        return HttpStatusCode.OK;
    }

    public async Task<HttpStatusCode> Validate(IFormFile file)
    {
        if (file.Length > _fileOptions.MaxFileSizeBytes)
            return HttpStatusCode.RequestEntityTooLarge;

        if (!_fileOptions.IsAllowed(file))
            return HttpStatusCode.UnsupportedMediaType;

        return HttpStatusCode.OK;
    }

    public async Task CreateFile(IFormFile fileToCreate, Guid entityId)
    {
        string newFileName = $"{Path.GetRandomFileName()}{Path.GetExtension(fileToCreate.FileName)}";
        string fullPath = Path.Combine(_fileOptions.RootStore, newFileName);

        Console.WriteLine($"saving file to {fullPath}");

        await _imageRepository.Create(newFileName, entityId);

        using (FileStream fs = File.Create(fullPath))
            await fileToCreate.CopyToAsync(fs);
    }
}
