using System.Text;
using System.Text.Json;
using Veterinary.Models;

namespace SimulacroDos.Utils;

public class MailersendUtils
{
    public async void EnviarCorreo(string userEmail, DateTime citaDate)
    {
        // URL de destino para la solicitud POST de la API Mailersend:
        string url = "https://api.mailersend.com/v1/email";

        // Token de autorización para la solicitud:
        string tokenEmail = "mlsn.1778844d8f0a7044308bbbc40cb353d40de859419ed90966bcc4bb3e70e6827a";

        // Se crea ua instancia de la clase Email para contener el mensaje:
        var emailMessage = new Email
        {
            from = new From {email = "MS_S5OpH9@trial-vywj2lp77qml7oqz.mlsender.net"},
            to =
            [
                new To {email = userEmail}
            ],
            subject = "Cita Médica",
            text = $"¡Tu cita ha sido programa para la fecha: {citaDate}!",
            html = $"¡Tu cita ha sido programa para la fecha: {citaDate}!"
        };

        // Serializar el objeto emailMessage en formato JSON:
        string jsonBody = JsonSerializer.Serialize(emailMessage);

        // Crear un objeto HTTPClient para realizar la solicitud HTTP:
        using (HttpClient client = new HttpClient())
        {
            // Configurar el encabezado Content-Type para indicar que el cuerpo es JSON:
            // client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            // // Configurar el encabezado de Authorization para indicar el token de autorización:
            // client.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenEmail}");

            // Configurar el encabezado de Authorization para indicar el token de autorización:
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenEmail);

            // Crear el contenido de la solicitud POST como StringContent:
            StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            // Realizar la solicitud POST a la URL indicada:
            HttpResponseMessage response = await client.PostAsync(url, content);

            // Se confirma si la solicitud fue éxitosa (código de estado: 200 - 209):
            if(response.IsSuccessStatusCode)
            {
                // Se muestra el estado de la solicitud:
                Console.WriteLine($"Estado de la solicitud: {response.StatusCode}");
            }
            else
            {
                // Si la solicitud no fue éxitosa, se muestra el estado de la solicitud:
                Console.WriteLine($"La solicitud falló con el código de estado: {response.StatusCode}");
            }
        }
    }
}