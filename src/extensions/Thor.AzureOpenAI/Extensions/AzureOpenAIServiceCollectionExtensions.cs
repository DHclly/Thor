using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Thor.Abstractions;
using Thor.Abstractions.Chats;
using Thor.Abstractions.Embeddings;
using Thor.Abstractions.Images;
using Thor.Abstractions.Realtime;
using Thor.AzureOpenAI.Chats;
using Thor.AzureOpenAI.Embeddings;
using Thor.AzureOpenAI.Realtime;

namespace Thor.AzureOpenAI.Extensions;

public static class AzureOpenAIServiceCollectionExtensions
{
    public static IServiceCollection AddAzureOpenAIService(this IServiceCollection services)
    {
        ThorGlobal.PlatformNames.Add(AzureOpenAIPlatformOptions.PlatformName, AzureOpenAIPlatformOptions.PlatformCode);

        ThorGlobal.ModelInfos.Add(AzureOpenAIPlatformOptions.PlatformCode, AzureOpenAIPlatformOptions.ModeInfoDict.Values.ToList());

        services.AddKeyedSingleton<IThorChatCompletionsService, AzureOpenAIChatCompletionsService>(
            AzureOpenAIPlatformOptions.PlatformCode);
        services.AddKeyedSingleton<IThorTextEmbeddingService, AzureOpenAITextEmbeddingGenerationService>(
            AzureOpenAIPlatformOptions.PlatformCode);
        services.AddKeyedSingleton<IThorImageService, AzureOpenAIServiceImageService>(AzureOpenAIPlatformOptions
            .PlatformCode);

        services.AddKeyedSingleton<IThorRealtimeService, AzureOpenAIRealtimeService>(AzureOpenAIPlatformOptions
            .PlatformCode);

        services.AddHttpClient(AzureOpenAIPlatformOptions.PlatformCode,
                options =>
                {
                    options.Timeout = TimeSpan.FromMinutes(6);

                    options.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));
                })
            .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(6),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(6),
                EnableMultipleHttp2Connections = true,
                ConnectTimeout = TimeSpan.FromMinutes(6)
            });
        return services;
    }
}