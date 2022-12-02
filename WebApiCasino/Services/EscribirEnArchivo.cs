using Microsoft.AspNetCore.Mvc;
using WebApiCasino.Controllers;
using WebApiCasino.Entidades;

namespace WebApiCasino.Services
{
    public class EscribirEnArchivo : IHostedService
    {
        private readonly IWebHostEnvironment env;

        private readonly string nombreArchivo = "Logger-Information.txt";
        // private readonly string archivo = "ListadoAlumnos.txt";
        private Timer timer;

        public EscribirEnArchivo(IWebHostEnvironment env)
        {
            this.env = env;

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //Se ejecuta cuando cargamos la aplicacion 1 vez
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(20));
            Escribir("Proceso iniciado");
           
            return Task.CompletedTask;
        }

        //no funciona el proyecto finalizado
        public Task StopAsync(CancellationToken cancellationToken)
        {

            timer.Dispose();
            Escribir("Proceso Finalizado");

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            Escribir("Proceso en ejecucion: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));

        }
        private void Escribir(string msg)
        {
            var ruta = $@"{env.ContentRootPath}\wwwroot\{nombreArchivo}";

            using (StreamWriter writer = new StreamWriter(ruta, append: true)) { writer.WriteLine(msg); }
        }

        private void GuardarAlumnos()
        {

        }
    }
}