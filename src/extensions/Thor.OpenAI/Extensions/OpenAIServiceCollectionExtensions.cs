using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Thor.Abstractions;
using Thor.Abstractions.Audios;
using Thor.Abstractions.Chats;
using Thor.Abstractions.Embeddings;
using Thor.Abstractions.Images;
using Thor.Abstractions.Realtime;
using Thor.OpenAI.Audios;
using Thor.OpenAI.Chats;
using Thor.OpenAI.Embeddings;
using Thor.OpenAI.Images;
using Thor.OpenAI.Realtime;

namespace Thor.DeepSeek.Extensions;

public static class OpenAIServiceCollectionExtensions
{
    public static IServiceCollection AddOpenAIService(this IServiceCollection services)
    {
        ThorGlobal.PlatformNames.Add(OpenAIPlatformOptions.PlatformName, OpenAIPlatformOptions.PlatformCode);

        ThorGlobal.ModelInfos.Add(OpenAIPlatformOptions.PlatformCode, OpenAIPlatformOptions.ModeInfoDict.Values.ToList());

        services.AddKeyedSingleton<IThorChatCompletionsService, OpenAIChatCompletionsService>(OpenAIPlatformOptions
            .PlatformCode);

        services.AddKeyedSingleton<IThorTextEmbeddingService, OpenAITextEmbeddingService>(
            OpenAIPlatformOptions.PlatformCode);

        services.AddKeyedSingleton<IThorImageService, OpenAIImageService>(OpenAIPlatformOptions.PlatformCode);

        services.AddKeyedSingleton<IThorCompletionsService, OpenAICompletionService>(OpenAIPlatformOptions
            .PlatformCode);

        services.AddKeyedTransient<IThorRealtimeService, OpenAIRealtimeService>(OpenAIPlatformOptions
            .PlatformCode);
        
        services.AddKeyedSingleton<IThorAudioService, OpenAIAudioService>(OpenAIPlatformOptions
            .PlatformCode);

        services.AddHttpClient(OpenAIPlatformOptions.PlatformCode,
                options =>
                {
                    options.Timeout = TimeSpan.FromMinutes(10);

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