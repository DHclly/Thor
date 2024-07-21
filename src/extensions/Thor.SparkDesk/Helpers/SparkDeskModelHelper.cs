using Thor.Abstractions.Consts;
using Thor.Abstractions.Dtos;

namespace Thor.SparkDesk.Helpers
{
    /// <summary>
    /// 模型帮助类
    /// </summary>
    public class SparkDeskModelHelper
    {
        /// <summary>
        /// 模型名称映射，兼容现有名称
        /// </summary>
        public static Dictionary<string, string> ModelNameMap = new()
        {
            ["SparkDesk-Lite"] = "general",
            ["SparkDesk-v1.5"] = "general",

            ["SparkDesk-Pro"] = "generalv3",
            ["SparkDesk-v3.1"] = "generalv3",

            ["SparkDesk-Max"] = "generalv3.5",
            ["SparkDesk-v3.5"] = "generalv3.5",

            ["SparkDesk-Ultra"] = "4.0Ultra",
            ["general-4.0-ultra"] = "4.0Ultra",

        };

        /// <summary>
        /// 获取模型编码
        /// </summary>
        /// <param name="modelId">模型id</param>
        /// <param name="modelType">模型类型，值有 chat,embeddings 等等，使用<see cref="ThorModelTypeConst"/> 赋值</param>
        /// <returns></returns>
        public static string GetModelCode(string modelId, string modelType = "chat")
        {
            modelId = modelId ?? string.Empty;

            if (ModelNameMap.ContainsKey(modelId))
            {
                modelId = ModelNameMap[modelId];
            }

            if (SparkDeskPlatformOptions.ModeInfoDict.ContainsKey(modelId))
            {
                var info = SparkDeskPlatformOptions.ModeInfoDict[modelId];
                if (info.Type == modelType)
                {
                    return info.Code;
                }

            }

            return modelId.ToLower();
        }
    }
}
