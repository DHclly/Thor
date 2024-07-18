﻿using System.Text.Json.Serialization;
using Thor.Abstractions.Dtos;
using Thor.Abstractions.ObjectModels.ObjectModels.SharedModels;

namespace Thor.Abstractions.ObjectModels.ObjectModels.ResponseModels;

public record EditCreateResponse : ThorBaseResponse, IOpenAiModels.ICreatedAt
{
    [JsonPropertyName("model")] public string Model { get; set; }

    [JsonPropertyName("choices")] public List<ChoiceResponse> Choices { get; set; }

    [JsonPropertyName("usage")] public ThorUsageResponse Usage { get; set; }

    [JsonPropertyName("created")] public int CreatedAt { get; set; }
}