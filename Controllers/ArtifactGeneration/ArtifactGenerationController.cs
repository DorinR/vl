using Microsoft.AspNetCore.Mvc;
using webapitest.Business.ArtifactGeneration;
using webapitest.Controllers.Models;

namespace webapitest.Controllers;

[ApiController]
[Route("[controller]")]
public class ArtifactGenerationController : ControllerBase
{
    private readonly ArtifactGenerationBusiness _artifactGenerationBusiness;

    public ArtifactGenerationController(ArtifactGenerationBusiness artifactGenerationBusiness)
    {
        _artifactGenerationBusiness = artifactGenerationBusiness;
    }

    [HttpPost]
    [Route("initializeDemandLetter")]
    public async Task<ActionResult> InitializeDemandLetter([FromBody] InitializeDemandLetterModel model)
    {
        var result = await _artifactGenerationBusiness.InitializeDemandLetter(model.userMessage);

        return Ok(new
        {
            response = result
        });
    }

    [HttpPost]
    [Route("GetNextQuestion")]
    public async Task<ActionResult> GetNextQuestion([FromBody] GetNextQuestionModel model)
    {
        try
        {
            var nextQuestion = await _artifactGenerationBusiness.GetNextQuestion(model.ArtifactId);
            return Ok(new { nextQuestion });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
        }
    }
}

public record GetNextQuestionModel(int ArtifactId);