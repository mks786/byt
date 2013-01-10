Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosChequeRecido
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosChequeRecido
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="ChequeRecido">ChequeRecido a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal ChequeRecido As ChequeRecido) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarChequeRecido(ChequeRecido)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los ChequeRecidoes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los ChequeRecidoes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of ChequeRecido))
            Return New ComandoConsultarTodosChequeRecidos()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un ChequeRecido
        '' ''' </summary>
        '' ''' <param name="usuario">ChequeRecido que se va a ChequeRecido</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarChequeRecido(ByVal ChequeRecido As ChequeRecido) As ComandoBase(Of Boolean)
            Return New ComandoEliminarChequeRecido(ChequeRecido)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un ChequeRecido por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal ChequeRecido As ChequeRecido) As ComandoBase(Of ChequeRecido)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="ChequeRecido">ChequeRecido a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaChequeRecido(ByVal ChequeRecido As ChequeRecido) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaChequeRecido(ChequeRecido)
        End Function

        Public Shared Function ObtenerComandoConsultarChequeRecidosFiltro(ByVal ChequeRecido As ChequeRecido) As ComandoBase(Of IList(Of ChequeRecido))
            Return New ComandoConsultarChequeRecidosFiltro(ChequeRecido)
        End Function

    End Class
End Namespace