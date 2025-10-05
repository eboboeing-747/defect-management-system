using Microsoft.Extensions.Configuration;

namespace DmsDb;

public class JwtOptions
{
    public string SecretKey { get; } = string.Empty;
    public int ValidityDurationMinutes { get; }

    public JwtOptions()
    {
        this.SecretKey = "JwtSecretTokenfa;lifhas;odihf;aosd8hf;aoisdhfoaisdfhfa;sdiofua;soidfja;s";
        this.ValidityDurationMinutes = 720;
    }

    public JwtOptions(ConfigurationManager configuration)
    {
        string jwtConfig = "JwtConfig";
        string secretKey = "SecretKey";
        string validiryDuration = "ValidityDurationMinutes";

        this.SecretKey = configuration[$"{jwtConfig}:{secretKey}"] ??
            throw new Exception($"no \"{jwtConfig}.{secretKey}\" in \"appsetting.Development.json\"");
        this.SecretKey = JwtOptions.ReadConfigurationField(configuration, "SecretKey");
        string JwtValidityDurationMinutesString = configuration[$"{jwtConfig}:{validiryDuration}"] ??
            throw new Exception($"no \"{jwtConfig}.{validiryDuration}\" in \"appsetting.Development.json\"");

        if (!int.TryParse(JwtValidityDurationMinutesString, out int JwtValidityDurationMinutes))
            throw new Exception($"failed to parse \"{jwtConfig}.{validiryDuration}\" into int");

        this.ValidityDurationMinutes = JwtValidityDurationMinutes;
    }

    public static string ReadConfigurationField(ConfigurationManager configuration, string field)
    {
        string val = configuration[$"JwtConfig:{field}"] ??
            throw new Exception($"no \"jwtConfig.{field}\" in \"appsetting.Development.json\"");

        return val;
    }
}
