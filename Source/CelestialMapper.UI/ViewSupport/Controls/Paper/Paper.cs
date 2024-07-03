namespace CelestialMapper.UI;

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

    #region PaperContent property

    //public IList PaperContent
    //{
    //    get { return (IList)GetValue(PaperContentProperty); }
    //    set { SetValue(PaperContentProperty, value); }
    //}

    //public static readonly DependencyProperty PaperContentProperty =
    //    Register(nameof(PaperContent), new PlatformPropertyMetadata<Paper, IList>(Enumerable.Empty<object>().ToList(), OnPaperContentChanged));

    //private static void OnPaperContentChanged(Paper d, DependencyPropertyChangedEventArgs<IList> e)
    //{

    //}

    #endregion

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
