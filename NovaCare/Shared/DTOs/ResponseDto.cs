using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs
{
    public class ResponseDto<T>
    {
        public bool IsSuccess { get; set; } = false;
        public int Status { get; set; } = 0;
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
