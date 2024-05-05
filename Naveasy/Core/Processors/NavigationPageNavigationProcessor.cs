﻿using Naveasy.Common;
using Naveasy.Extensions;

namespace Naveasy.Core.Processors;

public class NavigationPageNavigationProcessor(IApplicationProvider applicationProvider, IPageFactory pageFactory) : IPageNavigationProcessor
{
    public bool CanHandle<TViewModel>()
    {
        if (applicationProvider.MainPage is FlyoutPage)
        {
            return false;
        }

        var viewType = pageFactory.ResolveViewType(typeof(TViewModel));
        var result = viewType.IsSubclassOf(typeof(ContentPage)) || viewType == typeof(ContentPage);
        return result;
    }

    public async Task<INavigationResult> NavigateAsync<T>(INavigationParameters parameters = null, bool? animated = null)
    {
        try
        {
            parameters ??= new NavigationParameters();

            var navigation = applicationProvider.Navigation;
            var leavingPage = navigation.NavigationStack.LastOrDefault();

            var pageToNavigate = pageFactory.ResolvePage(typeof(T));

            await MvvmHelpers.OnInitializedAsync(pageToNavigate, parameters);
            await navigation.PushAsync(pageToNavigate);

            MvvmHelpers.OnNavigatedFrom(leavingPage, parameters);

            parameters.GetNavigationParametersInternal().Add(KnownInternalParameters.NavigationMode, NavigationMode.New);
            MvvmHelpers.OnNavigatedTo(pageToNavigate, parameters);

            return new NavigationResult(true);
        }
        catch (Exception ex)
        {
            return new NavigationResult(ex);
        }
    }

    public async Task<INavigationResult> NavigateAbsoluteAsync<T>(INavigationParameters parameters = null, bool? animated = null)
    {
        try
        {
            parameters ??= new NavigationParameters();

            var navigation = applicationProvider.Navigation;
            var pagesToRemove = navigation.NavigationStack.ToList();

            var pageToNavigate = pageFactory.ResolvePage(typeof(T));

            await MvvmHelpers.OnInitializedAsync(pageToNavigate, parameters);
            await navigation.PushAsync(pageToNavigate);

            foreach (var destroyPage in pagesToRemove)
            {
                navigation.RemovePage(destroyPage);
                MvvmHelpers.OnNavigatedFrom(destroyPage, parameters);
                MvvmHelpers.DestroyPage(destroyPage);
            }

            parameters.GetNavigationParametersInternal().Add(KnownInternalParameters.NavigationMode, NavigationMode.New);
            MvvmHelpers.OnNavigatedTo(pageToNavigate, parameters);

            return new NavigationResult(true);
        }
        catch (Exception ex)
        {
            return new NavigationResult(ex);
        }
    }
}