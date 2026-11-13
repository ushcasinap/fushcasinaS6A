using fushcasinaS6A.Models;
using System.Net;

namespace fushcasinaS6A.Views;

public partial class vActualizarEliminar : ContentPage
{
    // Misma URL del servicio web en XAMPP que usa vEstudiante
    private readonly string Url = DeviceInfo.Platform == DevicePlatform.Android
        ? "http://10.0.2.2/ws_estudiante/estudiante_ws.php"
        : "http://localhost/ws_estudiante/estudiante_ws.php";

	public vActualizarEliminar(Estudiante  datos)
	{
		InitializeComponent();
		txtCodigo.Text = datos.codigo.ToString();
		txtNombre.Text = datos.nombre;
		txtApellido.Text = datos.apellido;
		txtEdad.Text = datos.edad.ToString();
	}

	private async void btnActualizar_Clicked(object sender, EventArgs e)
	{
		try
		{
            // Validar los datos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtEdad.Text))
            {
                await DisplayAlertAsync("ERROR", "Ingrese nombre, apellido y edad", "Cerrar");
                return;
            }

            if (!int.TryParse(txtEdad.Text, out int edad) || edad <= 0)
            {
                await DisplayAlertAsync("ERROR", "La edad debe ser un número válido", "Cerrar");
                return;
            }

          
            string urlActualizar = Url +
                "?codigo=" + Uri.EscapeDataString(txtCodigo.Text) +
                "&nombre=" + Uri.EscapeDataString(txtNombre.Text.Trim()) +
                "&apellido=" + Uri.EscapeDataString(txtApellido.Text.Trim()) +
                "&edad=" + edad;

            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            cliente.UploadValues(urlActualizar, "PUT", parametros);

            await DisplayAlertAsync("Alerta", "Estudiante actualizado correctamente", "OK");

            // Regresa a la lista (se actualiza sola en OnAppearing)
            await Navigation.PopAsync();
        }
		catch (Exception ex)
		{
			await DisplayAlertAsync("Alerta", "Error: " + ex.Message, "OK");
		}
	}

	private async void btnEliminar_Clicked(object sender, EventArgs e)
	{
		try
		{
            // Confirmar antes de eliminar
            bool respuesta = await DisplayAlertAsync("Eliminar",
                                                     "¿Desea eliminar a " + txtNombre.Text + " " + txtApellido.Text + "?",
                                                     "Sí", "No");
            if (!respuesta)
                return;

           
            string urlEliminar = Url + "?codigo=" + Uri.EscapeDataString(txtCodigo.Text);

            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            cliente.UploadValues(urlEliminar, "DELETE", parametros);

            await DisplayAlertAsync("Alerta", "Estudiante eliminado correctamente", "OK");

            // Regresa a la lista (se actualiza sola en OnAppearing)
            await Navigation.PopAsync();
        }
		catch (Exception ex)
		{
			await DisplayAlertAsync("Alerta", "Error: " + ex.Message, "OK");
		}
	}
}
