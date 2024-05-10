using System.Reflection;

namespace CelestialMapper.TestUtilities;

public class TestUtils
{
    public static void Raise<TEventArgs>(object source, string eventName, TEventArgs eventArgs) where TEventArgs : EventArgs
    {
        var eventDelegate = (MulticastDelegate?)source.GetType().GetField(eventName, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(source);
        if (eventDelegate != null)
        {
            foreach (var handler in eventDelegate.GetInvocationList())
            {
                handler.Method.Invoke(handler.Target, new object[] { source, eventArgs });
            }
        }
    }

}
