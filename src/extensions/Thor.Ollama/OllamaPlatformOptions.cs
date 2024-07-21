using Thor.Abstractions.Dtos;

namespace Thor.Ollama
{
    public sealed class OllamaPlatformOptions
    {
        /// <summary>
        /// 平台名称
        /// </summary>
        public const string PlatformName = "Ollama";

        /// <summary>
        /// 平台编码
        /// </summary>
        public const string PlatformCode = "Ollama";

        /// <summary>
        /// 模型信息字典,key：模型编码，value：模型信息
        /// </summary>
        public static Dictionary<string, ThorModelInfo> ModeInfoDict = new()
        {
            ["llama2:latest"] = new ThorModelInfo() { Name = "llama2:latest", Code = "llama2:latest", Type = "chat"},
            ["llama3:latest"] = new ThorModelInfo() { Name = "llama3:latest", Code = "llama3:latest", Type = "chat" },
            ["qwen:4b"] = new ThorModelInfo() { Name = "qwen:4b", Code = "qwen:4b", Type = "chat" },
        };
    }
}
