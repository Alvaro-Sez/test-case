namespace Shared;

public static class Errors
{
    public static readonly Error IqDontExist = new Error("100", "the requested Iq doesnt exist");
    public static readonly Error IqAlreadyExist = new Error("101", "this Iq already exists");
    public static readonly Error RequestAlreadyPlaced = new Error("103", "you already placed a request");
    public static readonly Error IqNotAssigned = new Error("104", "no IQ assigned for this account");
    public static readonly Error UserDontExists = new Error("105", "this user doesn't exist");
    public static readonly Error NoLevelAccess = new Error("106", "you don't have high level access, only high access level users can upgrade the security level of the locks");
    public static readonly Error LockNotExist = new Error("107", "this lock doesn't exist");
    
}