using System.Collections.Generic;

namespace co_weelo_testproject_common.Response
{
    public class RegisterResponse
    {
        /// <summary>
        /// Estado de la solicitud
        /// </summary>
        public bool Successful { get; set; }
        /// <summary>
        /// Codigo del resultado de la solicitud
        /// </summary>
        public string ResultCode { get; set; }
        /// <summary>
        /// Mensaje de advertencia o error de la solicitud
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Lista de mensajes que pueden contener errores o advertencias
        /// </summary>
        public List<string> MessageList { get; set; }
    }
}
