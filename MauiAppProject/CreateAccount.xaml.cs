namespace MauiApp1;

public partial class CreateAccount : ContentPage
{
	public CreateAccount()
	{
		InitializeComponent();
	}

    private void Salir_Clicked(object sender, EventArgs e)
    {
		try
		{
			Navigation.PopModalAsync();
		}
		catch (Exception ex) { 
			
		}
    }
}