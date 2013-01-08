Imports System.Collections.Generic
Imports Diginsoft.Bolet.ObjetosComunes.Entidades

Namespace ContratosServicios
    Public Interface ITipoMarcaServicios
        Inherits IServicioBase(Of TipoMarca)
        Function ObtenerTipoMarcasFiltro(ByVal TipoMarca As TipoMarca) As IList(Of TipoMarca)
    End Interface
End Namespace