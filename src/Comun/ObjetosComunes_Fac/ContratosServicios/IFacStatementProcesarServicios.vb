Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacStatementProcesarServicios
        Inherits IServicioBase(Of FacStatementProcesar)
        Function ObtenerFacStatementProcesarsFiltro(ByVal FacStatementProcesar As FacStatementProcesar) As IList(Of FacStatementProcesar)
    End Interface
End Namespace