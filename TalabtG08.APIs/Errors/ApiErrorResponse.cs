namespace TalabtG08.APIs.Errors
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }  
        public string? ErrorMassege { get; set; }

        public ApiErrorResponse(int StatusCode, string? ErrorMassege = null)
        {
            this.StatusCode = StatusCode;
            this.ErrorMassege = ErrorMassege ?? GetDefaultMessageForStatusCode(StatusCode);
        }

        private string? GetDefaultMessageForStatusCode(int StatusCode)
        {
            switch (StatusCode)
            {
                case 400:
                      return "bad request you have made";

                case 401:
                    return "Authorized, you are not";

                case 404:
                    return "Resources, not Found";

                case 500:
                    return "Server Errror";
                default:
                    return null;
            }
          
        }

    }
}
