Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosServicio
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosServicio
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="Servicio">Servicio a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal Servicio As FacServicio) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarServicio(Servicio)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los Servicioes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los Servicioes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacServicio))
            Return New ComandoConsultarTodosServicios()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un Servicio
        ' ''' </summary>
        ' ''' <param name="usuario">Servicio que se va a Servicio</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarServicio(ByVal Servicio As FacServicio) As ComandoBase(Of Boolean)
            Return New ComandoEliminarServicio(Servicio)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un Servicio por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal Servicio As FacServicio) As ComandoBase(Of FacServicio)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="Servicio">Servicio a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaServicio(ByVal Servicio As FacServicio) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaServicio(Servicio)
        End Function
    End Class
End Namespace