namespace Shared;

public static class Errors
{
    public static readonly Error IqDontExist = new Error("100", "the requested Iq doesnt exist");
    public static readonly Error IqAlreadyExist = new Error("101", "this Iq already exists");
    public static readonly Error InvalidIdFormat = new Error("102", "invalid Id Format");
}