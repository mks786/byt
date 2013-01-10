Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacOperacionDetaProforma
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacOperacionDetaProforma
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacOperacionDetaProforma">FacOperacionDetaProforma a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacOperacionDetaProforma As FacOperacionDetaProforma) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacOperacionDetaProforma(FacOperacionDetaProforma)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacOperacionDetaProformaes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacOperacionDetaProformaes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacOperacionDetaProforma))
            Return New ComandoConsultarTodosFacOperacionDetaProformas()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacOperacionDetaProforma
        '' ''' </summary>
        '' ''' <param name="usuario">FacOperacionDetaProforma que se va a FacOperacionDetaProforma</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacOperacionDetaProforma(ByVal FacOperacionDetaProforma As FacOperacionDetaProforma) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacOperacionDetaProforma(FacOperacionDetaProforma)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacOperacionDetaProforma por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacOperacionDetaProforma As FacOperacionDetaProforma) As ComandoBase(Of FacOperacionDetaProforma)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacOperacionDetaProforma">FacOperacionDetaProforma a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacOperacionDetaProforma(ByVal FacOperacionDetaProforma As FacOperacionDetaProforma) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacOperacionDetaProforma(FacOperacionDetaProforma)
        End Function

        Public Shared Function ObtenerComandoConsultarFacOperacionDetaProformasFiltro(ByVal FacOperacionDetaProforma As FacOperacionDetaProforma) As ComandoBase(Of IList(Of FacOperacionDetaProforma))
            Return New ComandoConsultarFacOperacionDetaProformasFiltro(FacOperacionDetaProforma)
        End Function

    End Class
End Namespace