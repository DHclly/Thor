using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thor.Abstractions.Dtos;

namespace Thor.Moonshot
{
    /// <summary>
    /// 平台信息
    /// </summary>
    public class MoonshotPlatformOptions
    {
        /// <summary>
        /// 平台名称
        /// </summary>
        public const string PlatformName = "月之暗面";

        /// <summary>
        /// 平台编码
        /// </summary>
        public const string PlatformCode = "KimiOpenAI";

        /// <summary>
        /// 模型信息字典,key：模型编码，value：模型信息
        /// </summary>
        public static Dictionary<string, ThorModelInfo> ModeInfoDict = new()
        {
            ["moonshot-v1-8k"] = new ThorModelInfo() { Name = "moonshot-v1-8k", Code = "moonshot-v1-8k", Type = "chat", HasFunctionCall = true, ContextSize = 8 },
            ["moonshot-v1-32k"] = new ThorModelInfo() { Name = "moonshot-v1-32k", Code = "moonshot-v1-32k", Type = "chat", HasFunctionCall = true, ContextSize = 32 },
            ["moonshot-v1-128k"] = new ThorModelInfo() { Name = "moonshot-v1-128k", Code = "moonshot-v1-128k", Type = "chat", HasFunctionCall = true, ContextSize = 128 },
        };
    }
}
