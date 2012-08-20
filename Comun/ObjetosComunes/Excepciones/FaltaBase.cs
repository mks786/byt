using System.Runtime.Serialization;

namespace Trascend.Bolet.ObjetosComunes.Excepciones.Faltas
{
    [DataContract]
    public class FaltaBase
    {
        [DataMember]
        public string mensaje { get; set; }

        [DataMember]
        public string excepcionInterna { get; set; }

        [DataMember]
        public string stackTrace { get; set; }
    }
}
