namespace webapitest.Repository.Models;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public ICollection<Thought> Thoughts { get; set; }

    public ICollection<Event> Events { get; set; }
}