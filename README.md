# Navigation.MauiKit

A powerful MVVM navigation library for .NET MAUI that simplifies page navigation, popup management, and provides a clean separation of concerns for your mobile applications.

[![MAUI Multi-Platform Build](https://github.com/Gaburiere/mauikit.navigation/actions/workflows/publish.yml/badge.svg)](https://github.com/Gaburiere/mauikit.navigation/actions/workflows/publish.yml)
![NuGet Version](https://img.shields.io/nuget/vpre/Navigation.MauiKit)
![NuGet Downloads](https://img.shields.io/nuget/dt/Navigation.MauiKit)


## Features

- 🎯 **Parameter Passing** - Pass data between pages seamlessly with navigation lifecycle hooks
- 📱 **Popup Management** - Display and manage popups with return values using CommunityToolkit.Maui
- 🔄 **Lifecycle Hooks** - `OnNavigatedTo` and `OnNavigatedBack` callbacks for handling navigation events
- 🌍 **Localization Support** - Built-in localization system with runtime language switching
- 🧪 **Testable** - Built with testing in mind using service abstractions
- 🎨 **Base View Models** - Ready-to-use base classes with navigation capabilities built-in
- ⚡ **Loading States** - Built-in loading pages and popups with `IsBusy` support

## Installation

```bash
dotnet add package Navigation.MauiKit
```

Or via NuGet Package Manager:
```
Install-Package Navigation.MauiKit
```

## Quick Start

### 1. Setup in MauiProgram.cs

```csharp
using mauikit.navigation.core.Extensions;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiCommunityToolkit()
            .UseMauiApp<App>()
            .UseMauiKitNavigation()  // Add this line
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Optional: Add localization support
        builder.UseLocalizationProvider(AppResources.ResourceManager);

        // Register your pages and view models
        builder.Services.AddTransientWithShellRoute<MainPage, MainViewModel>(nameof(MainPage));
        builder.Services.AddTransientWithShellRoute<DetailPage, DetailViewModel>(nameof(DetailPage));

        // Register popups
        builder.Services.AddTransientPopup<MyPopup, MyPopupViewModel>();

        return builder.Build();
    }
}
```

### 2. Create Your ViewModel

```csharp
using mauikit.navigation.core.Classes;
using mauikit.navigation.core.Interfaces;

public partial class MainViewModel : MauiViewModel
{
    private readonly IAlertService _alertService;

    public MainViewModel(INavigationService navigationService, IAlertService alertService)
        : base(navigationService)
    {
        _alertService = alertService;
    }

    [RelayCommand]
    private async Task NavigateToDetail()
    {
        var parameter = "Hello from MainPage";
        await Push<DetailPage>(parameter);
    }

    public override async Task OnNavigatedBack(object? parameters)
    {
        if (parameters is string message)
        {
            await _alertService.DisplayAlert("Received", message, "OK");
        }
    }
}
```

### 3. Create Your Page

```xml
<?xml version="1.0" encoding="utf-8" ?>
<views:MauiPage
    x:Class="MyApp.MainPage"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:views="clr-namespace:mauikit.navigation.core.Views;assembly=mauikit.navigation.core"
    xmlns:local="clr-namespace:MyApp"
    Title="Main Page"
    x:DataType="local:MainViewModel">

    <VerticalStackLayout Padding="20" Spacing="10">
        <Button Text="Navigate to Detail"
                Command="{Binding NavigateToDetailCommand}" />
    </VerticalStackLayout>

</views:MauiPage>
```

```csharp
public partial class MainPage : MauiPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
```

## Navigation Patterns

### Forward Navigation with Parameters

```csharp
public partial class MainViewModel : MauiViewModel
{
    [RelayCommand]
    private async Task OpenDetail()
    {
        var data = new MyDataModel { Id = 1, Name = "Test" };
        await Push<DetailPage>(data);
    }
}
```

### Receiving Parameters on Navigation

```csharp
public partial class DetailViewModel : MauiViewModel
{
    [ObservableProperty]
    private MyDataModel? _receivedData;

    public override Task OnNavigatedTo(object? parameters)
    {
        if (parameters is MyDataModel data)
        {
            ReceivedData = data;
        }
        return base.OnNavigatedTo(parameters);
    }
}
```

### Back Navigation with Return Values

```csharp
public partial class DetailViewModel : MauiViewModel
{
    [RelayCommand]
    private async Task GoBack()
    {
        var returnValue = "Data from DetailPage";
        await Pop(returnValue);
    }
}
```

### Lifecycle Hooks

```csharp
public partial class MyViewModel : MauiViewModel
{
    // Called when page appears
    protected override async Task Appearing()
    {
        IsBusy = true;
        await LoadDataAsync();
        IsBusy = false;
    }

    // Called when page disappears
    protected override async Task Disappearing()
    {
        await SaveDataAsync();
    }

    // Called when navigating TO this page
    public override async Task OnNavigatedTo(object? parameters)
    {
        // Handle incoming parameters
    }

    // Called when navigating BACK to this page
    public override async Task OnNavigatedBack(object? parameters)
    {
        // Handle return values from child pages
    }
}
```

## Popup Management

### Display a Simple Popup

```csharp
public partial class MainViewModel : MauiViewModel
{
    [RelayCommand]
    private async Task ShowPopup()
    {
        await ShowPopupAsync<MyPopupViewModel>(options: new PopupOptions
        {
            CanBeDismissedByTappingOutsideOfPopup = true
        });
    }
}
```

### Display Popup with Parameters

```csharp
[RelayCommand]
private async Task ShowPopupWithData()
{
    var parameters = new Dictionary<string, object>
    {
        { "UserId", 123 },
        { "UserName", "John Doe" }
    };

    await ShowPopupAsync<MyPopupViewModel>(parameters);
}
```

### Popup with Return Value

```csharp
[RelayCommand]
private async Task ShowPopupWithReturn()
{
    var parameters = new Dictionary<string, object> { { "Mode", "Edit" } };

    var result = await ShowPopupAsync<string, MyPopupViewModel>(parameters, new PopupOptions
    {
        CanBeDismissedByTappingOutsideOfPopup = false
    });

    if (result is not null)
    {
        await _alertService.DisplayAlert("Result", result, "OK");
    }
}
```

### Creating a Popup ViewModel

```csharp
public partial class MyPopupViewModel : MauiPopupViewModel
{
    [ObservableProperty]
    private string? _userName;

    public MyPopupViewModel(INavigationService navigationService)
        : base(navigationService)
    {
    }

    // Receive parameters passed to popup
    public override void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("UserName", out var value))
        {
            UserName = value?.ToString();
        }
    }

    // Called when popup opens
    protected override async Task Opened()
    {
        IsBusy = true;
        await LoadDataAsync();
        IsBusy = false;
    }

    [RelayCommand]
    private async Task Close()
    {
        await ClosePopupAsync();
    }

    [RelayCommand]
    private async Task SaveAndClose()
    {
        var result = "Saved data";
        await ClosePopupAsync<string>(result);
    }
}
```

### Creating a Popup View

```xml
<?xml version="1.0" encoding="utf-8"?>
<views:MauiPopup
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:views="clr-namespace:mauikit.navigation.core.Views;assembly=mauikit.navigation.core"
    xmlns:local="clr-namespace:MyApp"
    x:Class="MyApp.MyPopup"
    x:DataType="local:MyPopupViewModel"
    BackgroundColor="White"
    HeightRequest="400"
    WidthRequest="300">

    <VerticalStackLayout Padding="20" Spacing="15">
        <Label Text="{Binding UserName}"
               FontSize="18"
               HorizontalOptions="Center" />

        <Entry Placeholder="Enter value" />

        <Button Text="Save"
                Command="{Binding SaveAndCloseCommand}" />
        <Button Text="Cancel"
                Command="{Binding CloseCommand}" />
    </VerticalStackLayout>

</views:MauiPopup>
```

## Loading States

The library provides built-in loading pages and popups:

### MauiLoadingPage

```xml
<views:MauiLoadingPage
    x:Class="MyApp.MyPage"
    xmlns:views="clr-namespace:mauikit.navigation.core.Views;assembly=mauikit.navigation.core"
    LoaderColor="Blue">

    <!-- Your content here -->
    <!-- Loading indicator automatically shows when IsBusy = true -->

</views:MauiLoadingPage>
```

### MauiLoadingPopup

```xml
<views:MauiLoadingPopup
    x:Class="MyApp.MyPopup"
    xmlns:views="clr-namespace:mauikit.navigation.core.Views;assembly=mauikit.navigation.core"
    LoaderColor="Blue"
    BackgroundColor="White">

    <!-- Your popup content -->
    <!-- Loading indicator automatically shows when IsBusy = true -->

</views:MauiLoadingPopup>
```

## Localization

### Setup Localization

```csharp
// In MauiProgram.cs
builder.UseLocalizationProvider(AppResources.ResourceManager);
```

### Use in ViewModel

```csharp
public partial class MainViewModel : MauiViewModel
{
    private readonly Localizator _localizator;

    public MainViewModel(INavigationService navigationService, Localizator localizator)
        : base(navigationService)
    {
        _localizator = localizator;
    }

    [RelayCommand]
    private void ChangeLanguage()
    {
        _localizator.Culture = new CultureInfo("fr-FR");
        // All bindings using TranslateExtension will update automatically
    }
}
```

### Use in XAML

```xml
<ContentPage
    xmlns:localizations="clr-namespace:mauikit.navigation.core.Localizations;assembly=mauikit.navigation.core">

    <VerticalStackLayout>
        <!-- Simple translation -->
        <Label Text="{localizations:Translate WelcomeMessage}" />

        <!-- Translation with string formatting -->
        <Label Text="{localizations:Translate UserGreeting, StringFormat='Hello, {0}!'}" />

        <Button Text="{localizations:Translate ChangeLanguage}"
                Command="{Binding ChangeLanguageCommand}" />
    </VerticalStackLayout>

</ContentPage>
```

## Alert Service

The `IAlertService` provides convenient methods for displaying alerts:

```csharp
public partial class MainViewModel : MauiViewModel
{
    private readonly IAlertService _alertService;

    public MainViewModel(INavigationService navigationService, IAlertService alertService)
        : base(navigationService)
    {
        _alertService = alertService;
    }

    [RelayCommand]
    private async Task ShowAlert()
    {
        await _alertService.DisplayAlert("Title", "Message", "OK");
    }

    [RelayCommand]
    private async Task ShowConfirm()
    {
        bool result = await _alertService.DisplayAlert(
            "Confirm",
            "Are you sure?",
            "Yes",
            "No"
        );

        if (result)
        {
            // User clicked "Yes"
        }
    }

    [RelayCommand]
    private async Task ShowPrompt()
    {
        string result = await _alertService.DisplayPromptAsync(
            "Input",
            "Enter your name",
            "OK",
            "Cancel",
            placeholder: "Your name here"
        );
    }
}
```

## API Reference

### MauiViewModel

Base class for page view models.

**Properties:**
- `bool IsBusy` - Observable property for loading states

**Methods:**
- `Task Push<T>(object? data = null)` - Navigate to page of type T
- `Task Pop(object? data = null)` - Navigate back
- `Task ShowPopupAsync<TPopupViewModel>(...)` - Show popup
- `Task<T?> ShowPopupAsync<T, TPopupViewModel>(...)` - Show popup with return value

**Lifecycle Methods:**
- `Task OnNavigatedTo(object? parameters)` - Called when navigating to this page
- `Task OnNavigatedBack(object? parameters)` - Called when returning to this page
- `Task Appearing()` - Called when page appears
- `Task Disappearing()` - Called when page disappears

### MauiPopupViewModel

Base class for popup view models.

**Properties:**
- `bool IsBusy` - Observable property for loading states

**Methods:**
- `Task ClosePopupAsync()` - Close popup without return value
- `Task ClosePopupAsync<TResult>(TResult result)` - Close popup with return value
- `void ApplyQueryAttributes(IDictionary<string, object> query)` - Receive parameters

**Lifecycle Methods:**
- `Task Opened()` - Called when popup opens
- `Task Closed()` - Called when popup closes

### INavigationService

Main navigation service (injected automatically).

**Methods:**
- `Task GoToAsync<T>(object? parameters = null)`
- `Task GoBackAsync(object? parameters = null)`
- `Task ShowPopupAsync<TPopupViewModel>(...)`
- `Task<TReturn?> ShowPopupAsync<TReturn, TPopupViewModel>(...)`
- `Task ClosePopupAsync()`
- `Task ClosePopupAsync<TResult>(TResult result)`

### IAlertService

Alert and dialog service (injected automatically).

**Methods:**
- `Task DisplayAlert(string title, string text, string cancelButton)`
- `Task<bool> DisplayAlert(string title, string text, string acceptButton, string cancelButton)`
- `Task<string> DisplayPromptAsync(string title, string message, ...)`

## Requirements

- .NET 10.0 or later
- .NET MAUI
- CommunityToolkit.Maui
- CommunityToolkit.Mvvm

## Sample Project

Check out the `mauikit.navigation.sample` project in this repository for a complete working example demonstrating all features.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.
