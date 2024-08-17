namespace CelestialMapper.ViewModel;

public interface ICommandZIndexParameters : ICommandMultiParameters
{

    public object Sender { get; set; }

    public ZIndexAction ZIndexAction { get; set; }

}
