namespace CelestialMapper.Common;

public delegate void PlatformEventHandler<TSender, TEventArgs>(TSender sender, TEventArgs e)
    where TEventArgs : EventArgs;

public class PlatformEventArgs<TData> : EventArgs
{


    public TData? Data { get; init; }

    public PlatformEventArgs()
    {

    }

    public PlatformEventArgs(TData data)
    {
        Data = data;
    }

}
