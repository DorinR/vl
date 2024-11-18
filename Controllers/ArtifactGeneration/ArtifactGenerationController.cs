using Microsoft.AspNetCore.Http.HttpResults;
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
    
    
    
}