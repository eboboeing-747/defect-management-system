namespace DmsDb.Service;
using Microsoft.AspNetCore.Http;

public class FileService
{
    private readonly FileOptions _fileOptions;

    public FileService(FileOptions fileOptions)
    {
        this._fileOptions = fileOptions;
    }

    public bool CheckRootStorage()
    {
        string fullDirPath = _fileOptions.RootStore;

        if (!Directory.Exists(fullDirPath))
        {
            Console.WriteLine($"[checkdir] creating {fullDirPath}");
            Directory.CreateDirectory(fullDirPath);
        }

        return Directory.Exists(fullDirPath);
    }

    public void CreateFile(IFormFile fileToCreate)
    {
        string postfix = $"{Path.GetRandomFileName()}{Path.GetExtension(fileToCreate.FileName)}";
        string fullPath = Path.Combine(_fileOptions.RootStore, postfix);

        Console.WriteLine($"saving file to {fullPath}");

        using (FileStream fs = File.Create(fullPath))
        {
            fileToCreate.CopyToAsync(fs);
        }
    }
}
