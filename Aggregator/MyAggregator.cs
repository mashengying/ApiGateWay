using Ocelot.Middleware;
using Ocelot.Multiplexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http;

namespace ApiGateWay.Aggregator
{
    public class MyAggregator:IDefinedAggregator
    {
        public MyAggregator()
        { 
        
        }
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            List<string> results = new List<string>();
            foreach (var context in responses)
            {
                var result =await context.Items.DownstreamResponse().Content.ReadAsStringAsync();
                results.Add(result);
            }
            var merge=string.Join(";",results.ToArray());
            var stringContent = new StringContent(merge);
            var headers = new List<Header>();
            return new DownstreamResponse(stringContent, HttpStatusCode.OK, headers, "some reason");
        }
    }
}
