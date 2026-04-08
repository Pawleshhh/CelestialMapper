namespace CelestialMapper.UI;

using Microsoft.Extensions.DependencyInjection;
using System;
using static CelestialMapper.UI.DependencyPropertyHelper;

public class FactoryView : ContentControl
{

    public FactoryView()
    {
        
    }

    public FeatureName FeatureName
    {
        get { return (FeatureName)GetValue(FeatureNameProperty); }
        set { SetValue(FeatureNameProperty, value); }
    }

    public static readonly DependencyProperty FeatureNameProperty =
        Register(nameof(FeatureName), new PlatformPropertyMetadata<FactoryView, FeatureName>(FeatureName.Unknown, OnFeatureNameChanged));

    private static void OnFeatureNameChanged(FactoryView d, DependencyPropertyChangedEventArgs<FeatureName> e)
    {
        if (!d.IsInitialized || !d.IsLoaded)
        {
            return;
        }
        Initialize(d);
    }

    public bool DoNotInitializeViewModel
    {
        get { return (bool)GetValue(DoNotInitializeViewModelProperty); }
        set { SetValue(DoNotInitializeViewModelProperty, value); }
    }

    public static readonly DependencyProperty DoNotInitializeViewModelProperty =
        Register(nameof(DoNotInitializeViewModel), new PlatformPropertyMetadata<FactoryView, bool>(false));

    public object? DataForViewModel
    {
        get { return (object?)GetValue(DataForViewModelProperty); }
        set { SetValue(DataForViewModelProperty, value); }
    }

    public static readonly DependencyProperty DataForViewModelProperty =
        Register(nameof(DataForViewModel), new PlatformPropertyMetadata<FactoryView, object?>(OnDataForViewModelChanged));

    private static void OnDataForViewModelChanged(FactoryView d, DependencyPropertyChangedEventArgs<object?> e)
    {
        if (d.RequiresData)
        {
            Initialize(d);
        }
    }

    public bool RequiresData { get; set; }

    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);

        Initialize(this);
    }

    private static void Initialize(FactoryView factoryView)
    {
        if (factoryView.RequiresData && factoryView.DataForViewModel is null)
        {
            return;
        }

        if (factoryView?.FeatureName is null)
        {
            return;
        }

        if (factoryView.FeatureName.IsUnknown())
        {
            // TODO: Create unknow view for such cases
            return;
        }

        var view = App.ServiceProvider.GetKeyedService<FeatureViewBase>(factoryView.FeatureName.ViewName);

        if (view is null)
        {
            // TODO: Create unknown view for such cases
            return;
        }

        view.DataForViewModel = factoryView.DataForViewModel;
        view.DoNotInitializeViewModel = factoryView.DoNotInitializeViewModel;
        view.FeatureName = factoryView.FeatureName;

        factoryView.Content = view;
    }

}
