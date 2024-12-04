using MauiAppProject;
using MauiAppProject.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.ObjectModel;
using System.Net.WebSockets;

namespace MauiApp1;

public class Project
{
    public int Id { get; set; }
    public string ProjectName { get; set; }
    public string ProjectType { get; set; }
    public ObservableCollection<string> Tasks { get; set; }
}

public partial class Projects : ContentPage
{
    public ObservableCollection<Project> objProject { get; set; }
    

    public  Projects(MauiAppProject.DataAccess.usuario user)
    {
        InitializeComponent();
        BindingContext = user;


        var dbContext = new ProjectDbContext();

        objProject = new ObservableCollection<Project>();
        ProjectsCollectionView.ItemsSource = objProject;

        foreach (var project in dbContext.Projects) {

            if (project.id == user.identificacion)
            {
                var tasks = new ObservableCollection<string>();

                foreach (var task in dbContext.Tasks)
                {

                    if (task.projectId == project.id)
                    {
                        tasks.Add(task.nombre);
                    }

                }

                var oldProject = new Project
                {
                    Id = project.id,
                    ProjectName = project.nombre,
                    ProjectType = project.descripcion,
                    Tasks = tasks
                };

                objProject.Add(oldProject);

            }

        }
        

        MessagingCenter.Subscribe<CreateProject, Project>(this, "objData",(sender, newProject) =>
        {
            objProject.Add(newProject);
        });

        MessagingCenter.Subscribe<UpdateProject, Project>(this, "UpdatedProject", async (sender, updatedProject) =>
        {
            var index = objProject.IndexOf(objProject.FirstOrDefault(p => p.Id == updatedProject.Id));
            if (index != -1)
            {
                objProject[index] = updatedProject;
            }
        });


    }

    private async void OnAddProjectClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateProject());
    }

    private async void SwipeItem_Invoked_Delete(object sender, EventArgs e)
    {
        var swipeView = sender as  SwipeItem;
        var DeleteProject = swipeView?.BindingContext as Project;

        if (DeleteProject != null) {

            bool answer = await DisplayAlert("Confirmación", $"¿Está seguro de eliminar el proyecto '{DeleteProject.ProjectName}'?", "Sí", "No");
            if (answer) {
                objProject.Remove(DeleteProject);
                await DisplayAlert("Confirmacion", $"Proyecto eliminado", "OK");
            }
            
        }
        
    }

    private async void SwipeItem_Invoked_Update(object sender, EventArgs e) {
        var swipeView = sender as SwipeItem;
        var UpdateProject = swipeView?.BindingContext as Project;
        await Navigation.PushAsync(new UpdateProject(UpdateProject));
    }

    private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
    {

    }

    private void Volver_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private async void Task_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var taskName = button.CommandParameter as string;

        var dbContext = new ProjectDbContext();

        var task = dbContext.Tasks.FirstOrDefault(t => t.nombre == taskName);
        if (task != null)
        {
            await Navigation.PushAsync(new CreateTask(task));
        }
    }
}








