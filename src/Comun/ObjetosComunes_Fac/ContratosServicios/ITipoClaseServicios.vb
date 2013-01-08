Imports System.Collections.Generic
Imports Diginsoft.Bolet.ObjetosComunes.Entidades

Namespace ContratosServicios
    Public Interface ITipoClaseServicios
        Inherits IServicioBase(Of TipoClase)
        Function ObtenerTipoClasesFiltro(ByVal TipoClase As TipoClase) As IList(Of TipoClase)
    End Interface
End Namespace