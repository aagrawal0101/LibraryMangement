using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.LibraryManagement.CommonError
{
    [JsonObject(MemberSerialization.OptIn, Id = "errorResponse", Title = "errorResponse")]
    public class ErrorInfo
    {
        public ErrorInfo(string field, string type, string detail)
        {
            Field = field;
            Type = type;
            Detail = detail;
        }

        [JsonProperty(PropertyName = "field")]
        public string Field { get; set; }


        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "detail")]
        public string Detail { get; set; }
    }
}
