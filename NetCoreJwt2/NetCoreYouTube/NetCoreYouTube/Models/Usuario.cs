using System.Reflection.Metadata.Ecma335;

namespace NetCoreYouTube.Models
{
    public class Usuario
    {

        // El el modelo de Usuario, se garantiza el perfil de casda usuario
        // Mateo es cliente, Pedro es empleado , Franco Administrador.
        public string idUsuario { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public string rol { get; set; }

        public static List<Usuario> DB()
        {
            var lista = new List<Usuario>()
            {
                new Usuario
                {
                    idUsuario = "1",
                    usuario = "Mateo",
                    password = "1234",
                    rol = "cliente",
                },

                new Usuario
                {
                    idUsuario = "2",
                    usuario = "Pedro",
                    password = "1234",
                    rol = "empleado",
                },

                new Usuario
                {
                    idUsuario = "3",
                    usuario = "Franco",
                    password = "1234",
                    rol = "administrador",
                },

                new Usuario
                {
                    idUsuario = "4",
                    usuario = "German",
                    password = "1234",
                    rol = "asesor",
                },
                
            };
            return lista;
        }
       
    }
}
