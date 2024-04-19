namespace Read.Contracts.Entities;

public class Lock
{
    public Guid Id { get; set; }
    public string Access { get; set; } = AccessLevel.Low;
}