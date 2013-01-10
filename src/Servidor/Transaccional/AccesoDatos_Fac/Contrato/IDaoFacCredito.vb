Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacCredito
        Inherits IDaoBase(Of FacCredito, Integer)
        Function ObtenerFacCreditosFiltro(ByVal FacCredito As FacCredito) As IList(Of FacCredito)
    End Interface
End Namespace