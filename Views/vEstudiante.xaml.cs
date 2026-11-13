using fushcasinaS6A.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace fushcasinaS6A.Views;

public partial class vEstudiante : ContentPage
{
    
    private readonly string Url = DeviceInfo.Platform == DevicePlatform.Android
        ? "http://10.0.2.2/ws_estudiante/estudiante_ws.php"
        : "http://localhost/ws_estudiante/estudiante_ws.php";
    
    private readonly HttpClient client = new HttpClient();
    //coleccion de datos
    private ObservableCollection<Estudiante> _estudiante = new ObservableCollection<Estudiante>();

    public vEstudiante()
    {
        InitializeComponent();
    }

    // Se ejecuta cada vez que se muestra la pagina:
    // asi la lista se actualiza al regresar de agregar un estudiante
    protected override void OnAppearing()
    {
        base.OnAppearing();
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

    private async void btnAgregarEstudiante_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new vAgregarEstudiante());
    }

    private async void listaEstudiante_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
       
        if (e.SelectedItem == null)
            return;

        var ObjetoEstudiante = (Estudiante)e.SelectedItem;

        // Quitar la seleccion para poder volver a tocar el mismo estudiante
        listaEstudiante.SelectedItem = null;

        await Navigation.PushAsync(new vActualizarEliminar(ObjetoEstudiante));
    }
}

