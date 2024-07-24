using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using System.Runtime.ConstrainedExecution;
public class Program
    {


    static async Task Main(string[] args)
    {
        try
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7080/chathub") // Asegúrate de que esta URL es correcta
                .Build();

            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Console.WriteLine($"{user}: {message}");
            });

            await connection.StartAsync();
            Console.WriteLine("Conectado a SignalR.");

            while (true)
            {
                Console.Write("Ingrese su nombre: ");
                var user = Console.ReadLine();

                Console.Write("Ingrese su mensaje: ");
                var message = Console.ReadLine();

                await connection.InvokeAsync("SendMessage", user, message);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al conectar: {ex.Message}");
        }
    }




    //public static string username = "";


    //public static void jump(int chrs = 50)
    //{
    //    for (int i = 0; i < chrs; i++)
    //    {
    //        Console.Write("#");
    //    }
    //}
    //public static void space()
    //{
    //    Console.WriteLine("");
    //    Console.WriteLine("");
    //    Console.WriteLine("");
    //}
    //public static void clean()
    //{
    //    Console.Clear();
    //}
    //private static void Main(string[] args)
    //{
    //    welcomeScreen();
    //    Console.ReadKey();
    //}

    //private static void welcomeScreen()
    //{
    //    Console.WriteLine("Welcome to MAGARO");
    //    jump();
    //    Console.ReadKey();
    //    selectUsername();
    //}


    //private static void selectUsername()
    //{
    //    Console.WriteLine("Please introduce your username");
    //    space();
    //    username = Console.ReadLine();

    //}

}