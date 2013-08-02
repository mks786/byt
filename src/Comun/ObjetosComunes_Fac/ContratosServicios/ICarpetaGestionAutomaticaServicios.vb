Imports System.Collections.Generic
Imports Diginsoft.Bolet.ObjetosComunes.Entidades


Namespace ContratosServicios
    Public Interface ICarpetaGestionAutomaticaServicios
        Inherits IServicioBase(Of CarpetaGestionAutomatica)

        Function ObtenerCarpetasPorIniciales(ByVal Usuario As Trascend.Bolet.ObjetosComunes.Entidades.Usuario) As IList(Of CarpetaGestionAutomatica)

    End Interface
End Namespace



