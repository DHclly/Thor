using Thor.Abstractions.Dtos;

namespace Thor.Hunyuan;

public class HunyuanPlatformOptions
{
    /// <summary>
    /// 平台名称
    /// </summary>
    public const string PlatformName = "腾讯混元大模型";

    /// <summary>
    /// 平台编码
    /// </summary>
    public const string PlatformCode = "Hunyuan";
    /// <summary>
    /// 模型信息字典,key：模型编码，value：模型信息
    /// </summary>
    public static Dictionary<string, ThorModelInfo> ModeInfoDict = new()
    {
        ["hunyuan-lite"] = new ThorModelInfo() { Name = "hunyuan-lite", Code = "hunyuan-lite", Type = "chat" },
        ["hunyuan-standard"] = new ThorModelInfo() { Name = "hunyuan-standard", Code = "hunyuan-standard", Type = "chat" },
        ["hunyuan-pro"] = new ThorModelInfo() { Name = "hunyuan-pro", Code = "hunyuan-pro", Type = "chat" },
        ["hunyuan-role"] = new ThorModelInfo() { Name = "hunyuan-role", Code = "hunyuan-role", Type = "chat" },
        ["hunyuan-code"] = new ThorModelInfo() { Name = "hunyuan-code", Code = "hunyuan-code", Type = "chat" },
    };
}