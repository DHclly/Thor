using Thor.Abstractions.Dtos;

namespace Thor.SparkDesk;

public sealed class SparkDeskPlatformOptions
{
    /// <summary>
    /// 平台名称
    /// </summary>
    public const string PlatformName = "星火大模型";

    /// <summary>
    /// 平台编码
    /// </summary>
    public const string PlatformCode = "SparkDesk";

    /// <summary>
    /// 平台基础Url
    /// </summary>
    public const string PlatformBaseUrl = "https://spark-api.xf-yun.com";


    /// <summary>
    /// 模型信息字典,key：模型编码，value：模型信息
    /// </summary>
    public static Dictionary<string, ThorModelInfo> ModeInfoDict = new()
    {
        ["general"] = new ThorModelInfo() { Name = "SparkDesk-Lite", Code = "general", Type = "chat" },
        //generalv2 不可用，报错
        ["generalv3"] = new ThorModelInfo() { Name = "SparkDesk-Pro", Code = "generalv3", Type = "chat" },
        ["generalv3.5"] = new ThorModelInfo() { Name = "SparkDesk-Max", Code = "generalv3.5", Type = "chat", HasFunctionCall = true },
        ["4.0Ultra"] = new ThorModelInfo() { Name = "SparkDesk-Ultra", Code = "4.0Ultra", Type = "chat", HasFunctionCall = true },
    };
}