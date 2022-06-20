using System.Text.Json;
using OngProject.Enum;

namespace OngProject.Middleware.Response
{
    public class ErrorResponse
    {
        public int StatusCode {set;get;}
        public string Message {set;get;}

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

}

