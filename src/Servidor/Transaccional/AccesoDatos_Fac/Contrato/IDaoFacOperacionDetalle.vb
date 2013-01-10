Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacOperacionDetalle
        Inherits IDaoBase(Of FacOperacionDetalle, Integer)
        Function ObtenerFacOperacionDetallesFiltro(ByVal FacOperacionDetalle As FacOperacionDetalle) As IList(Of FacOperacionDetalle)
    End Interface
End Namespace