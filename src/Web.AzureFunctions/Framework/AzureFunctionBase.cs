﻿using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Web.AzureFunctions.Framework
{
    public class AzureFunctionBase
    {
        protected async Task<TResult> ExecuteAsync<TResult>(Func<CancellationToken, Task<TResult>> valueFactory, ILogger logger, CancellationToken cancellationToken)
        {
            try
            {
                return await valueFactory.Invoke(cancellationToken);
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

        protected async Task ExecuteAsync(Func<CancellationToken, Task> valueFactory, ILogger logger, CancellationToken cancellationToken)
        {
            try
            {
                await valueFactory.Invoke(cancellationToken);
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

        protected void Execute(Action valueFactory, ILogger logger)
        {
            try
            {
                valueFactory.Invoke();
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
}
