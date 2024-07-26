using magaroBack.Model;

namespace magaroBack.Interface
{
    public interface IChatHub
    {
        public Task SendMessage(string user, string message);
        public bool saveMessage(Message message);
        public IEnumerable<Message> readAllMessages();
    }
}
