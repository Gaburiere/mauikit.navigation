namespace mauikit.navigation.sample.Features.Main;

public partial class NewPage
{
    public NewPage(NewViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}