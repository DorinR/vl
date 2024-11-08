using Microsoft.AspNetCore.Mvc;
using webapitest.Business.Interfaces;
using webapitest.Controllers.Models;
using webapitest.Repository;

namespace webapitest.Controllers;

[ApiController]
[Route("[controller]")]
public class ThoughtController : ControllerBase
{
    private readonly LLMChatRepository _llmChatRepository;
    private readonly IThoughtBusiness _thoughtBusiness;

    public ThoughtController(IThoughtBusiness thoughtBusiness, LLMChatRepository llmChatRepository)
    {
        _thoughtBusiness = thoughtBusiness;
        _llmChatRepository = llmChatRepository;
    }

    [HttpPost]
    [Route("Analyze")]
    public async Task<ActionResult<GetThoughtDistortionsResponseModel>> GetThoughtDistortions(
        [FromBody] GetThoughtDistortionsRequestModel model)
    {
        var result = await _thoughtBusiness.GetThoughtDistortions(model.thought);

        return Ok(new GetThoughtDistortionsResponseModel { Distortions = result.Distortions });
    }

    [HttpPost]
    [Route("Add")]
    public async Task<ActionResult<AddThoughtResponseModel>> AddThought([FromBody] AddThoughtRequestModel model)
    {
        var thoughtId = await _thoughtBusiness.AddThought(model.content);

        if (thoughtId == null) return BadRequest();

        return Ok(new AddThoughtResponseModel { Id = thoughtId });
    }

    [HttpGet]
    [Route("Thoughts")]
    public async Task<ActionResult<RetrieveThoughtsResponseModel>> RetrieveThoughts()
    {
        var result = await _thoughtBusiness.RetrieveThoughts();

        if (result == null) return BadRequest();

        return Ok(new RetrieveThoughtsResponseModel { Thoughts = result });
    }

    [HttpPost]
    [Route("AnalyzeThought")]
    public async Task<ActionResult<AnalyzeThoughtResponseModel>> AnalyzeThought(
        [FromBody] AnalyzeThoughtRequestModel model)
    {
        var result = await _llmChatRepository.GetChatResponse(model.Content);

        if (result == null) return BadRequest();

        return Ok(new AnalyzeThoughtResponseModel { DistortionsDetected = new List<string>(), Reformulation = result });
    }

    [HttpDelete("DeleteThought/{thoughtId}")]
    public async Task<ActionResult<DeleteThoughtResponseModel>> DeleteThought(
        string thoughtId)
    {
        var result = await _thoughtBusiness.DeleteThought(thoughtId);

        if (result == null) return BadRequest();

        return Ok(new DeleteThoughtResponseModel { thoughtId = result });
    }
}