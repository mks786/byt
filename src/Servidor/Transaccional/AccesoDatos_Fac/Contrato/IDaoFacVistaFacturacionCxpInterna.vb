Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacVistaFacturacionCxpInterna
        Inherits IDaoBase(Of FacVistaFacturacionCxpInterna, Integer)
        Function ObtenerFacVistaFacturacionCxpInternasFiltro(ByVal FacVistaFacturacionCxpInterna As FacVistaFacturacionCxpInterna) As IList(Of FacVistaFacturacionCxpInterna)
    End Interface
End Namespace