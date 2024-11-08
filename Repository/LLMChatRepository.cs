using FuzzySharp;
using OpenAI.Chat;
using webapitest.Repository.Models.Distortions;

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

    /**
     * Returns a list of the IDs of the distortions detected
     */
    public async Task<List<DistortionDto>> DetectDistortionsInThought(List<DistortionDto> possibleDistortions,
        string thought)
    {
        ChatClient client = new("gpt-4o-mini", _configuration["openaiApiKey"]);

        try
        {
            SystemChatMessage systemMessage = new(
                "I am practicing Cognitive behavioural therapy " +
                "and you are my aid.  I will give you a list of possible " +
                "thought distortions followed by a thought and your task" +
                " is to tell me which of those I've engages in. You must give" +
                "me the response in the form of a list of distortions taken from " +
                "the list that i provide you. Respond with a list of these separated" +
                "using a % character");

            var availableDistortionsString =
                string.Join(" ", possibleDistortions.Select(distortion => distortion.Name));

            UserChatMessage availableDistortions = new(availableDistortionsString);

            UserChatMessage userThought = new(thought);

            var completionPackage = new List<ChatMessage>
            {
                systemMessage,
                availableDistortions,
                userThought
            };

            ChatCompletion result = await client.CompleteChatAsync(completionPackage);

            var response = result.Content.FirstOrDefault()?.Text;

            var detectedDistortionsStrings = response.Split("%").ToList();

            var detectedDistortions = new List<DistortionDto>();

            foreach (var distortion in possibleDistortions)
            foreach (var distortionString in detectedDistortionsStrings)
            {
                var dissimilarityDistance = Levenshtein.EditDistance(distortion.Name, distortionString);
                if (dissimilarityDistance < 3) detectedDistortions.Add(distortion);
            }

            return detectedDistortions;
        }
        catch (Exception ex)
        {
            throw new Exception("there was a problem contact the OpenAI server");
        }
    }
}