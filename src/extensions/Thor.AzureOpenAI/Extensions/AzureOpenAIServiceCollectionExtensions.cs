using Microsoft.Extensions.DependencyInjection;
using Thor.Abstractions;
using Thor.Abstractions.Chats;
using Thor.Abstractions.Embeddings;
using Thor.Abstractions.Images;
using Thor.AzureOpenAI.Chats;
using Thor.AzureOpenAI.Embeddings;

namespace Thor.AzureOpenAI.Extensions;

public static class AzureOpenAIServiceCollectionExtensions
{
    public static IServiceCollection AddAzureOpenAIService(this IServiceCollection services)
    {
        ThorGlobal.PlatformNames.Add(AzureOpenAIPlatformOptions.PlatformName, AzureOpenAIPlatformOptions.PlatformCode);

        ThorGlobal.ModelInfos.Add(AzureOpenAIPlatformOptions.PlatformCode, AzureOpenAIPlatformOptions.ModeInfoDict.Values.ToList());

        services.AddKeyedSingleton<IThorChatCompletionsService, AzureOpenAIChatCompletionsService>(AzureOpenAIPlatformOptions.PlatformCode);
        services.AddKeyedSingleton<IThorTextEmbeddingService, AzureOpenAITextEmbeddingGenerationService>(
            AzureOpenAIPlatformOptions.PlatformCode);
        services.AddKeyedSingleton<IThorImageService, AzureOpenAIServiceImageService>(AzureOpenAIPlatformOptions.PlatformCode);

        return services;
    }
}