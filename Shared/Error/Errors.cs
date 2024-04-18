namespace Shared;

public static class Errors
{
    public static readonly Error IqDontExist = new Error("100", "the requested Iq doesnt exist");
    public static readonly Error IqAlreadyExist = new Error("101", "this Iq already exists");
    public static readonly Error RequestAlreadyPlaced = new Error("103", "you already placed a request");
    public static readonly Error IqNotAssigned = new Error("104", "you don't have any IQ assigned for this account");
}