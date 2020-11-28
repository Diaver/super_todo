namespace ApiService.Models.Api.Common
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public ErrorMessagesEnum ErrorMessagesEnum { get; set; }

        public static ApiResult Ok()
        {
            return new ApiResult
            {
                IsSuccess = true
            };
        }
        
        public static ApiResult Bad()
        {
            return new ApiResult
            {
                IsSuccess = false
            };
        }
        
        public static ApiResult Ok(string message)
        {
            return new ApiResult
            {
                IsSuccess = true,
                Message = message
            };
        }
        
        public static ApiResult Bad(ErrorMessagesEnum errorMessagesEnum, string message)
        {
            return new ApiResult
            {
                IsSuccess = false,
                ErrorMessagesEnum = errorMessagesEnum,
                Message = message
            };
        }

        public ApiResult<T> To<T>()
        {
            return new ApiResult<T>
            {
                IsSuccess = this.IsSuccess,
                ErrorMessagesEnum = this.ErrorMessagesEnum,
                Message = this.Message
            };
        }
        
        public ApiResult ToSimple()
        {
            return new ApiResult
            {
                IsSuccess = this.IsSuccess,
                ErrorMessagesEnum = this.ErrorMessagesEnum,
                Message = this.Message
            };
        }
    }

    public class ApiResult<T> : ApiResult
    {
        public T Data { get; set; }

        public static ApiResult<T> Ok(T data)
        {
            return new ApiResult<T> {IsSuccess = true, Data = data};
        }

        public new static ApiResult<T> Bad(ErrorMessagesEnum errorMessagesEnum, string message)
        {
            return new ApiResult<T>
            {
                IsSuccess = false,
                ErrorMessagesEnum = errorMessagesEnum,
                Message = message
            };
        }

        public new static ApiResult<T> Bad()
        {
            return new ApiResult<T>
            {
                IsSuccess = false,
            };
        }
        
       
    }
}