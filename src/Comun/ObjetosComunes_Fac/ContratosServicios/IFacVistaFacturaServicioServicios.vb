Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacVistaFacturaServicioServicios
        Inherits IServicioBase(Of FacVistaFacturaServicio)
        Function ObtenerFacVistaFacturaServiciosFiltro(ByVal FacVistaFacturaServicio As FacVistaFacturaServicio) As IList(Of FacVistaFacturaServicio)
    End Interface
End Namespace