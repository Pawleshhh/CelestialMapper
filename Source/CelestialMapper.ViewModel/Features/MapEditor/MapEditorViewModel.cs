using CelestialMapper.Core.Astronomy;
using CelestialMapper.Core.Infrastructure.Map;
using System.Globalization;
using System.Windows.Input;

namespace CelestialMapper.ViewModel;

[Export(typeof(MapEditorViewModel), IsSingleton = false, Key = nameof(MapEditorViewModel))]
public class MapEditorViewModel : ViewModelBase
{

    #region Fields

    #endregion

    #region Constructors

    public MapEditorViewModel(
        IViewModelSupport viewModelSupport) : base(viewModelSupport)
    {
    }

    #endregion

    #region Base

    public override FeatureName DefaultFeatureName => FeatureNames.MapEditor;

    public override void Initialize(IViewModelConfigurator configurator)
    {
        base.Initialize(configurator);
    }

    #endregion

    #region Commands

    #endregion

    #region Properties


    #endregion

    #region Methods

    #endregion

    #region Helpers

    #endregion

}
