﻿using System;
using Web.Configuration;

namespace Web.Rest.Configuration
{
    public class RestConfigurationBuilder
    {
        private readonly RestConfiguration _restConfiguration;

        public RestConfigurationBuilder(WebConfigurationBuilder webConfigurationBuilder)
        {
            _restConfiguration = new RestConfiguration(webConfigurationBuilder.Build());
        }

        public RestConfigurationBuilder UseSwaggerConfiguration(SwaggerConfiguration swaggerConfiguration)
        {
            _restConfiguration.SwaggerConfiguration = swaggerConfiguration;

            return this;
        }

        public RestConfigurationBuilder DocumentUsernameHeaderToken(bool flag = true)
        {
            _restConfiguration.IsDocumentUsernameHeaderToken = flag;

            return this;
        }

        public RestConfiguration Build()
        {
            _restConfiguration.SwaggerConfiguration ??= DefaultSwaggerConfiguration;

            if (_restConfiguration.ApplicationConfiguration["IsDocumentUsernameHeaderToken"] != null && bool.TryParse(_restConfiguration.ApplicationConfiguration["IsDocumentUsernameHeader"], out bool isDocumentUsernameHeaderToken))
            {
                _restConfiguration.IsDocumentUsernameHeaderToken = isDocumentUsernameHeaderToken;
            }

            return _restConfiguration;
        }

        private SwaggerConfiguration DefaultSwaggerConfiguration =>
            new SwaggerConfiguration
            {
                Title = _restConfiguration.ApplicationConfiguration["SwaggerConfiguration:Title"] ?? _restConfiguration.ApplicationConfiguration["ServiceName"],
                MajorVersion = Convert.ToInt32(_restConfiguration.ApplicationConfiguration["SwaggerConfiguration:MajorVersion"]),
                MinorVersion = Convert.ToInt32(_restConfiguration.ApplicationConfiguration["SwaggerConfiguration:MinorVersion"]),
                Description = _restConfiguration.ApplicationConfiguration["SwaggerConfiguration:Description"]
            };
    }
}
