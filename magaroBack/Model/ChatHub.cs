using magaroBack.Interface;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Xml;

namespace magaroBack.Model
{
    public class ChatHub : Hub,IChatHub
    {

        public ChatHub()
        {
        }
        public bool saveMessage(Message message)
        {
            string connectionString = "Server=DESKTOP-V1NJ5R4\\SQLEXPRESS;Database=magaro;User Id=sa;Password=C0mpl3j0;";

            string query = "INSERT INTO chat (message, username,senddate) VALUES (@message, @username,@senddate)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@message", message.message);
                command.Parameters.AddWithValue("@username", message.username);
                command.Parameters.AddWithValue("@senddate", message.sendDate);

                try
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Verificar si el registro se insertó correctamente
                    if (result < 0)
                    {
                        Console.WriteLine("Error al insertar los datos en la base de datos.");
                    }
                    else
                    {
                        return true;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
                return false;
            }
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
            Message msj = new Message { message = message, username = user, sendDate = DateTime.Now };
            saveMessage(msj);
        }

        public IEnumerable<Message> readAllMessages()
        {
            var entities = new List<Message>();
            string connectionString = "Server=DESKTOP-V1NJ5R4\\SQLEXPRESS;Database=magaro;User Id=sa;Password=C0mpl3j0;";


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM chat", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entities.Add(new Message
                            {
                                username = reader.GetString(0),
                                message = reader.GetString(1),
                                sendDate = reader.GetDateTime(2)
                            });
                        }
                    }
                }
            }

            return entities;
        }
    }
    
}
