Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacVistaFacturaServicio
        Inherits IDaoBase(Of FacVistaFacturaServicio, Integer)
        Function ObtenerFacVistaFacturaServiciosFiltro(ByVal FacVistaFacturaServicio As FacVistaFacturaServicio) As IList(Of FacVistaFacturaServicio)
    End Interface
End Namespace