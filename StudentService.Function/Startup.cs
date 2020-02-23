using System;
using Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentService.Core.Interfaces;
using StudentService.Infrastructure.Data;

[assembly: FunctionsStartup(typeof(StudentService.Function.Startup))]
namespace StudentService.Function
{
   public  class Startup  : FunctionsStartup
    {       
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IStudentRepository>(x => new StudentRepository(new CosmosClient(
            Environment.GetEnvironmentVariable("CosmosDBConnectionString"))));
            
        }
    }
}
