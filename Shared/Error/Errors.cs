namespace Shared;

public static class Errors
{
    public static readonly Error IqDontExist = new Error("100", "the requested Iq doesnt exist");
    public static readonly Error IqAlreadyExist = new Error("101", "this Iq already exists");
    public static readonly Error RequestAlreadyPlaced = new Error("103", "you already placed a request");
    public static readonly Error IqNotAssigned = new Error("104", "there are no IQ assigned for this account");
    public static readonly Error UserDontExists = new Error("105", "this user doesn't exist");
    public static readonly Error NoLevelAccess = new Error("106", "High level access is required for this operation");
    public static readonly Error LockNotExist = new Error("107", "this lock doesn't exist");
    public static readonly Error NoAccessToThisLock = new Error("108", "you dont have the IQ assigned for this lock");
    public static readonly Error UserNotAssignedToThisIq = new Error("109", "you are not assigned to this IQ");
    public static readonly Error IsNotAGuid = new Error("110", "The Id is not a valid GUID");
    
}