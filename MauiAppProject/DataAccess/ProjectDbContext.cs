using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.ApplicationModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppProject.DataAccess
{
    class ProjectDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<usuario> Usuarios { get; set; }

        //public DbSet<ProUser> ProyectoUsuario { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Projectum");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { 
            modelBuilder.Entity<Task>().HasData(
                    new Task(1, "Dieño de Interfaz", "Jaime", 1),
                    new Task(2, "Dfinir catalogo", "Pedro",  1),
                    new Task(3, "Definir proveedores", "Camila", 1),
                    new Task(4, "Dieño de Interfaz", "Jaime", 2),
                    new Task(5, "Dfinir catalogo", "Pedro", 2),
                    new Task(6, "Definir proveedores", "Camila", 2)
            );

            modelBuilder.Entity<usuario>()
                .HasKey(pu => new { pu.identificacion });

            modelBuilder.Entity<usuario>().HasData(
                    new usuario(1, "Juan", "Garcia", "jg@gmail.com", "Juan12345", "3124567645"),
                    new usuario(2, "Pablo", "Mina", "pm@hotmail.com", "Pablo12345", "3236572346"),
                    new usuario(3, "Maria", "Ortega", "mo@icloud.com", "Maria12345", "3114562456")
                );

            modelBuilder.Entity<Project>().HasData(
                
                    new Project(1, "Projectum", "Aplicacion para administracion de proyectos"),
                    new Project(2, "Calltech", "Aplicacion de gestion para contact center")

                );


            //modelBuilder.Entity<ProUser>().HasData(
            //        new ProUser(1, 1),
            //        new ProUser(2, 2)

            //    );
        }

        

    }
    public record Project(int id, string nombre, string descripcion)
    {
        internal ObservableCollection<Task> Tasks;
    }

    public record Task(int id, string nombre, string assignTo, int projectId) {
        
        public Project Project { get; set; }
    };


    public record usuario(int identificacion, string nombre, string apellido, string correo, string contraseña, string telefono)
    {

        internal ObservableCollection<Project> Projects { get; set; }
    };

    //public record ProUser(int identificacion, int projectId) {

    //    internal ObservableCollection<Project> Projects { get; set; }
    //    internal ObservableCollection<usuario> Usuarios { get; set; }
    //}


}
