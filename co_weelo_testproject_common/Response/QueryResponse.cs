using System;
using System.Runtime.Serialization;

namespace co_weelo_testproject_common.Response
{
    [Serializable]
    [DataContract]
    public class QueryResponse<T>
    {
        /// <summary>
        /// True: indica que la operación se ejecutó exitósamene.
        /// </summary>
        [DataMember]
        public bool Successful { get; set; }

        /// <summary>
        /// Código de fallo en caso de presentarse un error.
        /// </summary>
        [DataMember]
        public string ResultCode { get; set; }

        /// <summary>
        /// Detalle del error que pueda presentarse.
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// Entidad compuesta con información 
        /// </summary>
        [DataMember]
        public T ResultObject { get; set; }
    }
}
