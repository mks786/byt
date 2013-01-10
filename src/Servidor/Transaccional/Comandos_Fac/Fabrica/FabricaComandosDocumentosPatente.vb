Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosDocumentosPatente
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosDocumentosPatente
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="DocumentosPatente">DocumentosPatente a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal DocumentosPatente As DocumentosPatente) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarDocumentosPatente(DocumentosPatente)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los DocumentosPatentees
        ''' </summary>
        ''' <returns>El Comando para consultar todos los DocumentosPatentees</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of DocumentosPatente))
            Return New ComandoConsultarTodosDocumentosPatentees()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un DocumentosPatente
        ' ''' </summary>
        ' ''' <param name="usuario">DocumentosPatente que se va a DocumentosPatente</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarDocumentosPatente(ByVal DocumentosPatente As DocumentosPatente) As ComandoBase(Of Boolean)
            Return New ComandoEliminarDocumentosPatente(DocumentosPatente)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un DocumentosPatente por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal DocumentosPatente As DocumentosPatente) As ComandoBase(Of DocumentosPatente)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="DocumentosPatente">DocumentosPatente a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaDocumentosPatente(ByVal DocumentosPatente As DocumentosPatente) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaDocumentosPatente(DocumentosPatente)
        End Function
    End Class
End Namespace