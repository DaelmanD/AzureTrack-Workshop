using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace RMotownFestival.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var context = builder.GetContext();

            builder.Services.AddSingleton(p => new ComputerVisionClient(
                new ApiKeyServiceClientCredentials(context.Configuration["ComputerVisionApiKey"]))
            {
                Endpoint = context.Configuration["ComputerVisionEndpoint"]
            });
        }
    }
}
