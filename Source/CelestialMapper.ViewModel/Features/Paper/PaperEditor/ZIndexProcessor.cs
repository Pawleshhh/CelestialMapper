namespace CelestialMapper.ViewModel;

[Export(typeof(IZIndexProcessor), typeof(ZIndexProcessor), IsSingleton = true, Key = nameof(ZIndexProcessor))]
public class ZIndexProcessor : IZIndexProcessor
{
    public void Process(IPaperItem source, ZIndexAction action)
    {
        return;
    }
}
