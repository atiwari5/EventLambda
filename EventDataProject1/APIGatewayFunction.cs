using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System.Net;
using EventDataProject1.Handler;

using Amazon.Runtime.CredentialManagement;
using Amazon.SQS;
using Amazon.SQS.Model;
using System.Runtime.CompilerServices;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace EventDataProject1
{
    public class APIGatewayFunction
    {

        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request,
                                                       ILambdaContext context)
        {
            var jsonEvent = JsonConvert.SerializeObject(request);
            var jsonContext = JsonConvert.SerializeObject(context);

            context.Logger.Log(jsonEvent);
            context.Logger.Log(jsonContext);
            context.Logger.LogLine("-----------------------------------------");

            return this.CreateResponse(request);
        }

        private APIGatewayProxyResponse CreateResponse(APIGatewayProxyRequest request)
        {
            IEventBus _eventBus = new EventBusAmazonSQS();

            int statusCode = (request != null) ? (int)HttpStatusCode.OK
                                               : (int)HttpStatusCode.InternalServerError;

            EventRequest payload = JsonConvert.DeserializeObject<EventRequest>(
                           request.Body ?? "{\"message\": \"ERROR: No Payload\"}");

            // SQS call
            _eventBus.Publish(payload);

            EventResponse eventResponse = new EventResponse();

            eventResponse.Response_Key = "Notification Recevied for " + payload.Event_Type + "Event for " + payload.Policy ;

            string body = JsonConvert.SerializeObject(eventResponse);

            var response = new APIGatewayProxyResponse
            {
                StatusCode = statusCode,
                Body = body,
                Headers = new Dictionary<string, string>
               {
                   { "Content-Type", "application/json" },
                   { "Access-Control-Allow-Origin", "*" },
                    {"Correlation-id" , $"{payload.Id}" }
               }
            };

            return response;
        }

    }
}
