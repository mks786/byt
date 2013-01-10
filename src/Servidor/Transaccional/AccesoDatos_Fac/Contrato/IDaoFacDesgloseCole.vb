Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacDesgloseCole
        Inherits IDaoBase(Of FacDesgloseCole, Char)
        Function ObtenerFacDesgloseColesFiltro(ByVal FacDesgloseCole As FacDesgloseCole) As IList(Of FacDesgloseCole)
    End Interface
End Namespace