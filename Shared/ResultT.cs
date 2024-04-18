namespace Shared;

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error) =>
        _value = value;

    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");
    public static Result<TValue> From (TValue value)
    {
        return new Result<TValue>(value, true, Error.None);
    }
    
    public new static Result<TValue> Failure(Error error) => new(default(TValue) , false, error);
}