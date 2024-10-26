namespace CelestialMapper.ViewModel;

public interface IZIndexProcessor
{

    public void Process(IEnumerable<IPaperItem> paperItems, IPaperItem source, ZIndexAction action);

    public void ProcessNewItem(IEnumerable<IPaperItem> paperItems, IPaperItem newItem);

}
