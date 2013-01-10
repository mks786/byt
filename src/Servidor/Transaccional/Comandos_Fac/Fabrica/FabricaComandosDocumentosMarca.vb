Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosDocumentosMarca
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosDocumentosMarca
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="DocumentosMarca">DocumentosMarca a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal DocumentosMarca As DocumentosMarca) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarDocumentosMarca(DocumentosMarca)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los DocumentosMarcaes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los DocumentosMarcaes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of DocumentosMarca))
            Return New ComandoConsultarTodosDocumentosMarcas()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un DocumentosMarca
        ' ''' </summary>
        ' ''' <param name="usuario">DocumentosMarca que se va a DocumentosMarca</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarDocumentosMarca(ByVal DocumentosMarca As DocumentosMarca) As ComandoBase(Of Boolean)
            Return New ComandoEliminarDocumentosMarca(DocumentosMarca)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un DocumentosMarca por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal DocumentosMarca As DocumentosMarca) As ComandoBase(Of DocumentosMarca)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="DocumentosMarca">DocumentosMarca a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaDocumentosMarca(ByVal DocumentosMarca As DocumentosMarca) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaDocumentosMarca(DocumentosMarca)
        End Function
    End Class
End Namespace