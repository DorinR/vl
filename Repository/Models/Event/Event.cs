using webapitest.Repository.Models.Distortions;

namespace webapitest.Repository.Models;

public class Event
{
    public Guid Id { get; set; }

    public DateTime DateCreated { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public string Situation { get; set; }

    public string Thoughts { get; set; }

    public string Emotions { get; set; }

    public string ResultingBehaviour { get; set; }

    public List<Distortion> Distortions { get; set; }

    public string ChallengePrompt { get; set; }

    public string Challenge { get; set; }

    public string Outcome { get; set; }
}