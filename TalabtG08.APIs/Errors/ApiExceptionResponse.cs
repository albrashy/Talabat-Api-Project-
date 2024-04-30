namespace TalabtG08.APIs.Errors
{
    public class ApiExceptionResponse :ApiErrorResponse
    {
        public string? Detailes { get; set; }

        public ApiExceptionResponse(int StatusCode ,string? Messsage=null ,string? Details=null): base(StatusCode)
        {
            this.Detailes = Details;
        }
    }
}
