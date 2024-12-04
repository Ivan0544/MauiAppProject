using System.Collections.ObjectModel;
using System.Formats.Tar;

namespace MauiApp1
{
    public partial class UpdateProject : ContentPage
    {
        public ObservableCollection<string> Tasks { get; set; }

        public UpdateProject(Project project)
        {
            InitializeComponent();

            ProjectIdEntry.Text = project.Id.ToString();
            ProjectNameEntry.Text = project.ProjectName;
            ProjectTypeEntry.Text = project.ProjectType;
            Tasks = new ObservableCollection<string>(project.Tasks);
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
            var button = (Button)sender;
            var task = button.BindingContext as string;

            if (task != null)
            {
                Tasks.Remove(task);
            }
        }
        private async void OnSaveProjectClicked(object sender, EventArgs e)
        {
            var updatedProject = new Project
            {
                Id = int.Parse(ProjectIdEntry.Text),
                ProjectName = ProjectNameEntry.Text,
                ProjectType = ProjectTypeEntry.Text,
                Tasks = new ObservableCollection<string>(Tasks)
            };

            bool confirm = await DisplayAlert("Confirmación", "¿Está seguro de guardar los cambios?", "Sí", "No");
            if (confirm)
            {
                await Navigation.PopAsync();

                MessagingCenter.Send(this, "UpdatedProject", updatedProject);
            }
        }
        private async void Salir_Clicked(object sender, EventArgs e)
        {
            var confirm = await DisplayAlert("Advertencia", "¿Desea salir sin guardar los cambios?", "Sí", "No");
            if (confirm)
            {
                await Navigation.PopAsync();
            }
        }
    }
}
