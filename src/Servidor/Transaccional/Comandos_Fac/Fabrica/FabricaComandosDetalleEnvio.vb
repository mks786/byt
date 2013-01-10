Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosDetalleEnvio
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosDetalleEnvio
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="DetalleEnvio">DetalleEnvio a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal DetalleEnvio As FacDetalleEnvio) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarDetalleEnvio(DetalleEnvio)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los DetalleEnvioes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los DetalleEnvioes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacDetalleEnvio))
            Return New ComandoConsultarTodosDetalleEnvios()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un DetalleEnvio
        ' ''' </summary>
        ' ''' <param name="usuario">DetalleEnvio que se va a DetalleEnvio</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarDetalleEnvio(ByVal DetalleEnvio As FacDetalleEnvio) As ComandoBase(Of Boolean)
            Return New ComandoEliminarDetalleEnvio(DetalleEnvio)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un DetalleEnvio por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal DetalleEnvio As FacDetalleEnvio) As ComandoBase(Of FacDetalleEnvio)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="DetalleEnvio">DetalleEnvio a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaDetalleEnvio(ByVal DetalleEnvio As FacDetalleEnvio) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaDetalleEnvio(DetalleEnvio)
        End Function
    End Class
End Namespace