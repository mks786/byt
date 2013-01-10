Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacFacturaTotal
        Inherits IDaoBase(Of FacFacturaTotal, Integer)
        Function ObtenerFacFacturaTotalsFiltro(ByVal FacFacturaTotal As FacFacturaTotal) As IList(Of FacFacturaTotal)
    End Interface
End Namespace