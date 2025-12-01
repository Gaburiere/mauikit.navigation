namespace mauikit.navigation.sample.Features.Main;

public partial class NewModalPage
{
    public NewModalPage(NewModalViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}