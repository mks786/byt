Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoTasa
        Inherits IDaoBase(Of Tasa, DateTime)
        Function ObtenerTasasFiltro(ByVal Tasa As Tasa) As IList(Of Tasa)
    End Interface
End Namespace