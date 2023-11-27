﻿using static Clicco.Domain.Core.Exceptions.Errors;

namespace Clicco.Domain.Core.ResponseModel
{
    public class ResponseDto
    {
        public object Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public Guid RequestId { get; } = Guid.NewGuid();
        public Error Error { get; set; }
        public string Message { get; set; } = "The Operation Is Successful!";

        public ResponseDto Fail(Error error)
        {
            IsSuccess = false;
            Error = error;
            Message = string.Empty;
            return this;
        }
    }

    public class ResponseDto<T> : ResponseDto
    {
        public new T Data
        {
            get => (T)base.Data;
            set => base.Data = value;
        }
    }
}