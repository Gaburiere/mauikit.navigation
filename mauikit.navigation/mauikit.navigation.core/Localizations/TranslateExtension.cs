namespace mauikit.navigation.core.Localizations
{
    /// <summary>
    /// A XAML markup extension that provides automatic localization support
    /// by binding to a <see cref="Localizator"/> service.
    /// </summary>
    /// <remarks>
    /// <para>This extension enables you to translate text directly in XAML by specifying the resource key. When the current culture changes, the text automatically updates.</para>
    /// <para>Usage in XAML:</para>
    /// <code>
    /// xmlns:localizations="clr-namespace:mauikit.navigation.core.Localizations;assembly=mauikit.navigation.core"
    ///
    /// &lt;Label Text="{localizations:Translate Name=HelloKey}" /&gt;
    /// &lt;Label Text="{localizations:Translate Name=GreetingKey, StringFormat='Welcome: {0}'}" /&gt;
    /// </code>
    /// </remarks>
    [ContentProperty(nameof(Name))]
    [AcceptEmptyServiceProvider]
    public partial class TranslateExtension : BindableObject, IMarkupExtension<BindingBase>
    {
        private readonly Localizator _localizator;

        /// <summary>
        /// Identifies the <see cref="Name"/> bindable property.
        /// </summary>
        public static BindableProperty NameProperty = BindableProperty.Create(
            nameof(Name),
            typeof(string),
            typeof(TranslateExtension),
            null,
            propertyChanged: (b, o, n) => ((TranslateExtension)b).OnTranslatedNameChanged());

        /// <summary>
        /// Gets or sets the resource key to translate.
        /// </summary>
        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }

        /// <summary>
        /// Gets or sets an optional string format applied to the translated value.
        /// </summary>
        public string? StringFormat { get; set; }

        /// <summary>
        /// Gets the translated string for the specified <see cref="Name"/>.
        /// Automatically updates when the application culture changes.
        /// </summary>
        public string? TranslatedName
            => (Name is string name && _localizator[name] is string translatedName)
                ? translatedName
                : Name;

        /// <summary>
        /// Notifies that the translated text has changed.
        /// </summary>
        public void OnTranslatedNameChanged() => OnPropertyChanged(nameof(TranslatedName));

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslateExtension"/> class
        /// and subscribes to <see cref="Localizator.PropertyChanged"/> to refresh text
        /// when the culture changes.
        /// </summary>
        public TranslateExtension()
        {
            _localizator ??= IPlatformApplication.Current!.Services.GetRequiredService<Localizator>();
            _localizator.PropertyChanged += (s, e) => OnTranslatedNameChanged();
        }

        /// <summary>
        /// Provides the translated value binding used by XAML.
        /// </summary>
        /// <param name="serviceProvider">The service provider used by the XAML parser.</param>
        /// <returns>A <see cref="BindingBase"/> bound to the translated string.</returns>
        public BindingBase ProvideValue(IServiceProvider serviceProvider)
            => BindingBase.Create<TranslateExtension, string?>(
                static source => source.TranslatedName,
                mode: BindingMode.OneWay,
                source: this,
                stringFormat: StringFormat);

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
            => ProvideValue(serviceProvider);
    }
}
