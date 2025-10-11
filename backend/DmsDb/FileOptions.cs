using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace DmsDb;

public class FileOptions
{
    public List<string> AllowedExtentions { get; set; } = new List<string>();
    public string RootStore { get; set; } = string.Empty;
    public long MaxFileSizeBytes { get; set; }

    // private readonly ILogger<FileOptions> _logger;

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

    // public FileOptions(ConfigurationManager configuration, ILogger<FileOptions> logger)
    // {
    //     this._logger = logger;
    //
    //     string fileUploads = "FileUploads";
    //     string rootStore = "RootStore";
    //     string maxFileSizeBytes = "MaxFileSizeBytes";
    //
    //     string? rootStore1 = configuration[$"{fileUploads}:{rootStore}"];
    //
    //     if (string.IsNullOrEmpty(rootStore1))
    //         this._logger.LogInformation($"no {fileUploads}.{rootStore}: using default path ({defaultRootStore}) instead");
    //     else
    //         this.RootStore = this.defaultRootStore;
    //
    //     long? maxsizebruh = configuration[$"{fileUploads}:{maxFileSizeBytes}"];
    //
    //     if (maxsizebruh == null)
    //         this._logger.LogInformation($"no {fileUploads}.{maxFileSizeBytes}: using default max file size ({defaultMaxFileSizeBytes}) instead");
    //     else
    //         this.MaxFileSizeBytes = this.defaultMaxFileSizeBytes;
    // }
}
