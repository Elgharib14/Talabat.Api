using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Talabat.Api.Error
{
    public class ApiErrorRespones
    {
        public int StatusCode { get; set; }
        public string Massage { get; set; }

        public ApiErrorRespones(int Code , string message = null)
        {
            StatusCode = Code;
            Massage = message?? GetDefaultTMessageForStatusCode(Code);
        }

        private string GetDefaultTMessageForStatusCode(int code)
        {
            return StatusCode switch
            {
                400 => "Bad Request",
                401 => "UnAutherized",
                404 => "Response Was not Found",
                500 => "Errors are the path to the dark side. Errors lead to anger",
                _ => null!
            };
        }
    }
}
