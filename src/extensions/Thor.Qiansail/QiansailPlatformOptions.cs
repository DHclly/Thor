using Thor.Abstractions.Dtos;

namespace Thor.Qiansail
{
    public class QiansailPlatformOptions
    {
        /// <summary>
        /// 平台名称
        /// </summary>
        public const string PlatformName = "阿里云百炼";

        /// <summary>
        /// 平台编码
        /// </summary>
        public const string PlatformCode = "Qiansail";

        /// 模型信息字典,key：模型编码，value：模型信息
        /// </summary>
        public static Dictionary<string, ThorModelInfo> ModeInfoDict = new()
        {
            ["qwen-plus"] = new ThorModelInfo() { Name = "qwen-plus", Code = "qwen-plus", Type = "chat" },
            ["qwen-max"] = new ThorModelInfo() { Name = "qwen-max", Code = "qwen-max", Type = "chat" },
            ["qwen-turbo"] = new ThorModelInfo() { Name = "qwen-turbo", Code = "qwen-turbo", Type = "chat" },
        };
    }
}
