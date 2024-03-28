namespace CelestialMapper.Common;

public delegate void PlatformEventHandler<TEventArgs>(TEventArgs e)
    where TEventArgs : EventArgs;

public class PlatformEventArgs<TSender, TData> : EventArgs
{

    public TSender? Sender { get; init; }

    public TData? Data { get; init; }

    public PlatformEventArgs()
    {

    }

    public PlatformEventArgs(TSender sender, TData data)
    {
        Sender = sender;
        Data = data;
    }

}
