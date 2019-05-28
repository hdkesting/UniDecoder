// <copyright file="Startup.cs" company="Hans Kesting">
// Copyright (c) Hans Kesting. All rights reserved.
// </copyright>

namespace UnidecoderWeb
{
    using System;
    using System.IO;
    using System.Net;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.StaticFiles;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Net.Http.Headers;
    using UnidecoderWeb.Services;

    /// <summary>
    /// The startup class for the web app.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is in development mode.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is development; otherwise, <c>false</c>.
        /// </value>
        public bool IsDevelopment { get; private set; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <remarks>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </remarks>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddTransient<UnicodeService, UnicodeService>();
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <remarks>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </remarks>
        /// <param name="app">The application.</param>
        /// <param name="env">The environment.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                this.IsDevelopment = true;
                app.UseDeveloperExceptionPage();
            }

            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files?tabs=aspnetcore2x#serving-a-default-document
            app.UseDefaultFiles();

            app.UseStaticFiles(new StaticFileOptions { OnPrepareResponse = this.PrepareCompressedResponse });

            app.UseMvc();
        }

        private void PrepareCompressedResponse(StaticFileResponseContext context)
        {
            //// https://github.com/aspnet/StaticFiles/issues/7

            var file = context.File;
            var request = context.Context.Request;
            var response = context.Context.Response;

            if (file.Name.EndsWith(".gz"))
            {
                // possibly just redirected to the .gz version
                response.Headers[HeaderNames.ContentEncoding] = "gzip";

                // reset to *original* content type (not application/x-gzip)
                new FileExtensionContentTypeProvider().TryGetContentType(file.Name.Replace(".gz", string.Empty), out string contentType);
                response.Headers[HeaderNames.ContentType] = contentType ?? "application/octet-stream";
                return;
            }

            var requestPath = request.Path.Value;
            var filePath = file.PhysicalPath;

            if (this.IsDevelopment && file.Name.IndexOf(".min.", StringComparison.OrdinalIgnoreCase) != -1)
            {
                // in development, replace minified files with originals
                if (File.Exists(filePath.Replace(".min.", ".")))
                {
                    response.StatusCode = (int)HttpStatusCode.TemporaryRedirect;
                    response.Headers[HeaderNames.Location] = requestPath.Replace(".min.", ".");
                }

                return;
            }

            // if a .gz version exists, use that! (which enters this method again)
            var acceptEncoding = (string)request.Headers[HeaderNames.AcceptEncoding];
            if (acceptEncoding.IndexOf("gzip", StringComparison.OrdinalIgnoreCase) != -1 && File.Exists(filePath + ".gz"))
            {
                response.StatusCode = (int)HttpStatusCode.MovedPermanently;
                response.Headers[HeaderNames.Location] = requestPath + ".gz";
            }
        }
    }
}
