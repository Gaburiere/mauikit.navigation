namespace mauikit.navigation.sample.Features.Main;

public partial class SamplePopupPage
{
    public SamplePopupPage(SamplePopupViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}