using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mangaroUI
{
    public partial class Form1 : Form
    {
        public string username = "no establecido";
        public int previousData = 0;
        private Microsoft.AspNetCore.SignalR.Client.HubConnection connection;

        public Form1(string u)
        {
            username = u;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Text = username;
            timer1.Start();
            InitializeSignalR();
        }
        public async Task updateUserListAsync()
        {
            string url = "https://localhost:7080/getOnlineUsers";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Realiza la solicitud HTTP GET
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Asegúrate de que la respuesta fue exitosa
                    response.EnsureSuccessStatusCode();

                    // Lee el contenido de la respuesta como una cadena
                    string responseData = await response.Content.ReadAsStringAsync();
                    responseData = responseData.Replace("\"]","X");
                    checkedListBox1.Items.Add(responseData);
                }
                catch (HttpRequestException e)
                {
                    // Maneja cualquier error que ocurra durante la solicitud HTTP
                    Console.WriteLine($"Error: {e.Message}");
                    
                }
            }
        }
        private async void InitializeSignalR()
        {
            // Configura la conexión al servidor de SignalR
            connection = new HubConnectionBuilder()
                //.WithUrl("http://216.172.100.170:8088/chathub") production
                .WithUrl("https://localhost:7080/chathub?username="+username)
                
                .Build();

            // Suscríbete a un método del hub para recibir mensajes
            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                // Asegúrate de actualizar el TextBox en el hilo de la interfaz de usuario
                this.Invoke((Action)(() =>
                {
                    if (user != username)
                    {
                        PlaySystemSound();
                    }
                    appendMessage("@" + user + ": " + message + Environment.NewLine);
                }));
            });
            
            connection.On<string>("UserConnected", (user) =>
            {
                // Asegúrate de actualizar el TextBox en el hilo de la interfaz de usuario
                this.Invoke((Action)(() =>
                {
                    PlaySystemSound();                    
                    status.Text = "# Nuevo usuario conectado" + user;
                    updateUserListAsync();
                }));
            });



            try
            {
                // Inicia la conexión
                await connection.StartAsync();
                //MessageBox.Show("Conectado a SignalR!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar a SignalR: " + ex.Message);
            }
            //await connection.InvokeAsync("UserConnected", username);
        }

        private void PlaySystemSound()
        {
            // Reproduce el sonido de Asterisk del sistema
            //SystemSounds.Asterisk.Play();

            // O puedes elegir uno de los siguientes:
            //SystemSounds.Beep.Play();
            //SystemSounds.Exclamation.Play();
            SystemSounds.Hand.Play();
            //SystemSounds.Question.Play();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            
            string message = textBox2.Text;
            await connection.InvokeAsync("SendMessage", username, message);

            //await DoSomethingAsync(message,username);
            textBox2.Clear();
            reloadMessagesAsync();
        }

        private void appendMessage(string message)
        {
            textBox1.AppendText(message + Environment.NewLine);
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
        }

        private async Task<string> reloadMessagesAsync()
        {
            string data = await GetHttpDataAsync("http://216.172.100.170:8088/getAllMessages");
            previousData = data.ToString().Length;
            appendMessage(data.ToString()
                .Replace("{", "@")
                .Replace("[", "")
                .Replace("]", "---------------")
                .Replace("}", "\r\n\r\n")
                .Replace('"', ' ')
                .Replace(",", "")
                .Replace("sendDate :", "\r\n")
                .Replace("message ", "")
                .Replace("username :", "")
                );

            return data.ToString();
        }

        private async Task DoSomethingAsync(string msj, string usr)
        {
            try
            {
                var connection = new HubConnectionBuilder()
                    .WithUrl("http://216.172.100.170:8088/chathub") // Asegúrate de que esta URL es correcta
                    .Build();

                connection.On<string, string>("ReceiveMessage", (user, message) =>
                {
                    Console.WriteLine($"{user}: {message}");
                });

                await connection.StartAsync();
                Console.WriteLine("Conectado a SignalR.");
                await connection.InvokeAsync("SendMessage", usr, msj);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar: {ex.Message}");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            string newData = await reloadMessagesAsync();
            verifyNewMessage(newData.Length);
        }

        private async Task verifyNewMessage(int newDataLenth)
        {
            if (previousData < newDataLenth)
            {
                PlaySystemSound();
            }
        }

        public async Task<string> GetHttpDataAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Realiza la solicitud HTTP GET
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Asegúrate de que la respuesta fue exitosa
                    response.EnsureSuccessStatusCode();

                    // Lee el contenido de la respuesta como una cadena
                    string responseData = await response.Content.ReadAsStringAsync();

                    return responseData;
                }
                catch (HttpRequestException e)
                {
                    // Maneja cualquier error que ocurra durante la solicitud HTTP
                    Console.WriteLine($"Error: {e.Message}");
                    return null;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(textBox2.Text))
            {
                e.SuppressKeyPress = true; // Evita el sonido de "ding" y la nueva línea en el TextBox
                string message = textBox2.Text;
                await connection.InvokeAsync("SendMessage", username, message);

                //await DoSomethingAsync(message,username);
                textBox2.Clear();
                reloadMessagesAsync();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
