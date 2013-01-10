Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosDocumentosTraduccion
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosDocumentosTraduccion
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="DocumentosTraduccion">DocumentosTraduccion a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal DocumentosTraduccion As DocumentosTraduccion) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarDocumentosTraduccion(DocumentosTraduccion)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los DocumentosTraducciones
        ''' </summary>
        ''' <returns>El Comando para consultar todos los DocumentosTraducciones</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of DocumentosTraduccion))
            Return New ComandoConsultarTodosDocumentosTraducciones()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un DocumentosTraduccion
        ' ''' </summary>
        ' ''' <param name="usuario">DocumentosTraduccion que se va a DocumentosTraduccion</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarDocumentosTraduccion(ByVal DocumentosTraduccion As DocumentosTraduccion) As ComandoBase(Of Boolean)
            Return New ComandoEliminarDocumentosTraduccion(DocumentosTraduccion)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un DocumentosTraduccion por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal DocumentosTraduccion As DocumentosTraduccion) As ComandoBase(Of DocumentosTraduccion)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="DocumentosTraduccion">DocumentosTraduccion a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaDocumentosTraduccion(ByVal DocumentosTraduccion As DocumentosTraduccion) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaDocumentosTraduccion(DocumentosTraduccion)
        End Function
    End Class
End Namespace