﻿using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Thor.Abstractions;
using Thor.Abstractions.Chats;
using Thor.Abstractions.Embeddings;
using Thor.Abstractions.Images;
using Thor.Moonshot.Chats;
using Thor.Moonshot.Embeddings;
using Thor.Moonshot.Images;

namespace Thor.Moonshot.Extensions;

/// <summary>
/// 月之暗面服务扩展
/// </summary>
public static class MoonshotServiceCollectionExtensions
{
    /// <summary>
    /// 添加月之暗面平台支持
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddMoonshotService(this IServiceCollection services)
    {
        ThorGlobal.PlatformNames.Add(MoonshotPlatformOptions.PlatformName, MoonshotPlatformOptions.PlatformCode);

        ThorGlobal.ModelInfos.Add(MoonshotPlatformOptions.PlatformCode, MoonshotPlatformOptions.ModeInfoDict.Values.ToList());

        services.AddKeyedSingleton<IThorChatCompletionsService, MoonshotChatCompletionsService>(MoonshotPlatformOptions.PlatformCode);

        services.AddKeyedSingleton<IThorTextEmbeddingService, MoonshotTextEmbeddingService>(
            MoonshotPlatformOptions.PlatformCode);

        services.AddKeyedSingleton<IThorImageService, MoonshotImageService>(MoonshotPlatformOptions.PlatformCode);

        services.AddKeyedSingleton<IThorCompletionsService, MoonshotCompletionService>(MoonshotPlatformOptions
            .PlatformCode);

        services.AddHttpClient(MoonshotPlatformOptions.PlatformCode,
                options => { options.Timeout = TimeSpan.FromMinutes(6); })
            .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                MaxConnectionsPerServer = 300,
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(10),
                PooledConnectionLifetime = TimeSpan.FromMinutes(30),
                EnableMultipleHttp2Connections = true,
            });

        return services;
    }
}