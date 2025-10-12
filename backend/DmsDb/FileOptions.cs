using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace DmsDb;

public class FileOptions
{
    public List<string> AllowedExtentions { get; set; } = new List<string>();
    public string RootStore { get; set; } = string.Empty;
    public long MaxFileSizeBytes { get; set; }

    // for some reason Environment.ExpandEnvironmentVariables only works for windows variables
    private readonly string defaultRootStore = "%HOME%/.local/share/dms/data";
    private readonly long defaultMaxFileSizeBytes = 67108864;

    public FileOptions()
    {
        this.AllowedExtentions = new List<string>{ ".png", ".jpg", ".jpeg" };
        this.RootStore = Environment.ExpandEnvironmentVariables(this.defaultRootStore);
        this.MaxFileSizeBytes = this.defaultMaxFileSizeBytes;
    }

    public bool IsAllowed(IFormFile file)
    {
        return AllowedExtentions.Contains(Path.GetExtension(file.FileName));
    }

    public FileOptions(ConfigurationManager configuration)
    {
        string fileUploads = "FileUploads";
        string rootStoreField = "RootStore";
        string maxFileSizeBytesField = "MaxFileSizeBytes";

        AllowedExtentions = new List<string>{ ".png", ".jpg", ".jpeg" };

        string? rootStore = configuration[$"{fileUploads}:{rootStoreField}"];

        if (string.IsNullOrWhiteSpace(rootStore))
        {
            Console.WriteLine($"no {fileUploads}.{rootStoreField}: using default path ({defaultRootStore}) instead");
            rootStore = defaultRootStore;
        }

        RootStore = Environment.ExpandEnvironmentVariables(rootStore);
        Console.WriteLine($"[{this.GetType().Name}] root store declared at: '{RootStore}'");

        string? maxFileSizeBytesString = configuration[$"{fileUploads}:{maxFileSizeBytesField}"];

        if (long.TryParse(maxFileSizeBytesString, out long maxFileSizeBytes))
            MaxFileSizeBytes = maxFileSizeBytes;
        else
        {
            Console.WriteLine($"no {fileUploads}.{maxFileSizeBytesField}: using default max file size ({defaultMaxFileSizeBytes}) instead");
            MaxFileSizeBytes = defaultMaxFileSizeBytes;
        }
    }
}
