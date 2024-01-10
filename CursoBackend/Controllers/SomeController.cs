using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CursoBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {
        [HttpGet("sync")]
        public IActionResult GetSync()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();

            Thread.Sleep(1000);
            Console.WriteLine("CONEXION A BASE DE DATOS");

            Thread.Sleep(1000);
            Console.WriteLine("ENVIO DE EMAIL");

            Console.WriteLine("TODOA HA TERMINADO");

            stopwatch.Stop();


            return Ok(stopwatch.Elapsed);
        }

        [HttpGet("async")]
        public async Task<IActionResult> GetAsync()
        {
            var task1 = new Task(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Conexion a base de datos terminada");
            });

            task1.Start();
            Console.WriteLine("Hace otro cosa");

            await task1;
            Console.WriteLine("Toda ha terminado");

            return Ok();
        }
    }
}
