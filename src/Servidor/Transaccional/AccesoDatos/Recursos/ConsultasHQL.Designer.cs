﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Trascend.Bolet.AccesoDatos.Recursos {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ConsultasHQL {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ConsultasHQL() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Trascend.Bolet.AccesoDatos.Recursos.ConsultasHQL", typeof(ConsultasHQL).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select distinct(a) from Agente a left join fetch a.Poderes as poder where .
        /// </summary>
        public static string CabeceraObtenerAgente {
            get {
                return ResourceManager.GetString("CabeceraObtenerAgente", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select a from Asociado a where .
        /// </summary>
        public static string CabeceraObtenerAsociado {
            get {
                return ResourceManager.GetString("CabeceraObtenerAsociado", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select c from CambioDeDomicilio c left join fetch c.Marca as marca left join fetch c.InteresadoActual as interesadoActual left join fetch c.InteresadoAnterior as interesadoAnterior left join fetch c.Agente as agente  left join fetch c.Poder as poder where .
        /// </summary>
        public static string CabeceraObtenerCambioDeDomicilio {
            get {
                return ResourceManager.GetString("CabeceraObtenerCambioDeDomicilio", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select c from CambioDeNombre c left join fetch c.Marca as marca left join fetch c.InteresadoActual as interesadoActual left join fetch c.InteresadoAnterior as interesadoAnterior left join fetch c.Agente as agente left join fetch c.Poder as poder where .
        /// </summary>
        public static string CabeceraObtenerCambioDeNombre {
            get {
                return ResourceManager.GetString("CabeceraObtenerCambioDeNombre", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select c from CambioPeticionario c left join fetch c.Marca as marca left join fetch c.InteresadoAnterior as anterior left join fetch c.AgenteAnterior as agenteAnt left join fetch c.AgenteActual as agenteAct left join fetch c.PoderAnterior as PoderAnt  left join fetch c.PoderActual as PoderAct left join fetch c.InteresadoAnterior as interesadoAnt left join fetch c.InteresadoActual as interesadoAct left join fetch c.BoletinPublicacion as boletin where .
        /// </summary>
        public static string CabeceraObtenerCambioPeticionario {
            get {
                return ResourceManager.GetString("CabeceraObtenerCambioPeticionario", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select c from Carta c left join fetch c.Resumen as resumen left join fetch c.Asociado as asociado where .
        /// </summary>
        public static string CabeceraObtenerCarta {
            get {
                return ResourceManager.GetString("CabeceraObtenerCarta", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select c from CartaOut c where .
        /// </summary>
        public static string CabeceraObtenerCartaOut {
            get {
                return ResourceManager.GetString("CabeceraObtenerCartaOut", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select c from Cesion c left join fetch c.Marca as marca left join fetch c.Cedente as cedente left join fetch c.AgenteCedente as agenteCed left join fetch c.AgenteCesionario as agenteCes left join fetch c.PoderCedente as PoderCed  left join fetch c.PoderCesionario as PoderCes left join fetch c.Cedente as interesadoCed left join fetch c.Cesionario as interesadoCes left join fetch c.BoletinPublicacion as boletin where .
        /// </summary>
        public static string CabeceraObtenerCesion {
            get {
                return ResourceManager.GetString("CabeceraObtenerCesion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select f from Fusion f left join fetch f.Marca as marca left join fetch f.InteresadoEntre as interesadoEntre left join fetch f.InteresadoSobreviviente as interesadoSobreviviente left join fetch f.Agente as agente left join fetch f.Poder as poder where .
        /// </summary>
        public static string CabeceraObtenerFusion {
            get {
                return ResourceManager.GetString("CabeceraObtenerFusion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select i from Interesado i left outer join i.Pais as pais where .
        /// </summary>
        public static string CabeceraObtenerInteresado {
            get {
                return ResourceManager.GetString("CabeceraObtenerInteresado", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select l from Licencia l left join fetch l.Marca as marca left join fetch l.AgenteLicenciatario as Alicentario left join fetch l.AgenteLicenciante as Alice left join fetch l.Asociado as asosia left join fetch l.Boletin as boletin left join fetch l.InteresadoLicenciatario as InterLicen left join fetch l.InteresadoLicenciante left join fetch l.PoderLicenciatario as Plicentario left join fetch l.PoderLicenciante as Plicenciant Where .
        /// </summary>
        public static string CabeceraObtenerLicencia {
            get {
                return ResourceManager.GetString("CabeceraObtenerLicencia", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select m from Marca m left join fetch m.Asociado as asociado left join fetch m.Corresponsal as corresponsal left join fetch m.Interesado as interesado where .
        /// </summary>
        public static string CabeceraObtenerMarca {
            get {
                return ResourceManager.GetString("CabeceraObtenerMarca", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select p from Poder p left join fetch p.Boletin as boletin left join fetch p.Interesado as interesado where .
        /// </summary>
        public static string CabeceraObtenerPoder {
            get {
                return ResourceManager.GetString("CabeceraObtenerPoder", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to a.Id = &apos;{0}&apos;.
        /// </summary>
        public static string FiltroObtenerAgenteId {
            get {
                return ResourceManager.GetString("FiltroObtenerAgenteId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to upper(a.Nombre) like &apos;%{0}%&apos;.
        /// </summary>
        public static string FiltroObtenerAgenteNombre {
            get {
                return ResourceManager.GetString("FiltroObtenerAgenteNombre", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to a.Id = {0}.
        /// </summary>
        public static string FiltroObtenerAsociadoId {
            get {
                return ResourceManager.GetString("FiltroObtenerAsociadoId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to upper(a.Nombre) like &apos;%{0}%&apos;.
        /// </summary>
        public static string FiltroObtenerAsociadoNombre {
            get {
                return ResourceManager.GetString("FiltroObtenerAsociadoNombre", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to c.Id = {0}.
        /// </summary>
        public static string FiltroObtenerCambioDeDomicilioId {
            get {
                return ResourceManager.GetString("FiltroObtenerCambioDeDomicilioId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to marca.Id = {0}.
        /// </summary>
        public static string FiltroObtenerCambioDeDomicilioIdMarca {
            get {
                return ResourceManager.GetString("FiltroObtenerCambioDeDomicilioIdMarca", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to c.Id = {0}.
        /// </summary>
        public static string FiltroObtenerCambioDeNombreId {
            get {
                return ResourceManager.GetString("FiltroObtenerCambioDeNombreId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to marca.Id = {0}.
        /// </summary>
        public static string FiltroObtenerCambioDeNombreIdMarca {
            get {
                return ResourceManager.GetString("FiltroObtenerCambioDeNombreIdMarca", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to c.FechaCesion between &apos;{0}&apos; and &apos;{1}&apos;.
        /// </summary>
        public static string FiltroObtenerCambioPeticionarioFecha {
            get {
                return ResourceManager.GetString("FiltroObtenerCambioPeticionarioFecha", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to c.Id = {0}.
        /// </summary>
        public static string FiltroObtenerCambioPeticionarioId {
            get {
                return ResourceManager.GetString("FiltroObtenerCambioPeticionarioId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to marca.Id = {0}.
        /// </summary>
        public static string FiltroObtenerCambioPeticionarioIdMarca {
            get {
                return ResourceManager.GetString("FiltroObtenerCambioPeticionarioIdMarca", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to c.Fecha between &apos;{0}&apos; and &apos;{1}&apos;.
        /// </summary>
        public static string FiltroObtenerCartaFecha {
            get {
                return ResourceManager.GetString("FiltroObtenerCartaFecha", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to c.Id = {0}.
        /// </summary>
        public static string FiltroObtenerCartaId {
            get {
                return ResourceManager.GetString("FiltroObtenerCartaId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to asociado.Id = {0}.
        /// </summary>
        public static string FiltroObtenerCartaIdAsociado {
            get {
                return ResourceManager.GetString("FiltroObtenerCartaIdAsociado", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to c.FechaIngreso between &apos;{0}&apos; and &apos;{1}&apos;.
        /// </summary>
        public static string FiltroObtenerCartaOutFecha {
            get {
                return ResourceManager.GetString("FiltroObtenerCartaOutFecha", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to c.NRelacion like &apos;{0}%&apos;.
        /// </summary>
        public static string FiltroObtenerCartaOutId {
            get {
                return ResourceManager.GetString("FiltroObtenerCartaOutId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to c.Status = &apos;{0}&apos;.
        /// </summary>
        public static string FiltroObtenerCartaOutStatus {
            get {
                return ResourceManager.GetString("FiltroObtenerCartaOutStatus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to upper(resumen.Descripcion) like &apos;%{0}%&apos;.
        /// </summary>
        public static string FiltroObtenerCartaResumen {
            get {
                return ResourceManager.GetString("FiltroObtenerCartaResumen", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to c.FechaCesion between &apos;{0}&apos; and &apos;{1}&apos;.
        /// </summary>
        public static string FiltroObtenerCesionFecha {
            get {
                return ResourceManager.GetString("FiltroObtenerCesionFecha", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to c.Id = {0}.
        /// </summary>
        public static string FiltroObtenerCesionId {
            get {
                return ResourceManager.GetString("FiltroObtenerCesionId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to marca.Id = {0}.
        /// </summary>
        public static string FiltroObtenerCesionIdMarca {
            get {
                return ResourceManager.GetString("FiltroObtenerCesionIdMarca", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to f.Fecha between &apos;{0}&apos; and &apos;{1}&apos;.
        /// </summary>
        public static string FiltroObtenerFusionFecha {
            get {
                return ResourceManager.GetString("FiltroObtenerFusionFecha", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to f.Id = {0}.
        /// </summary>
        public static string FiltroObtenerFusionId {
            get {
                return ResourceManager.GetString("FiltroObtenerFusionId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to marca.Id = {0}.
        /// </summary>
        public static string FiltroObtenerFusionIdMarca {
            get {
                return ResourceManager.GetString("FiltroObtenerFusionIdMarca", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to i.Id = {0}.
        /// </summary>
        public static string FiltroObtenerInteresadoId {
            get {
                return ResourceManager.GetString("FiltroObtenerInteresadoId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to upper(i.Nombre) like &apos;%{0}%&apos;.
        /// </summary>
        public static string FiltroObtenerInteresadoNombre {
            get {
                return ResourceManager.GetString("FiltroObtenerInteresadoNombre", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to l.Fecha between &apos;{0}&apos; and &apos;{1}&apos;.
        /// </summary>
        public static string FiltroObtenerLicenciaFecha {
            get {
                return ResourceManager.GetString("FiltroObtenerLicenciaFecha", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to l.Id = {0}.
        /// </summary>
        public static string FiltroObtenerLicenciaId {
            get {
                return ResourceManager.GetString("FiltroObtenerLicenciaId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to marca.Id = {0}.
        /// </summary>
        public static string FiltroObtenerLicenciaIdMarca {
            get {
                return ResourceManager.GetString("FiltroObtenerLicenciaIdMarca", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to upper(m.Descripcion) like &apos;%{0}%&apos;.
        /// </summary>
        public static string FiltroObtenerMarcaDescripcion {
            get {
                return ResourceManager.GetString("FiltroObtenerMarcaDescripcion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to m.FechaPublicacion between &apos;{0}&apos; and &apos;{1}&apos;.
        /// </summary>
        public static string FiltroObtenerMarcaFecha {
            get {
                return ResourceManager.GetString("FiltroObtenerMarcaFecha", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to upper(m.Fichas) like &apos;%{0}%&apos;.
        /// </summary>
        public static string FiltroObtenerMarcaFichas {
            get {
                return ResourceManager.GetString("FiltroObtenerMarcaFichas", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to m.Id = {0}.
        /// </summary>
        public static string FiltroObtenerMarcaId {
            get {
                return ResourceManager.GetString("FiltroObtenerMarcaId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to asociado.Id = {0}.
        /// </summary>
        public static string FiltroObtenerMarcaIdAsociado {
            get {
                return ResourceManager.GetString("FiltroObtenerMarcaIdAsociado", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to interesado.Id = {0}.
        /// </summary>
        public static string FiltroObtenerMarcaIdInteresado {
            get {
                return ResourceManager.GetString("FiltroObtenerMarcaIdInteresado", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to p.Fecha between &apos;{0}&apos; and &apos;{1}&apos;.
        /// </summary>
        public static string FiltroObtenerPoderFecha {
            get {
                return ResourceManager.GetString("FiltroObtenerPoderFecha", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to p.Id = {0}.
        /// </summary>
        public static string FiltroObtenerPoderId {
            get {
                return ResourceManager.GetString("FiltroObtenerPoderId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select p from Poder p left join fetch p.Agentes where p.Id = &apos;{0}&apos;.
        /// </summary>
        public static string ObtenerAgentesDeUnPoder {
            get {
                return ResourceManager.GetString("ObtenerAgentesDeUnPoder", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select distinct(a) from Agente a left join fetch a.Poderes order by a.Id.
        /// </summary>
        public static string ObtenerAgentesYPoderes {
            get {
                return ResourceManager.GetString("ObtenerAgentesYPoderes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select a from Asignacion a where a.Carta = &apos;{0}&apos;.
        /// </summary>
        public static string ObtenerAsignacionesPorCarta {
            get {
                return ResourceManager.GetString("ObtenerAsignacionesPorCarta", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select distinct(a) from Asociado a left outer join fetch a.Justificaciones where a.Id = &apos;{0}&apos; order by a.Id.
        /// </summary>
        public static string ObtenerAsociadoConTodo {
            get {
                return ResourceManager.GetString("ObtenerAsociadoConTodo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select a from Auditoria a where a.Fk = &apos;{0}&apos; and a.Tabla = &apos;{1}&apos;.
        /// </summary>
        public static string ObtenerAuditoriaPorFKYTabla {
            get {
                return ResourceManager.GetString("ObtenerAuditoriaPorFKYTabla", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select b from Busqueda b where b.Marca.Id = &apos;{0}&apos;.
        /// </summary>
        public static string ObtenerBusquedasPorMarca {
            get {
                return ResourceManager.GetString("ObtenerBusquedasPorMarca", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select c from Contacto c where c.Asociado.id = &apos;{0}&apos;.
        /// </summary>
        public static string ObtenerContactosPorAsociado {
            get {
                return ResourceManager.GetString("ObtenerContactosPorAsociado", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select d from DatosTransferencia d where d.Asociado.id = &apos;{0}&apos;.
        /// </summary>
        public static string ObtenerDatosTransferenciaPorAsociado {
            get {
                return ResourceManager.GetString("ObtenerDatosTransferenciaPorAsociado", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select i from InfoAdicinal i where i.Id = &apos;{0}&apos;.
        /// </summary>
        public static string ObtenerInfoAdicinalPorId {
            get {
                return ResourceManager.GetString("ObtenerInfoAdicinalPorId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select i from InfoBol i left outer join fetch i.TipoInfobol where i.Marca.Id = &apos;{0}&apos;.
        /// </summary>
        public static string ObtenerInfoBolesPorMarcas {
            get {
                return ResourceManager.GetString("ObtenerInfoBolesPorMarcas", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select distinct(i) from Interesado i left join fetch i.Pais left join fetch i.Nacionalidad where i.Id = &apos;{0}&apos; order by i.Id.
        /// </summary>
        public static string ObtenerInteresadoConTodo {
            get {
                return ResourceManager.GetString("ObtenerInteresadoConTodo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select p from Poder p left join fetch p.Interesado where p.Id =&apos;{0}&apos;.
        /// </summary>
        public static string ObtenerInteresadosDeUnPoder {
            get {
                return ResourceManager.GetString("ObtenerInteresadosDeUnPoder", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select l from ListaDatosDominio l where l.Filtro = &apos;{0}&apos;.
        /// </summary>
        public static string ObtenerListaDatosDominioPorParametro {
            get {
                return ResourceManager.GetString("ObtenerListaDatosDominioPorParametro", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select distinct(l) from ListaDatosValores l where l.Id = &apos;{0}&apos;.
        /// </summary>
        public static string ObtenerListaDatosValoresPorParametro {
            get {
                return ResourceManager.GetString("ObtenerListaDatosValoresPorParametro", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select m from Marca m left join fetch m.Asociado as asociado left join fetch m.Corresponsal as corresponsal where m.Id = &apos;{0}&apos; order by m.Id.
        /// </summary>
        public static string ObtenerMarcaConTodo {
            get {
                return ResourceManager.GetString("ObtenerMarcaConTodo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select o from Operacion o where o.CodigoAplicada = {0} and o.Aplicada = &apos;M&apos;.
        /// </summary>
        public static string ObtenerOperacionesPorMarcas {
            get {
                return ResourceManager.GetString("ObtenerOperacionesPorMarcas", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select o from Operacion o where o.CodigoAplicada = {0} and o.Aplicada = &apos;M&apos; and o.Servicio.Id = &apos;{1}&apos;.
        /// </summary>
        public static string ObtenerOperacionesPorMarcasYServicio {
            get {
                return ResourceManager.GetString("ObtenerOperacionesPorMarcasYServicio", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select a from Agente a left join fetch a.Poderes as poder where a.Id = &apos;{0}&apos;.
        /// </summary>
        public static string ObtenerPoderesPorAgente {
            get {
                return ResourceManager.GetString("ObtenerPoderesPorAgente", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select p from Poder p where p.Interesado.Id = &apos;{0}&apos;.
        /// </summary>
        public static string ObtenerPoderesPorInteresado {
            get {
                return ResourceManager.GetString("ObtenerPoderesPorInteresado", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select distinct(r) from Rol r left join fetch r.Objetos order by r.Id.
        /// </summary>
        public static string ObtenerRolesYObjetos {
            get {
                return ResourceManager.GetString("ObtenerRolesYObjetos", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select u from Usuario u left join fetch u.Rol as rol left join fetch rol.Objetos where u.Id = &apos;{0}&apos; and u.Password = &apos;{1}&apos;.
        /// </summary>
        public static string ObtenerUsuarioPorIdYPassword {
            get {
                return ResourceManager.GetString("ObtenerUsuarioPorIdYPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select u from Usuario u where u.Iniciales=&apos;{0}&apos;.
        /// </summary>
        public static string ObtenerUsuarioPorIniciales {
            get {
                return ResourceManager.GetString("ObtenerUsuarioPorIniciales", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select r from Resolucion r where r.Id = {0} and r.FechaResolucion = &apos;{1}&apos; and r.Boletin.Id =  {2}.
        /// </summary>
        public static string VerificarExistenciaResolucion {
            get {
                return ResourceManager.GetString("VerificarExistenciaResolucion", resourceCulture);
            }
        }
    }
}
