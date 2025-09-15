namespace SprintBusiness.Shared.Dtos
{
    // public class ResultDto<T>(bool succeeded, T? entity = null, string? message = null) where T : class
    // {
    //     public bool Succeeded { get; set; } = succeeded;
    //     public string? Message { get; set; } = message;
    //     public T? Entity { get; set; } = entity;
    // }

    // public class ResultDto(bool succeeded, string? message = null)
    // {
    //     public bool Succeeded { get; set; } = succeeded;
    //     public string? Message { get; set; } = message;
    // }

    public class ResultDto<T> where T : class
    {
        public bool Succeeded { get; }
        public string? Message { get; }
        public T? Entity { get; }

        protected ResultDto(bool succeeded, T? entity = null, string? message = null)
        {
            Succeeded = succeeded;
            Entity = entity;
            Message = message;
        }

        // طرق Success
        public static ResultDto<T> Success(T entity, string? message = null)
            => new(true, entity, message);

        public static ResultDto<T> Success(string? message = null)
            => new(true, null, message);

        // طرق Failure
        public static ResultDto<T> Failure(string? message = null)
            => new(false, null, message);

        public static ResultDto<T> Failure(T? entity, string? message = null)
            => new(false, entity, message);

        // عملية تحويل ضمنية
        public static implicit operator bool(ResultDto<T> result)
            => result.Succeeded;
    }

    public class ResultDto
    {
        public bool Succeeded { get; }
        public string? Message { get; }

        protected ResultDto(bool succeeded, string? message = null)
        {
            Succeeded = succeeded;
            Message = message;
        }

        // طرق Success
        public static ResultDto Success(string? message = null)
            => new(true, message);

        // طرق Failure
        public static ResultDto Failure(string? message = null)
            => new(false, message);

        // عملية تحويل ضمنية
        public static implicit operator bool(ResultDto result)
            => result.Succeeded;
    }
}
