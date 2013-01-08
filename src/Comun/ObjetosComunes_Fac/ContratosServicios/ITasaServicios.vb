Imports System.Collections.Generic
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface ITasaServicios
        Inherits IServicioBase(Of Tasa)
        Function ObtenerTasasFiltro(ByVal Tasa As Tasa) As IList(Of Tasa)
    End Interface
End Namespace