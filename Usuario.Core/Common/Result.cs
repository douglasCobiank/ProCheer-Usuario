namespace Usuario.Core.Common
{
    public class Result
    {
        public bool Success { get; }
        public string? Error { get; }

        protected Result(bool success, string? error)
        {
            Success = success;
            Error = error;
        }

        public static Result Ok() => new(true, null);
        public static Result Fail(string error) => new(false, error);
    }

    public class Result<T> : Result
    {
        public T? Data { get; }

        private Result(bool success, T? data, string? error)
            : base(success, error)
        {
            Data = data;
        }

        public static Result<T> Ok(T data) =>
            new(true, data, null);

        public static new Result<T> Fail(string error) =>
            new(false, default, error);
    }
}
