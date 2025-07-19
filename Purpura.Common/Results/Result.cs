namespace Purpura.Common.Results
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }

        protected Result()
        {
            IsSuccess = true;
        }

        protected Result(string error)
        {
            IsSuccess = false;
            Error = error;
        }

        public static Result Success()
        {
            return new Result();
        }

        

        public static Result Failure(string error)
        {
            return new Result(error);
        }
    }

    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }
        public T? Data { get; set; }

        protected Result(T data)
        {
            IsSuccess = true;
            Data = data;
        }

        protected Result(string error)
        {
            IsSuccess = false;
            Data = default;
        }

        public static Result<T> Success(T data)
        {
            return new Result<T>(data);
        }

        public static Result<T> Failure(string error)
        {
            return new Result<T>(error);
        }
    }
}
