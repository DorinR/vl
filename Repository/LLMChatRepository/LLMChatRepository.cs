using FuzzySharp;
using OpenAI.Chat;

namespace webapitest.Repository;

public class LLMChatRepository
{
    private readonly IConfiguration _configuration;

    public LLMChatRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> GetChatResponse(string message)
    {
        ChatClient client = new("gpt-4o-mini", _configuration["openaiApiKey"]);

        try
        {
            SystemChatMessage systemMessage = new(
                "I am practicing Cognitive behavioural therapy " +
                "and you are my aid. I will transcribe my thoughts and " +
                "give them to you and your job is to tell me which thought " +
                "distortions I am engaging in and giving me a reformulation of my thought.");

            UserChatMessage userMessage = new(message);

            var completionPackage = new List<ChatMessage>
            {
                systemMessage,
                userMessage
            };

            ChatCompletion result = await client.CompleteChatAsync(completionPackage);

            var response = result.Content.FirstOrDefault()?.Text;

            return response;
        }
        catch (Exception ex)
        {
            throw new Exception("there was a problem contact the OpenAI server");
        }
    }
    
    public async Task<string> GetChatResponseWithSystemMessage(string usrMessage, string sysMessage)
    {
        ChatClient client = new("gpt-4o-mini", _configuration["openaiApiKey"]);

        try
        {
            SystemChatMessage systemMessage = new(sysMessage);

            UserChatMessage userMessage = new(usrMessage);

            var completionPackage = new List<ChatMessage>
            {
                systemMessage,
                userMessage
            };

            ChatCompletion result = await client.CompleteChatAsync(completionPackage);

            var response = result.Content.FirstOrDefault()?.Text;

            return response;
        }
        catch (Exception ex)
        {
            throw new Exception("there was a problem contact the OpenAI server");
        }
    }
}