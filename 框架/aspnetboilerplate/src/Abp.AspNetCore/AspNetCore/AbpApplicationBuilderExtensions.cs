﻿using System.Globalization;
using System.Linq;
using Abp.AspNetCore.Localization;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Localization;
using Castle.LoggingFacility.MsLogging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Abp.AspNetCore
{
    public static class AbpApplicationBuilderExtensions
    {
        public static void UseAbp(this IApplicationBuilder app)
        {
            AddCastleLoggerFactory(app);

            InitializeAbp(app);

            ConfigureRequestLocalization(app);
        }

        private static void InitializeAbp(IApplicationBuilder app)
        {
            var abpBootstrapper = app.ApplicationServices.GetRequiredService<AbpBootstrapper>();
            abpBootstrapper.Initialize();
        }

        private static void AddCastleLoggerFactory(IApplicationBuilder app)
        {
            var castleLoggerFactory = app.ApplicationServices.GetService<Castle.Core.Logging.ILoggerFactory>();
            if (castleLoggerFactory == null)
            {
                return;
            }

            app.ApplicationServices
                .GetRequiredService<ILoggerFactory>()
                .AddCastleLogger(castleLoggerFactory);
        }

        private static void ConfigureRequestLocalization(IApplicationBuilder app)
        {
            var iocResolver = app.ApplicationServices.GetRequiredService<IIocResolver>();
            using (var languageManager = iocResolver.ResolveAsDisposable<ILanguageManager>())
            {
                var defaultLanguage = languageManager.Object
                    .GetLanguages()
                    .FirstOrDefault(l => l.IsDefault);

                if (defaultLanguage == null)
                {
                    return;
                }

                var supportedCultures = languageManager.Object
                    .GetLanguages()
                    .Select(l => new CultureInfo(l.Name))
                    .ToArray();

                var defaultCulture = new RequestCulture(defaultLanguage.Name);

                var options = new RequestLocalizationOptions
                {
                    DefaultRequestCulture = defaultCulture,
                    SupportedCultures = supportedCultures,
                    SupportedUICultures = supportedCultures
                };

                var settingManager = iocResolver.Resolve<ISettingManager>();
                var settingRequestCultureProvider = new SettingRequestCultureProvider(settingManager);
                AddSettingRequestCultureProvider(options, settingRequestCultureProvider);

                app.UseRequestLocalization(options);
            }
        }

        private static void AddSettingRequestCultureProvider(RequestLocalizationOptions options, SettingRequestCultureProvider settingRequestCultureProvider)
        {
            var acceptLanguageHeaderRequestCultureProvider = options.RequestCultureProviders.FirstOrDefault(rcp => rcp.GetType() == typeof(AcceptLanguageHeaderRequestCultureProvider));

            if (acceptLanguageHeaderRequestCultureProvider != null)
            {
                var acceptLanguageHeaderRequestCultureProviderIndex = options.RequestCultureProviders.IndexOf(acceptLanguageHeaderRequestCultureProvider);
                options.RequestCultureProviders.Insert(acceptLanguageHeaderRequestCultureProviderIndex, settingRequestCultureProvider);
            }
            else
            {
                options.RequestCultureProviders.Add(settingRequestCultureProvider);
            }
        }
    }
}
