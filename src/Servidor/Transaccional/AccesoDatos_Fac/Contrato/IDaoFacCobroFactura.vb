Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacCobroFactura
        Inherits IDaoBase(Of FacCobroFactura, Integer)
        Function ObtenerFacCobroFacturasFiltro(ByVal FacCobroFactura As FacCobroFactura) As IList(Of FacCobroFactura)
    End Interface
End Namespace