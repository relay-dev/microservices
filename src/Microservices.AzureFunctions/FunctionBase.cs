﻿using Microsoft.Extensions.Logging;
using System;

namespace Microservices.AzureFunctions
{
    public class FunctionBase
    {
        protected TResult Execute<TResult>(Func<TResult> valueFactory, ILogger logger)
        {
            try
            {
                return valueFactory.Invoke();
            }
            catch (OperationCanceledException)
            {
                logger.LogInformation("Request was cancelled");

                throw;
            }
            catch (Exception e)
            {
                logger.LogError(e, "{0} failed with message: {1}", this.GetType(), e.Message);

                throw;
            }
        }
    }

//    public abstract class FunctionStartupBase : FunctionsStartup
//    {
//        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
//        {
//            FunctionsHostBuilderContext context = builder.GetContext();

//            string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.SubstringBefore("tests"), "src", typeof(TStartup).Namespace);

//            configBuilder
//                .SetBasePath(basePath)
//                .AddJsonFile("appsettings.json", true, true)
//                .AddJsonFile("appsettings.Development.json", true, true)
//                .AddJsonFile("local.settings.json", true, true)
//                .AddUserSecrets<TStartup>()
//                .AddEnvironmentVariables();

//            builder.ConfigurationBuilder
//                .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
//                .AddJsonFile(Path.Combine(context.ApplicationRootPath, $"appsettings.{context.EnvironmentName}.json"), optional: true, reloadOnChange: false)
//                .AddEnvironmentVariables();

//            base.ConfigureAppConfiguration(builder);
//        }
//    }
}
