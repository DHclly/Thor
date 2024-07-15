﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using Thor.Abstractions.ObjectModels.ObjectModels;
using Thor.Abstractions.ObjectModels.ObjectModels.RequestModels;
using Thor.Abstractions.ObjectModels.ObjectModels.SharedModels;

namespace Thor.Abstractions.Chats.Dtos;

/// <summary>
/// 对话补全请求参数对象
/// </summary>
public class ThorChatCompletionsRequest : IOpenAiModels.ITemperature, IOpenAiModels.IModel, IOpenAiModels.IUser
{
    public ThorChatCompletionsRequest()
    {
        Messages = new List<ThorChatMessage>();
    }

    /// <summary>
    /// 包含迄今为止对话的消息列表
    /// </summary>
    [JsonPropertyName("messages")]
    public List<ThorChatMessage> Messages { get; set; }

    /// <summary>
    /// 模型唯一编码值，如 gpt-4，gpt-3.5-turbo,moonshot-v1-8k，看底层具体平台定义
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; }

    /// <summary>
    /// 温度采样的替代方法称为核采样，介于 0 和 1 之间，其中模型考虑具有 top_p 概率质量的标记的结果。
    /// 因此 0.1 意味着仅考虑包含前 10% 概率质量的标记。
    /// 我们通常建议更改此项或 temperature ，但不要同时更改两者。
    /// </summary>
    [JsonPropertyName("top_p")]
    public float? TopP { get; set; }

    /// <summary>
    /// 使用什么采样温度，介于 0 和 2 之间。
    /// 较高的值（如 0.8）将使输出更加随机，而较低的值（如 0.2）将使其更加集中和确定性。
    /// 我们通常建议更改此项或 top_p ，但不要同时更改两者。
    /// </summary>
    [JsonPropertyName("temperature")]
    public float? Temperature { get; set; }

    /// <summary>
    /// 为每条输入消息生成多少个聊天完成选项。请注意，您将根据所有选项生成的代币数量付费。将 n 保留为 1 以最大限度地降低成本。
    /// </summary>
    [JsonPropertyName("n")]
    public int? N { get; set; } = 1;

    /// <summary>
    /// 如果设置，将发送部分消息增量，就像在 ChatGPT 中一样。
    /// 令牌可用时将作为仅数据服务器发送事件发送，流由 data: [DONE] 消息终止。
    /// </summary>
    [JsonPropertyName("stream")]
    public bool? Stream { get; set; }

    /// <summary>
    ///     Up to 4 sequences where the API will stop generating further tokens. The returned text will not contain the stop
    ///     sequence.
    /// </summary>
    [JsonIgnore]
    public string? Stop { get; set; }

    /// <summary>
    ///     Up to 4 sequences where the API will stop generating further tokens. The returned text will not contain the stop
    ///     sequence.
    /// </summary>
    [JsonIgnore]
    public IList<string>? StopAsList { get; set; }

    [JsonPropertyName("stop")]
    public IList<string>? StopCalculated
    {
        get
        {
            if (Stop != null && StopAsList != null)
            {
                throw new ValidationException(
                    "Stop and StopAsList can not be assigned at the same time. One of them is should be null.");
            }

            if (Stop != null)
            {
                return new List<string> { Stop };
            }

            return StopAsList;
        }
    }

    /// <summary>
    ///     The maximum number of tokens allowed for the generated answer. By default, the number of tokens the model can
    ///     return will be (4096 - prompt tokens).
    /// </summary>
    /// <see href="https://platform.openai.com/docs/api-reference/completions/create#completions/create-max_tokens" />
    [JsonPropertyName("max_tokens")]
    public int? MaxTokens { get; set; }

    /// <summary>
    ///     Number between -2.0 and 2.0. Positive values penalize new tokens based on whether they appear in the text so far,
    ///     increasing the model's likelihood to talk about new topics.
    /// </summary>
    /// <seealso href="https://platform.openai.com/docs/api-reference/parameter-details" />
    [JsonPropertyName("presence_penalty")]
    public float? PresencePenalty { get; set; }


    /// <summary>
    ///     Number between -2.0 and 2.0. Positive values penalize new tokens based on their existing frequency in the text so
    ///     far, decreasing the model's likelihood to repeat the same line verbatim.
    /// </summary>
    /// <seealso href="https://platform.openai.com/docs/api-reference/parameter-details" />
    [JsonPropertyName("frequency_penalty")]
    public float? FrequencyPenalty { get; set; }

    /// <summary>
    ///     Modify the likelihood of specified tokens appearing in the completion.
    ///     Accepts a json object that maps tokens(specified by their token ID in the GPT tokenizer) to an associated bias
    ///     value from -100 to 100. You can use this tokenizer tool (which works for both GPT-2 and GPT-3) to convert text to
    ///     token IDs. Mathematically, the bias is added to the logits generated by the model prior to sampling. The exact
    ///     effect will vary per model, but values between -1 and 1 should decrease or increase likelihood of selection; values
    ///     like -100 or 100 should result in a ban or exclusive selection of the relevant token.
    ///     As an example, you can pass { "50256": -100}
    ///     to prevent the endoftext token from being generated.
    /// </summary>
    /// <seealso href="https://platform.openai.com/tokenizer?view=bpe" />
    [JsonPropertyName("logit_bias")]
    public object? LogitBias { get; set; }

    /// <summary>
    ///     A list of functions the model may generate JSON inputs for.
    /// </summary>
    [JsonIgnore]
    public IList<ToolDefinition>? Tools { get; set; }


    [JsonIgnore] public object? ToolsAsObject { get; set; }

    /// <summary>
    ///     A list of tools the model may call. Currently, only functions are supported as a tool. Use this to provide a list
    ///     of functions the model may generate JSON inputs for.
    /// </summary>
    [JsonPropertyName("tools")]
    public object? ToolsCalculated
    {
        get
        {
            if (ToolsAsObject != null && Tools != null)
            {
                throw new ValidationException(
                    "ToolsAsObject and Tools can not be assigned at the same time. One of them is should be null.");
            }

            return Tools ?? ToolsAsObject;
        }
        set
        {
            if (value is JsonElement jsonElement)
            {
                if (jsonElement.ValueKind == JsonValueKind.Array)
                {
                    Tools = JsonSerializer.Deserialize<List<ToolDefinition>>(jsonElement.GetRawText(), ThorJsonSerializer.DefaultOptions);
                }
                else
                {
                    ToolsAsObject = jsonElement;
                }
            }
        }
    }

    /// <summary>
    ///     Controls which (if any) function is called by the model. none means the model will not call a function and instead
    ///     generates a message. auto means the model can pick between generating a message or calling a function. Specifying
    ///     a particular function via {"type: "function", "function": {"name": "my_function"}} forces the model to call that
    ///     function.
    ///     none is the default when no functions are present. auto is the default if functions are present.
    /// </summary>
    [JsonIgnore]
    public ToolChoice? ToolChoice { get; set; }

    [JsonPropertyName("tool_choice")]
    public object? ToolChoiceCalculated
    {
        get
        {
            if (ToolChoice != null && ToolChoice.Type != StaticValues.CompletionStatics.ToolChoiceType.Function &&
                ToolChoice.Function != null)
            {
                throw new ValidationException(
                    "You cannot choose another type besides \"function\" while ToolChoice.Function is not null.");
            }

            if (ToolChoice?.Type == StaticValues.CompletionStatics.ToolChoiceType.Function)
            {
                return ToolChoice;
            }

            return ToolChoice?.Type;
        }
        set
        {
            if (value is JsonElement jsonElement)
            {
                if (jsonElement.ValueKind == JsonValueKind.String)
                {
                    ToolChoice = new ToolChoice
                    {
                        Type = jsonElement.GetString()
                    };
                }
            }
            else
            {
                ToolChoice = (ToolChoice)value;
            }
        }
    }

    /// <summary>
    ///     The format that the model must output. Used to enable JSON mode.
    ///     Must be one of "text" or "json_object".<br />
    ///     <see cref="StaticValues.CompletionStatics.ResponseFormat" /><br />
    ///     <example>
    ///         Sample Usage:<br />
    ///         new ResponseFormat { Type = StaticValues.CompletionStatics.ResponseFormat.Json }
    ///     </example>
    /// </summary>
    [JsonPropertyName("response_format")]
    public ResponseFormat? ResponseFormat { get; set; }

    /// <summary>
    ///     This feature is in Beta. If specified, our system will make a best effort to sample deterministically, such that
    ///     repeated requests with the same seed and parameters should return the same result. Determinism is not guaranteed,
    ///     and you should refer to the system_fingerprint response parameter to monitor changes in the backend.
    /// </summary>
    [JsonPropertyName("seed")]
    public int? Seed { get; set; }

    public IEnumerable<ValidationResult> Validate()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse. Learn more.
    /// </summary>
    [JsonPropertyName("user")]
    public string User { get; set; }
}