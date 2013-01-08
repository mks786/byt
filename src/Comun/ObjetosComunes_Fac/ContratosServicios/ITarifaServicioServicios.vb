Imports System.Collections.Generic
Imports Diginsoft.Bolet.ObjetosComunes.Entidades

Namespace ContratosServicios
    Public Interface ITarifaServicioServicios
        Inherits IServicioBase(Of TarifaServicio)
        Function ObtenerTarifaServiciosFiltro(ByVal TarifaServicio As TarifaServicio) As IList(Of TarifaServicio)
    End Interface
End Namespace