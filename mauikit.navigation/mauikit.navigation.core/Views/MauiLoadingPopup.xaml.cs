namespace mauikit.navigation.core.Views;

public partial class MauiLoadingPopup
{
    /// <summary>
    /// Gets or sets the color of the loading indicator.
    /// Default value is White.
    /// </summary>
    public static readonly BindableProperty LoaderColorProperty = BindableProperty.Create(nameof(LoaderColor), typeof(Color), typeof(MauiLoadingPage), Colors.White);

    /// <summary>
    /// Gets or sets the color of the ActivityIndicator displayed during loading.
    /// </summary>
    public Color LoaderColor
    {
        get { return (Color)GetValue(LoaderColorProperty); }
        set { SetValue(LoaderColorProperty, value); }
    }

    public MauiLoadingPopup()
    {
        InitializeComponent();
    }
}