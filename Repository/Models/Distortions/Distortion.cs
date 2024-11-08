namespace webapitest.Repository.Models.Distortions;

public class Distortion
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public List<Thought> Thoughts { get; set; } = new();

    public List<Event> Events { get; set; } = new();
}