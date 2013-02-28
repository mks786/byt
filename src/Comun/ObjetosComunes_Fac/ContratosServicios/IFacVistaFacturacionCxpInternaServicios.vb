Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacVistaFacturacionCxpInternaServicios
        Inherits IServicioBase(Of FacVistaFacturacionCxpInterna)
        Function ObtenerFacVistaFacturacionCxpInternasFiltro(ByVal FacVistaFacturacionCxpInterna As FacVistaFacturacionCxpInterna) As IList(Of FacVistaFacturacionCxpInterna)
    End Interface
End Namespace