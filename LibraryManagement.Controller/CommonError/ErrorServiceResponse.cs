using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.LibraryManagement.CommonError
{

    [JsonObject(MemberSerialization.OptIn)]
    public class ErrorServiceResponse
    {
        [JsonProperty(PropertyName = "errors")]
        public List<ErrorResponse> Errors { get; set; }
    }

   
}
