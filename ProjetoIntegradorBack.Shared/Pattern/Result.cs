namespace ProjetoIntegradorBack.Shared.Pattern
{
    public sealed class Result<T>
    {
        public T? Success { get; private set; }
        public Failure? Failure { get; private set; }
        public bool IsSuccess { get; private set; }
        public bool IsFailure => !IsSuccess;

        private Result(T success)
        {
            Success = success;
            IsSuccess = true;
        }

        private Result(Failure failure)
        {
            Failure = failure;
            IsSuccess = false;
        }

        public static Result<T> AsSuccess(T success) => new(success);
        public static Result<T> AsFailure(Failure failure) => new(failure);
    }
}