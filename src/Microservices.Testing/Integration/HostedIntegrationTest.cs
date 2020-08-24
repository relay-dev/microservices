﻿using Core.Plugins.NUnit.Integration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Microservices.Testing.Integration
{
    public abstract class HostedIntegrationTest<TToTest> : IntegrationTest<TToTest>
    {
        protected IHostBuilder CreateTestHostBuilder<TStartup>(string basePath = "") where TStartup : class =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(new string[0])
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<TStartup>();
                    webBuilder.ConfigureLogging((hostingContext, logging) =>
                    {
                        logging.AddConsole();
                        logging.AddDebug();
                    });
                })
                .ConfigureAppConfiguration((webBuilder, configBuilder) =>
                {
                    configBuilder.AddUserSecrets<TStartup>();

                    configBuilder
                        .SetBasePath(basePath)
                        .AddJsonFile("appsettings.json", false, true)
                        .AddJsonFile("appsettings.Development.json", false, true)
                        .AddEnvironmentVariables();
                });

        protected HttpRequest CreateHttpRequest()
        {
            return new DefaultHttpContext().Request;
        }

        protected HttpRequest CreateHttpRequest(string key, string val)
        {
            HttpRequest request = CreateHttpRequest();

            request.QueryString = QueryString.Create(
                new Dictionary<string, string>
                {
                    {key, val}
                });

            return request;
        }

        protected HttpRequest CreateHttpRequest(Dictionary<string, string> queryStringParameters)
        {
            HttpRequest request = CreateHttpRequest();

            request.QueryString = QueryString.Create(queryStringParameters);

            return request;
        }
    }
}