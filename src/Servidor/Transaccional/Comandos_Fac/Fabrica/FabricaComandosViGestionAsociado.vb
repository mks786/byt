Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosViGestionAsociado
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosViGestionAsociado
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="ViGestionAsociado">ViGestionAsociado a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        'Public Shared Function ObtenerComandoInsertarOModificar(ByVal ViGestionAsociado As ViGestionAsociado) As ComandoBase(Of Boolean)
        '    Return New ComandoInsertarOModificarViGestionAsociado(ViGestionAsociado)
        'End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los ViGestionAsociadoes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los ViGestionAsociadoes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of ViGestionAsociado))
            Return New ComandoConsultarTodosViGestionAsociados()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un ViGestionAsociado
        '' ''' </summary>
        '' ''' <param name="usuario">ViGestionAsociado que se va a ViGestionAsociado</param>
        '' ''' <returns>Comando para eliminar</returns>
        'Public Shared Function ObtenerComandoEliminarViGestionAsociado(ByVal ViGestionAsociado As ViGestionAsociado) As ComandoBase(Of Boolean)
        '    Return New ComandoEliminarViGestionAsociado(ViGestionAsociado)
        'End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un ViGestionAsociado por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal ViGestionAsociado As ViGestionAsociado) As ComandoBase(Of ViGestionAsociado)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="ViGestionAsociado">ViGestionAsociado a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        'Public Shared Function ObtenerComandoVerificarExistenciaViGestionAsociado(ByVal ViGestionAsociado As ViGestionAsociado) As ComandoBase(Of Boolean)
        '    Return New ComandoVerificarExistenciaViGestionAsociado(ViGestionAsociado)
        'End Function
    End Class
End Namespace