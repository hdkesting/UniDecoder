// <copyright file="Startup.cs" company="Hans Kesting">
// Copyright (c) Hans Kesting. All rights reserved.
// </copyright>

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Unidecoder.Functions.Services;

[assembly: FunctionsStartup(typeof(Unidecoder.Functions.Startup))]

namespace Unidecoder.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<UnicodeService>();
        }
    }
}
