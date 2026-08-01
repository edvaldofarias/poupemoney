namespace PoupeMoney.Core.Application.Commons;

public class Response
{
    private readonly List<Error> _errors = [];
    public IReadOnlyList<Error> Errors => _errors.ToList();
    public bool Success => Errors.Any() is false;

    internal void AddError(IEnumerable<ValidationFailure> failures)
    {
        AddError(failures.ToArray());
    }

    internal void AddError(params ValidationFailure[] failures)
    {
        var errors = failures.Select(x =>
            new Error(
                x.ErrorMessage,
                x.PropertyName,
                x.AttemptedValue?.ToString())).ToArray();
        _errors.AddRange(errors);
    }
}

public sealed class Response<T> : Response
{
    public T? Data { get; private set; }

    internal Response<T> AddData(T data)
    {
        Data = data;
        return this;
    }
}
