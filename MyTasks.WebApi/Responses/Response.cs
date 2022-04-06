using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.WebApi.Responses
{
    public class Response
    {
        public Response()
        {
            Errors = new List<Error>();
        }
        public bool IsSuccess => Errors == null || !Errors.Any();
        public List<Error> Errors { get; set; }
    }
}
