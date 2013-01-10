Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosMotivo
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosMotivo
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="FacMotivo">Motivo a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacMotivo As FacMotivo) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarMotivo(FacMotivo)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los Motivoes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los Motivoes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacMotivo))
            Return New ComandoConsultarTodosMotivos()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un Motivo
        ' ''' </summary>
        ' ''' <param name="usuario">Motivo que se va a Motivo</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarMotivo(ByVal FacMotivo As FacMotivo) As ComandoBase(Of Boolean)
            Return New ComandoEliminarMotivo(FacMotivo)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un Motivo por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal Motivo As FacMotivo) As ComandoBase(Of FacMotivo)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="FacMotivo">Motivo a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaMotivo(ByVal FacMotivo As FacMotivo) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaMotivo(FacMotivo)
        End Function
    End Class
End Namespace