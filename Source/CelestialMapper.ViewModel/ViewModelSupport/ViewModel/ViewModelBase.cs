﻿namespace CelestialMapper.ViewModel;

public abstract class ViewModelBase : NotifyPropertyChangedBase, IViewModel
{

    #region Fields

    private readonly IViewModelSupport viewModelSupport;

    #endregion

    #region Constructors

    public ViewModelBase(IViewModelSupport viewModelSupport)
    {
        this.viewModelSupport = viewModelSupport;
    }

    #endregion

    #region Properties

    public abstract FeatureName DefaultFeatureName { get; }
    public FeatureName FeatureName
    {
        get => GetPropertyValue<FeatureName>() ?? FeatureName.Unknown;
        private set => SetPropertyValue(value);
    }
    public string Name
    {
        get => GetPropertyValue<string>() ?? string.Empty;
        private set => SetPropertyValue(value);
    }

    public bool IsInitialized { get; private set; }

    #endregion

    #region Methods

    public virtual void Initialize(IViewModelConfigurator configurator)
    {
        FeatureName = configurator.GetFeatureName();
        Name = GetName();

        SubscribeToEvents();

        IsInitialized = true;
    }

    public virtual void Unitilialize()
    {
        UnsubscribeFromEvents();
    }

    protected virtual void SubscribeToEvents()
    {

    }

    protected virtual void UnsubscribeFromEvents()
    {

    }

    protected virtual string GetName()
    {
        this.viewModelSupport.ResourceResolver.TryResolveString($"String.FeatureName.{FeatureName.Name}", out var name);
        return name;
    }

    public virtual Dictionary<FeatureName, IViewModelConfigurator> InitializeConfigurators()
    {
        return new()
        {
            [DefaultFeatureName] = IViewModelConfigurator.Create(DefaultFeatureName)
        };
    }

    protected TViewModel GetViewModel<TViewModel>(FeatureName featureName, Action<TViewModel>? postConfigure = null)
        where TViewModel : IViewModel
    {
        return this.viewModelSupport.IoCManager.ServiceProvider.ResolveViewModel<TViewModel>(featureName, postConfigure);
    }
    #endregion

}
