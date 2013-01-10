Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacOperacionProforma
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacOperacionProforma
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacOperacionProforma">FacOperacionProforma a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacOperacionProforma As FacOperacionProforma) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacOperacionProforma(FacOperacionProforma)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacOperacionProformaes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacOperacionProformaes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacOperacionProforma))
            Return New ComandoConsultarTodosFacOperacionProformas()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacOperacionProforma
        '' ''' </summary>
        '' ''' <param name="usuario">FacOperacionProforma que se va a FacOperacionProforma</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacOperacionProforma(ByVal FacOperacionProforma As FacOperacionProforma) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacOperacionProforma(FacOperacionProforma)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacOperacionProforma por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacOperacionProforma As FacOperacionProforma) As ComandoBase(Of FacOperacionProforma)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacOperacionProforma">FacOperacionProforma a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacOperacionProforma(ByVal FacOperacionProforma As FacOperacionProforma) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacOperacionProforma(FacOperacionProforma)
        End Function

        Public Shared Function ObtenerComandoConsultarFacOperacionProformasFiltro(ByVal FacOperacionProforma As FacOperacionProforma) As ComandoBase(Of IList(Of FacOperacionProforma))
            Return New ComandoConsultarFacOperacionProformasFiltro(FacOperacionProforma)
        End Function

    End Class
End Namespace