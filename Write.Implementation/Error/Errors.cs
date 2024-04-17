using SharedError = Shared.Error;
namespace Write.Implementation.Error;

public static class Errors
{
    public static SharedError IqDontExist = new SharedError("100", "the requested Iq doesnt exist");
    public static SharedError IqAlreadyExist = new SharedError("101", "this Iq already exists");
}