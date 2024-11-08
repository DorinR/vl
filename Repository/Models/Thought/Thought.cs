using webapitest.Repository.Models.Distortions;

namespace webapitest.Repository.Models;

public class Thought
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime DateCreated { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }

    public List<Distortion> Distortions { get; set; } = new();
}