namespace MauiApp1;
using MauiAppProject.DataAccess;
public partial class Login : ContentPage
{
    ProjectDbContext db = new ProjectDbContext();
	public Login()
	{
		InitializeComponent();
        
	}

    private void CreateAccount_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushModalAsync(new CreateAccount());
        }
        catch (Exception ex) {
            DisplayAlert("Error", $"{ex}", "OK");
        }
    }

    private void Login_Clicked(object sender, EventArgs e)
    {
        try {
            string User = user.Text;
            string Password = password.Text;

            var LogUser = db.Usuarios.FirstOrDefault(u => u.correo == User);

            if (LogUser != null)
            {
                if (LogUser.contraseña == Password)
                {
                    //var userProject = db.ProyectoUsuario.Where(pu => pu.userId == LogUser.identificacion);
                    DisplayAlert("Info", "Acceso concedido.", "OK");
                    Navigation.PushAsync(new Projects(LogUser));
                }
                else {

                    DisplayAlert("Error", "Usuario o contraseña incorrecta.", "OK");
                }
                
            }
            else {

                DisplayAlert("Error", "Usuario o contraseña incorrecta.", "OK");
            }


        }
        catch (Exception ex) {

            DisplayAlert("Error", "Error: " + ex.Message, "OK");


        }
    }
}