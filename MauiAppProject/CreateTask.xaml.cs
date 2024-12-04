using MauiAppProject.DataAccess;
using System.Collections.ObjectModel;
using System.Formats.Tar;
using System.Threading.Tasks;

namespace MauiAppProject;

public partial class CreateTask : ContentPage
{
	public CreateTask(DataAccess.Task task)
	{
		InitializeComponent();
        BindingContext = task;
	}

    private void SaveTaskClicked(object sender, EventArgs e)
    {

    }

    private async void Salir_Clicked(object sender, EventArgs e)
    {
        var advice = await DisplayAlert("Advertencia", $"La informacion ingresada se perdera", "OK", "Cancelar");
        if (advice)
        {
            await Navigation.PopAsync();
        }
    }
}