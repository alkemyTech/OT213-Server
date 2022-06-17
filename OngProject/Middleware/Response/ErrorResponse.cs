using OngProject.Enum;

namespace OngProject.Middleware.Response
{
    public class ErrorResponse
    {
        public ResponseCode ResponseCode {set;get;}
        public string Message {set;get;}

        public ErrorResponse(ResponseCode responseCode, string message)
        {
            this.ResponseCode = responseCode;
            this.Message = message;
        }
    }

}

