namespace CelestialMapper.ViewModel;

[Export(typeof(IZIndexProcessor), typeof(ZIndexProcessor), IsSingleton = true, Key = nameof(ZIndexProcessor))]
public class ZIndexProcessor : IZIndexProcessor
{
    public void Process(
        IEnumerable<IPaperItem> items,
        IPaperItem source, 
        ZIndexAction action)
    {
        var paperItems = items.OrderBy(x => x.ZIndex);
        var indexData = new ZIndexData(
            paperItems.MinBy(x => x.ZIndex)?.ZIndex ?? 0,
            paperItems.MaxBy(x => x.ZIndex)?.ZIndex ?? 0,
            paperItems,
            source);

        switch (action)
        {
            case ZIndexAction.BringToFront:
                BringToFront(indexData);
                break;
            case ZIndexAction.SendToBack:
                SendToBack(indexData);
                break;
            case ZIndexAction.BringForward:
                BringForward(indexData);
                break;
            case ZIndexAction.SendBackward:
                SendBackward(indexData);
                break;
            default:
                throw new InvalidOperationException($"Unexpected value of ZIndexAction: {action}");
        }
    }

    public void ProcessNewItem(IEnumerable<IPaperItem> paperItems, IPaperItem newItem)
    {
        newItem.ZIndex = paperItems.MaxBy(x => x.ZIndex)?.ZIndex + 1 ?? 0;
    }

    private void BringToFront(ZIndexData indexData)
    {
        if (indexData.SourceIndex >= indexData.MaxIndex)
        {
            return;
        }

        indexData.Source.ZIndex = indexData.MaxIndex + 1;
    }

    private void SendToBack(ZIndexData indexData)
    {
        if (indexData.SourceIndex <= indexData.MinIndex)
        {
            return;
        }

        indexData.Source.ZIndex = indexData.MinIndex - 1;
    }

    private void BringForward(ZIndexData indexData)
    {
        if (indexData.SourceIndex >= indexData.MaxIndex)
        {
            return;
        }

        var currentIndex = indexData.SourceIndex;
        var minOneIndexUp = indexData.Items.Where(x => x.ZIndex > currentIndex).Min()?.ZIndex;
        var itemsOneIndexUp = indexData.Items.Where(x => x.ZIndex == minOneIndexUp);

        itemsOneIndexUp.ForEach(x => x.ZIndex -= 1);
        indexData.Source.ZIndex += 1;
    }

    private void SendBackward(ZIndexData indexData)
    {
        if (indexData.SourceIndex <= indexData.MinIndex)
        {
            return;
        }

        var currentIndex = indexData.SourceIndex;
        var maxOneIndexDown = indexData.Items.Where(x => x.ZIndex < currentIndex).Max()?.ZIndex;
        var itemsOneIndexDown = indexData.Items.Where(x => x.ZIndex == maxOneIndexDown);

        itemsOneIndexDown.ForEach(x => x.ZIndex += 1);
        indexData.Source.ZIndex -= 1;
    }

    private record ZIndexData(int MinIndex, int MaxIndex, IOrderedEnumerable<IPaperItem> Items, IPaperItem Source)
    {
        public int SourceIndex => Source.ZIndex;
    }
}
