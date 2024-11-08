using webapitest.Repository.Models.Distortions;

namespace webapitest.Repository.Models;

public class ThoughtDto
{
    public Guid Id { get; set; }

    public string Content { get; set; }

    public string DateCreated { get; set; }

    public Guid UserId { get; set; }

    public List<DistortionDto> Distortions { get; set; } = new();
}