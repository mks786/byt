Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacStatementProcesar
        Inherits IDaoBase(Of FacStatementProcesar, Integer)
        Function ObtenerFacStatementProcesarsFiltro(ByVal FacStatementProcesar As FacStatementProcesar) As IList(Of FacStatementProcesar)
    End Interface
End Namespace