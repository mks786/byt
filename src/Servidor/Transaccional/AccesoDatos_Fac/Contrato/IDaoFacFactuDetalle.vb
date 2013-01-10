Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacFactuDetalle
        Inherits IDaoBase(Of FacFactuDetalle, Integer)
        Function ObtenerFacFactuDetallesFiltro(ByVal FacFactuDetalle As FacFactuDetalle) As IList(Of FacFactuDetalle)
    End Interface
End Namespace