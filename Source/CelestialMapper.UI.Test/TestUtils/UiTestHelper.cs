namespace CelestialMapper.UI.Test;

internal class UiTestHelper
{

    private Exception? capturedException;

    public void RunTest(Action action)
    {
        var thread = new Thread(() =>
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                this.capturedException = ex;
            }
        });
        thread.SetApartmentState(ApartmentState.STA);

        thread.Start();
        thread.Join();

        try
        {
            if (this.capturedException != null)
            {
                throw this.capturedException;
            }
        }
        finally
        {
            this.capturedException = null;
        }
    }

}
