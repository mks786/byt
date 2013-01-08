Imports System.Collections.Generic
Imports Diginsoft.Bolet.ObjetosComunes.Entidades

Namespace ContratosServicios
    Public Interface ITipoPatenteServicios
        Inherits IServicioBase(Of TipoPatente)
        Function ObtenerTipoPatentesFiltro(ByVal TipoPatente As TipoPatente) As IList(Of TipoPatente)
    End Interface
End Namespace