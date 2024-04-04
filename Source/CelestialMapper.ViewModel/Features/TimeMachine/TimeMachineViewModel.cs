﻿using PracticalAstronomy.CSharp;
using System.Windows.Input;

namespace CelestialMapper.ViewModel;

[Export(typeof(TimeMachineViewModel), IsSingleton = true, Key = nameof(TimeMachineViewModel))]
public class TimeMachineViewModel : ViewModelBase
{

    #region Fields

    private readonly Geographic defaultLocation = new(0, 0);

    private readonly ITimeMachineManager timeMachineManager;

    #endregion

    #region Constructors

    public TimeMachineViewModel(
        ITimeMachineManager timeMachineManager,
        IViewModelSupport viewModelSupport) : base(viewModelSupport)
    {
        this.timeMachineManager = timeMachineManager;
    }

    #endregion

    #region ViewModelBase

    public override string DefaultFeatureName => FeatureNames.TimeMachine;

    public override void Initialize(IViewModelConfigurator configurator)
    {
        base.Initialize(configurator);

        DateTime = DateTime.Now;

        ApplyCommand = new RelayCommand(o =>
        {
            this.timeMachineManager.Update(DateTime, new(Latitude, Longitude));
        });
    }

    protected override void SubscribeToEvents()
    {
        base.SubscribeToEvents();
        this.timeMachineManager.LocationChanged += TimeMachineManager_LocationChanged;
        this.timeMachineManager.DateTimeChanged += TimeMachineManager_DateTimeChanged;
        this.timeMachineManager.TimeMachineUpdated += TimeMachineManager_TimeMachineUpdated;
    }

    protected override void UnsubscribeFromEvents()
    {
        base.UnsubscribeFromEvents();
        this.timeMachineManager.LocationChanged -= TimeMachineManager_LocationChanged;
        this.timeMachineManager.DateTimeChanged -= TimeMachineManager_DateTimeChanged;
        this.timeMachineManager.TimeMachineUpdated -= TimeMachineManager_TimeMachineUpdated;
    }

    #endregion

    #region Event handlers

    private void TimeMachineManager_TimeMachineUpdated(PlatformEventArgs<ITimeMachineManager, (DateTime DateTime, Geographic Location)> e)
    {
        DateTime = e.Data.DateTime;
        Longitude = e.Data.Location.Longitude;
        Latitude = e.Data.Location.Latitude;
    }

    private void TimeMachineManager_DateTimeChanged(PlatformEventArgs<ITimeMachineManager, DateTime> e)
    {
        DateTime = e.Data;
    }

    private void TimeMachineManager_LocationChanged(PlatformEventArgs<ITimeMachineManager, Geographic> e)
    {
        if (e.Data is null)
        {
            (Latitude, Longitude) = this.defaultLocation;
        }

        (Latitude, Longitude) = e.Data;
    }

    #endregion

    #region Properties

    public ICommand? ApplyCommand { get; private set; }

    public DateTime DateTime
    {
        get => GetPropertyValue<DateTime>();
        set => SetPropertyValue(value);
    }

    public double Latitude
    {
        get => GetPropertyValue<double>();
        set => SetPropertyValue(value);
    }

    public double Longitude
    {
        get => GetPropertyValue<double>();
        set => SetPropertyValue(value);
    }

    #endregion

}
