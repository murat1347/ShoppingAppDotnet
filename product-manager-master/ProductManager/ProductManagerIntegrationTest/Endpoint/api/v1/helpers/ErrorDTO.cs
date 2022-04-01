using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProductManagerIntegrationTest.Endpoint.api.v1
{
    public class ErrorDTO
    {
        public String type { get; set; }

        public String title { get; set; }

        public int status { get; set; }

        public String traceId { get; set; }

        public Hashtable errors { get; set; }

        public String FirstNamedError(string key){
          
            if(errors[key] == null){
                return null;
            }

            JsonElement arr = (JsonElement)errors[key];
            return arr.EnumerateArray().First().ToString();
        }
    }
}
