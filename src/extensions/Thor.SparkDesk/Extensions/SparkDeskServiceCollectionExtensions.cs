using Microsoft.Extensions.DependencyInjection;
using Thor.Abstractions;
using Thor.Abstractions.Chats;
using Thor.Abstractions.Embeddings;
using Thor.Abstractions.Images;
using Thor.SparkDesk.Chats;
using Thor.SparkDesk.Embeddings;
using Thor.SparkDesk.Helpers;
using Thor.SparkDesk.Images;

namespace Thor.SparkDesk.Extensions;

public static class SparkDeskServiceCollectionExtensions
{
    public static IServiceCollection AddSparkDeskService(this IServiceCollection services)
    {
        //添加 Ollama 平台支持
        ThorGlobal.PlatformNames.Add(SparkDeskPlatformOptions.PlatformName, SparkDeskPlatformOptions.PlatformCode);

        // 添加平台支持模型列表
        ThorGlobal.ModelInfos.Add(SparkDeskPlatformOptions.PlatformCode, SparkDeskPlatformOptions.ModeInfoDict.Values.ToList());

        services.AddKeyedSingleton<IThorChatCompletionsService, SparkDeskChatCompletionsService>(SparkDeskPlatformOptions.PlatformCode);

        services.AddKeyedSingleton<IThorImageService, SparkDeskImageService>(SparkDeskPlatformOptions.PlatformCode);

        services.AddKeyedSingleton<IThorTextEmbeddingService, SparkDeskTextEmbeddingService>(SparkDeskPlatformOptions.PlatformCode);

        return services;
    }
}