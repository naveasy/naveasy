﻿using Microsoft.Extensions.Logging;
using Naveasy.Core;
using Naveasy.Extensions;
using Naveasy.Samples.ViewModels;
using Naveasy.Samples.ViewModels.Flyout;
using Naveasy.Samples.Views;
using Naveasy.Samples.Views.Flyout;

namespace Naveasy.Samples;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseNaveasy()
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services
            .AddTransientForNavigation<LoginPage, LoginPageViewModel>()
            .AddTransientForNavigation<HomeContentPage, HomeContentPageViewModel>()
            .AddTransientForNavigation<HomeFlyoutPage, HomeFlyoutPageViewModel>()
            .AddTransientForNavigation<ProductsPage, ProductsPageViewModel>()
            .AddTransientForNavigation<ProductDetailsPage, ProductDetailsPageViewModel>()
            .AddTransientForNavigation<HelpPage, HelpPageViewModel>()
            .AddTransientForNavigation<PageA, PageAViewModel>()
            .AddTransientForNavigation<PageB, PageBViewModel>()
            .AddTransientForNavigation<PageC, PageCViewModel>()
            .AddTransientForNavigation<PageD, PageDViewModel>();

#if DEBUG
        builder.Logging
            .SetMinimumLevel(LogLevel.Trace)
            .AddDebug();
#endif

        return builder.Build();
    }
}
