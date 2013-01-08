Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacAnuladaFisicaServicios
        Inherits IServicioBase(Of FacAnuladaFisica)
        Function ObtenerFacAnuladaFisicasFiltro(ByVal FacAnuladaFisica As FacAnuladaFisica) As IList(Of FacAnuladaFisica)
    End Interface
End Namespace