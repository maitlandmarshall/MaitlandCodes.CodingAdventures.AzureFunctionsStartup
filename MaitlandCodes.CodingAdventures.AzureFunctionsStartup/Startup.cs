using MaitlandCodes.CodingAdventures.AzureFunctionsStartup.Services;
using MaitlandCodes.CodingAdventures.AzureFunctionsStartup;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(Startup))]
namespace MaitlandCodes.CodingAdventures.AzureFunctionsStartup
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<MyInjectedService>();
        }
    }
}
