using System.Windows.Forms;

namespace ProbabilityTheoryGameForBirthday
{
    public interface IMessageService
    {
        void ShowMessage(string message);
        void ShowSuccess(string success);
        void ShowDefeat(string defeat);
    }

    public class MessageService : IMessageService
    {
        private string _message;
        private string _success;
        private string _defeat;

        public MessageService(string message, string success, string defeat)
        {
            _message = message;
            _success = success;
            _defeat  = defeat;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, _message, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowSuccess(string success)
        {
            MessageBox.Show(success, _success, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowDefeat(string defeat)
        {
            MessageBox.Show(defeat, _defeat, MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
    }
}
