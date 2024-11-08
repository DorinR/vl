using Microsoft.AspNetCore.Mvc;
using webapitest.Controllers.Models.Distortion;
using webapitest.Repository;

namespace webapitest.Controllers.Models;

[ApiController]
[Route("[controller]")]
public class DistortionController : ControllerBase
{
    private readonly DistortionRepository _distortionRepository;

    public DistortionController(DistortionRepository distortionRepository)
    {
        _distortionRepository = distortionRepository;
    }

    [HttpPost]
    [Route("Add")]
    public async Task<ActionResult<AddDistortionResponseModel>> AddDistortion(
        [FromBody] AddDistortionRequestModel model)
    {
        var distortionId = await _distortionRepository.AddDistortion(model.Name);

        if (distortionId == null) return BadRequest();

        return Ok(new AddDistortionResponseModel { DistortionId = distortionId });
    }
}