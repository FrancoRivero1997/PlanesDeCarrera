using System.Security.Claims;

namespace NetCoreYouTube.Models
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }

        public static dynamic validarToken(ClaimsIdentity identity)
        {
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    return new
                    {
                        success = false,
                        message= "Verificar si estas enviando un token validado",
                        result = ""
                    };
                }

                var id = identity.Claims.FirstOrDefault(a => a.Type == "id").Value;
                Usuario usuario = Usuario.DB().FirstOrDefault(o => o.idUsuario == id);
                return new
                {
                    success = true,
                    message = "Exito",
                    result = usuario
                };
            }
            catch (Exception error)
            {
                return new
                {
                    success = false,
                    message = "Error: " + error,
                    result = ""
                };
           
            }
        }
    }
}
