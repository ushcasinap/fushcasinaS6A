using fushcasinaS6A.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace fushcasinaS6A.Views;

public partial class vEstudiante : ContentPage
{
    // URL del servicio web en XAMPP (la misma que se prueba en Postman)
    // En Windows se usa localhost; en el emulador Android localhost es el propio emulador, por eso se usa 10.0.2.2
    private readonly string Url = DeviceInfo.Platform == DevicePlatform.Android
        ? "http://10.0.2.2/ws_estudiante/estudiante_ws.php"
        : "http://localhost/ws_estudiante/estudiante_ws.php";
    //protocolo de comunicacion http
    private readonly HttpClient client = new HttpClient();
    //coleccion de datos
    private ObservableCollection<Estudiante> _estudiante = new ObservableCollection<Estudiante>();

    public vEstudiante()
    {
        InitializeComponent();
        MostrarEstudiantes();
    }

    public async void MostrarEstudiantes()
    {
        try
        {
            var content = await client.GetStringAsync(Url);
            List<Estudiante>? _mostrar = JsonConvert.DeserializeObject<List<Estudiante>>(content);

            _estudiante = new ObservableCollection<Estudiante>(_mostrar ?? new List<Estudiante>());
            listaEstudiante.ItemsSource = _estudiante;
        }
        catch (Exception ex)
        {
            lblMensaje.Text = "Error: " + ex.Message;
        }
    }
}
