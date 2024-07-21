using Thor.Abstractions.Dtos;

namespace Thor.OpenAI;

public class OpenAIPlatformOptions
{
    /// <summary>
    /// 平台名称
    /// </summary>
    public const string PlatformName = "OpenAI";

    /// <summary>
    /// 平台编码
    /// </summary>
    public const string PlatformCode = "OpenAI";

    /// <summary>
    /// 模型信息字典,key：模型编码，value：模型信息
    /// </summary>
    public static Dictionary<string, ThorModelInfo> ModeInfoDict = new()
    {
        ["gpt-3.5-turbo"] = new ThorModelInfo() { Name = "gpt-3.5-turbo", Code = "gpt-3.5-turbo", Type = "chat", },
    };
}