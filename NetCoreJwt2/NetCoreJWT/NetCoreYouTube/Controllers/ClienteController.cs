using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreYouTube.Models;
using System.Security.Claims;

namespace NetCoreYouTube.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class ClienteController : ControllerBase
    {

        /// <summary>
        ///
        /// En clienteController se verifica si sos administrador para GUARDAR USUARIOS, PARA ELIMINARLOS 
        /// Y SI SE REQUIERE BUSCAR POR ID ALGUN USUARIO
        /// 
        /// </summary>
        /// <returns></returns>


        [HttpPost]
        [Route("guardar")]
        [Authorize]
        public dynamic guardarCliente(Cliente cliente)
        {

            //Guardar en la db y le asignas un id
            cliente.id = "3";
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var tokenvalidate = Jwt.validarToken(identity);

            if (tokenvalidate.success == false) return tokenvalidate;

            Usuario usuario = tokenvalidate.result;

            if (usuario.rol == "administrador")
            {
                return new
                {
                    success = true,
                    message = "cliente registrado",
                    result = cliente
                };
            }
            else
            {
                return new
                {
                    seccess = false,
                    message = "no tienes permisos para guardar cliente",
                    result = ""
                };

            }
        }

        [HttpPost]
        [Route("eliminar")]
        [Authorize]
        public dynamic eliminarCliente(Cliente cliente)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var tokenvalidate = Jwt.validarToken(identity);

            if (tokenvalidate.success == false) return tokenvalidate;

            Usuario usuario = tokenvalidate.result;

            if (usuario.rol != "administrador")
            {
                return new
                {
                    seccess = false,
                    message = "no tienes permisos para eliminar cliente",
                    result = ""
                };
            }

            return new
            {
                success = true,
                message = "cliente eliminado",
                result = cliente
            };
        }




        [HttpGet]
        [Route("listar")]
        public dynamic listarCliente()
        {
            //Todo el codigo

            List<Cliente> clientes = new List<Cliente>
            {
                new Cliente
                {
                    id = "1",
                    correo = "google@gmail.com",
                    edad = "26",
                    nombre = "Franco Rivero Nicolas"
                },
                new Cliente
                {
                    id = "2",
                    correo = "miguelgoogle@gmail.com",
                    edad = "23",
                    nombre = "Miguel Mantilla"
                }
            };

            return clientes;
        }

        [HttpGet]
        [Route("listarxid")]
        public dynamic listarClientexid(int codigo)
        {
            //obtienes el cliente de la db

            return new Cliente
            {
                id = codigo.ToString(),
                correo = "google@gmail.com",
                edad = "26",
                nombre = "Franco Rivero Nicolas"
            };
        }
    }
}
