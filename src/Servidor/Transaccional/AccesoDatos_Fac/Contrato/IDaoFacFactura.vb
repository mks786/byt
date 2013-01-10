Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacFactura
        Inherits IDaoBase(Of FacFactura, Integer)
        Function ObtenerFacFacturasFiltro(ByVal FacFactura As FacFactura) As IList(Of FacFactura)
    End Interface
End Namespace