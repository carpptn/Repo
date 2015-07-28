
namespace PUConnector.Api.UI
{
    public class PUUIListener : System.Diagnostics.TraceListener
    {
        string msg = null;

        public override void Write(string message)
        {
            msg = message;
        }

        public override void WriteLine(string message)
        {
            if (Program.mainForm != null && Program.mainForm.rtbConsole != null)
                Program.mainForm.rtbConsole.Text = msg + " " + message + "\n\n" + Program.mainForm.rtbConsole.Text;
            msg = null;
        }
    }
}
