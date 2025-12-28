namespace Purpura.Common.Results
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string? Error { get; private set; }

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
        public bool IsSuccess { get; private set; }
        public string? Error { get; private set; }
        public T Data { get; private set; }
        public List<T> DataList { get; private set; }

        protected Result(T data)
        {
            IsSuccess = true;
            Data = data;
        }

        protected Result(List<T> dataList)
        {
            IsSuccess = true;
            DataList = dataList;
        }

        protected Result(string error)
        {
            IsSuccess = false;
            Error = error;
        }

        public static Result<T> Success(T data)
        {
            return new Result<T>(data);
        }

        public static Result<T> Success(List<T> dataList)
        {
            return new Result<T>(dataList);
        }

        public static Result<T> Failure(string error)
        {
            return new Result<T>(error);
        }
    }
}
