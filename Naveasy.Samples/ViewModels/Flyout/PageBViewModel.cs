﻿using Microsoft.Extensions.Logging;
using Naveasy.Samples.Views;
using System.Windows.Input;

namespace Naveasy.Samples.ViewModels.Flyout;

public class PageBViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

    public PageBViewModel(ILogger<PageBViewModel> logger, INavigationService navigationService) : base(logger)
    {
        _navigationService = navigationService;
        Title = "Page B";
        NavigateCommand = new Command(Navigate);
        BackCommand = new Command(Back);
    }

    public ICommand NavigateCommand { get; }
    public ICommand BackCommand { get; }

    private void Navigate()
    {
        _navigationService.NavigateAsync<PageCViewModel>();
    }

    private void Back()
    {
        _navigationService.GoBackAsync();
    }
}