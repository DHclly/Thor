using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Thor.Abstractions;
using Thor.Abstractions.Chats;
using Thor.Abstractions.Embeddings;
using Thor.Abstractions.Images;
using Thor.OpenAI.Chats;
using Thor.OpenAI.Embeddings;
using Thor.OpenAI.Images;

namespace Thor.OpenAI.Extensions;

public static class OpenAIServiceCollectionExtensions
{
    public static IServiceCollection AddOpenAIService(this IServiceCollection services)
    {
        ThorGlobal.PlatformNames.Add(OpenAIPlatformOptions.PlatformName, OpenAIPlatformOptions.PlatformCode);

        ThorGlobal.ModelInfos.Add(OpenAIPlatformOptions.PlatformCode, OpenAIPlatformOptions.ModeInfoDict.Values.ToList());

        services.AddKeyedSingleton<IThorChatCompletionsService, OpenAIChatCompletionsService>(OpenAIPlatformOptions.PlatformCode);

        services.AddKeyedSingleton<IThorTextEmbeddingService, OpenAITextEmbeddingService>(
            OpenAIPlatformOptions.PlatformCode);

        services.AddKeyedSingleton<IThorImageService, OpenAIImageService>(OpenAIPlatformOptions.PlatformCode);

        services.AddKeyedSingleton<IThorCompletionsService, OpenAICompletionService>(OpenAIPlatformOptions
            .PlatformCode);

        services.AddHttpClient(OpenAIPlatformOptions.PlatformCode,
                options => { options.Timeout = TimeSpan.FromMinutes(6); })
            .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip,
                EnableMultipleHttp2Connections = true,
            });

        return services;
    }
}