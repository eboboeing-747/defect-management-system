using Microsoft.Extensions.Configuration;

namespace DmsService;

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
        string validiryDuration = "ValidityDurationMinutes";

        this.SecretKey = ReadConfigurationField(configuration, "SecretKey");
        string JwtValidityDurationMinutesString = ReadConfigurationField(configuration, validiryDuration);

        if (!int.TryParse(JwtValidityDurationMinutesString, out int JwtValidityDurationMinutes))
            throw new Exception($"failed to parse \"{jwtConfig}.{validiryDuration}\" into int");

        this.ValidityDurationMinutes = JwtValidityDurationMinutes;
    }

    public static string ReadConfigurationField(ConfigurationManager configuration, string field)
    {
        string val = configuration[$"JwtConfig:{field}"] ??
            throw new Exception($"no JwtConfig.{field} in \"appsetting.Development.json\"");

        return val;
    }
}
