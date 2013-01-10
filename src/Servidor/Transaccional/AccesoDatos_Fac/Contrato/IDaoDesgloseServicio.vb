Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoDesgloseServicio
        Inherits IDaoBase(Of FacDesgloseServicio, Char)
        Function ObtenerFacDesgloseServiciosFiltro(ByVal FacDesgloseServicio As FacDesgloseServicio) As IList(Of FacDesgloseServicio)
    End Interface
End Namespace