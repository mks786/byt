Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacFacturaPendiente
        Inherits IDaoBase(Of FacFacturaPendiente, Integer)
        Function ObtenerFacFacturaPendientesFiltro(ByVal FacFacturaPendiente As FacFacturaPendiente) As IList(Of FacFacturaPendiente)
    End Interface
End Namespace