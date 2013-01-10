Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacFacturaTotalZ
        Inherits IDaoBase(Of FacFacturaTotalZ, Integer)
        Function ObtenerFacFacturaTotalZsFiltro(ByVal FacFacturaTotalZ As FacFacturaTotalZ) As IList(Of FacFacturaTotalZ)
    End Interface
End Namespace