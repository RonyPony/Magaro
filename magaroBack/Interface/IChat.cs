using magaroBack.Model;

namespace magaroBack.Interface
{
    public interface IChat
    {
        public bool saveMessage(Message message);
        public IEnumerable<Message> readAllMessages();
    }
}
