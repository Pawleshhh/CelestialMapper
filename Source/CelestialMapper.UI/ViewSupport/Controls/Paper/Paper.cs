namespace CelestialMapper.UI;

using CelestialMapper.ViewModel;
using System;
using System.Collections.Specialized;
using static CelestialMapper.UI.DependencyPropertyHelper;

public class Paper : PlatformItemsControl
{

    public static readonly string PaperDefaultStyleKey = "Style.Paper";

    public Paper()
    {
        Style = TryFindResource(PaperDefaultStyleKey) as Style;
    }

    public Paper(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Style = TryFindResource(PaperDefaultStyleKey) as Style;
    }

    #region PaperSize property

    public PaperSize PaperSize
    {
        get { return (PaperSize)GetValue(PaperSizeProperty); }
        set { SetValue(PaperSizeProperty, value); }
    }

    public static readonly DependencyProperty PaperSizeProperty =
        Register(nameof(PaperSize), new PlatformPropertyMetadata<Paper, PaperSize>(PaperSize.A4, OnPaperSizeChanged));

    private static void OnPaperSizeChanged(Paper d, DependencyPropertyChangedEventArgs<PaperSize> e)
    {
        d.ApplyPaperSize(e.NewValue);
    }

    #endregion

    #region ZIndexCommand

    public RelayCommand<ZIndexAction> ZIndexActionCommand
    {
        get => this.GetValue<RelayCommand<ZIndexAction>>(ZIndexActionCommandProperty);
        set => SetValue(ZIndexActionCommandProperty, value);
    }

    public static readonly DependencyProperty ZIndexActionCommandProperty =
        Register(nameof(ZIndexActionCommand), new PlatformPropertyMetadata<Paper, RelayCommand<ZIndexAction>>());

    #endregion

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        ApplyPaperSize(PaperSize);
    }

    protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
    {
        base.OnItemsChanged(e);
    }

    private void ApplyPaperSize(PaperSize paperSize)
    {
        var size = paperSize.GetPaperSizeInPixels();

        Width = size.Width;
        Height = size.Height;
    }
}
