using Thor.Abstractions.Dtos;

namespace Thor.ErnieBot.Helpers;

public class ErnieBotModelHelper
{
    

    /// <summary>
    /// 获取模型端点
    /// </summary>
    /// <param name="modelId">模型id</param>
    /// <param name="modelType">模型类型，值有 chat,embeddings</param>
    /// <returns></returns>
    public static string GetModelEndpoint(string modelId, string modelType = "chat")
    {
        modelId = modelId ?? string.Empty;

        if (ErnieBotPlatformOptions.ModeInfoDict.ContainsKey(modelId))
        {
            var info = ErnieBotPlatformOptions.ModeInfoDict[modelId];
            if (info.Type == modelType)
            {
                return info.Code;
            }

        }

        return modelId.ToLower().Replace("-", "_");
    }
}