using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiApp1;

public partial class CreateProject : ContentPage
{
    public ObservableCollection<string> Tasks { get; set; }
    public ICommand RemoveTaskCommand { get; set; }

    public CreateProject()
    {
        InitializeComponent();
        Tasks = new ObservableCollection<string>();
        TasksCollectionView.ItemsSource = Tasks;

    }

    private void OnAddTaskClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(TaskEntry.Text))
        {
            Tasks.Add(TaskEntry.Text);
            TaskEntry.Text = string.Empty;
        }
    }

    private void OnRemoveTaskClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var task = button?.BindingContext as string;

        if (!string.IsNullOrEmpty(task) && Tasks.Contains(task))
        {
            Tasks.Remove(task);
        }
    }

    private async void OnSaveProjectClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ProjectIdEntry.Text) ||
            string.IsNullOrWhiteSpace(ProjectNameEntry.Text) ||
            string.IsNullOrWhiteSpace(ProjectTypeEntry.Text))
        {
            await DisplayAlert("Error", "Todos los campos deben estar completos.", "OK");
            return;
        }

        if (!int.TryParse(ProjectIdEntry.Text, out var projectId))
        {
            await DisplayAlert("Error", "El ID del proyecto debe ser un número.", "OK");
            return;
        }

        var newProject = new Project
        {
            Id = projectId,
            ProjectName = ProjectNameEntry.Text,
            ProjectType = ProjectTypeEntry.Text,
            Tasks = new ObservableCollection<string>(Tasks)
        };

        bool advice = await DisplayAlert("Notificación", $"¿La información del proyecto {ProjectNameEntry.Text} es correcta?", "Si", "No");
        if (advice)
        {
            await Navigation.PopAsync();
            MessagingCenter.Send(this, "objData", newProject);
        }
    }

    private async void Salir_Clicked(object sender, EventArgs e)
    {
        var advice = await DisplayAlert("Advertencia", "La información ingresada se perderá", "OK", "Cancelar");
        if (advice)
        {
            await Navigation.PopAsync();
        }
    }
}
