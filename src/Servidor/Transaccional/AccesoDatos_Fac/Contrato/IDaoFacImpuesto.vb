Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacImpuesto
        Inherits IDaoBase(Of FacImpuesto, DateTime)
        Function ObtenerFacImpuestosFiltro(ByVal FacImpuesto As FacImpuesto) As IList(Of FacImpuesto)
    End Interface
End Namespace