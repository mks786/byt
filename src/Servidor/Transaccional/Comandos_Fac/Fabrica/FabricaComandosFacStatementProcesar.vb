Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacStatementProcesar
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacStatementProcesar
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacStatementProcesar">FacStatementProcesar a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacStatementProcesar As FacStatementProcesar) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacStatementProcesares
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacStatementProcesares</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacStatementProcesar))
            Return New ComandoConsultarTodosFacStatementProcesars()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacStatementProcesar
        '' ''' </summary>
        '' ''' <param name="usuario">FacStatementProcesar que se va a FacStatementProcesar</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacStatementProcesar(ByVal FacStatementProcesar As FacStatementProcesar) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacStatementProcesar por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacStatementProcesar As FacStatementProcesar) As ComandoBase(Of FacStatementProcesar)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacStatementProcesar">FacStatementProcesar a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacStatementProcesar(ByVal FacStatementProcesar As FacStatementProcesar) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        Public Shared Function ObtenerComandoConsultarFacStatementProcesarsFiltro(ByVal FacStatementProcesar As FacStatementProcesar) As ComandoBase(Of IList(Of FacStatementProcesar))
            Return New ComandoConsultarFacStatementProcesarsFiltro(FacStatementProcesar)
        End Function

    End Class
End Namespace