using System.Linq.Expressions;
using System.Net;

namespace fushcasinaS6A.Views;

public partial class vAgregarEstudiante : ContentPage
{
	public vAgregarEstudiante()
	{
		InitializeComponent();
	}

	private void btnAgregar_Clicked(object sender, EventArgs e)
	{
		try
		{
            WebClient cliente = new WebClient();
			var parametros = new System.Collections.Specialized.NameValueCollection();
			parametros.Add("nombre", txtNombre.Text);
            parametros.Add("apellido", txtApellido.Text);
            parametros.Add("edad", txtEdad.Text);

			cliente.UploadValues("http://localhost/ws_estudiante/estudiante_ws.php", "POST", parametros);
			Navigation.PushAsync(new vEstudiante());
        }
		catch (Exception ex)
		{
			DisplayAlertAsync("Alerta", "Error: " + ex.Message, "OK");
        }
	}

	private void btnCancelar_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new vEstudiante());
	}
}