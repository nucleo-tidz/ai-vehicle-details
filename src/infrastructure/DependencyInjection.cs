﻿using System.Diagnostics.CodeAnalysis;

using Azure.AI.Agents.Persistent;
using Azure.AI.Projects;
using Azure.Identity;

using infrastructure.Agents;
using infrastructure.Factory;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

namespace infrastructure
{
    [Experimental("SEMX")]
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddSemanticKernel(configuration).
                AddTransient<IVehicleAgent, VehicleAgent>().
                AddTransient<IModelContextPrtocolFactory, ModelContextPrtocolFactory>().AddAgent(configuration);
            return services;
        }
        public static IServiceCollection AddSemanticKernel(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddTransient<Kernel>(serviceProvider =>
            {
                IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
                kernelBuilder.Services.AddAzureOpenAIChatCompletion("gpt-4.1",
                  configuration["foundry-endpoint"],
                  configuration["apikey"],
                   "gpt-4.1",
                   "gpt-4.1");
                return kernelBuilder.Build();
            });
        }
        public static IServiceCollection AddAgent(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(_ =>
            {
                var credential = new DefaultAzureCredential(
                new DefaultAzureCredentialOptions
                {
                    ExcludeVisualStudioCredential = true,
                    ExcludeEnvironmentCredential = true,
                    ExcludeManagedIdentityCredential = true,
                    ExcludeInteractiveBrowserCredential = false,
                    ExcludeAzureCliCredential = false,
                    ExcludeAzureDeveloperCliCredential = true,
                    ExcludeAzurePowerShellCredential = true,
                    ExcludeSharedTokenCacheCredential = true,
                    ExcludeVisualStudioCodeCredential = true,
                    ExcludeWorkloadIdentityCredential = true,

                });
                var projectClient = new AIProjectClient(new Uri(configuration["AgentProjectEndpoint"]), credential);
                return projectClient.GetPersistentAgentsClient();
            });
            return services;
        }
    }
}
