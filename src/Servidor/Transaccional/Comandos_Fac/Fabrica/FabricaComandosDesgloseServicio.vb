Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosDesgloseServicio
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosDesgloseServicio
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="DesgloseServicio">DesgloseServicio a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal DesgloseServicio As FacDesgloseServicio) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarDesgloseServicio(DesgloseServicio)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los DesgloseServicioes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los DesgloseServicioes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacDesgloseServicio))
            Return New ComandoConsultarTodosDesgloseServicios()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un DesgloseServicio
        ' ''' </summary>
        ' ''' <param name="usuario">DesgloseServicio que se va a DesgloseServicio</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarDesgloseServicio(ByVal DesgloseServicio As FacDesgloseServicio) As ComandoBase(Of Boolean)
            Return New ComandoEliminarDesgloseServicio(DesgloseServicio)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un DesgloseServicio por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal DesgloseServicio As FacDesgloseServicio) As ComandoBase(Of FacDesgloseServicio)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="DesgloseServicio">DesgloseServicio a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaDesgloseServicio(ByVal DesgloseServicio As FacDesgloseServicio) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaDesgloseServicio(DesgloseServicio)
        End Function

        Public Shared Function ObtenerComandoConsultarFacDesgloseServiciosFiltro(ByVal FacDesgloseServicio As FacDesgloseServicio) As ComandoBase(Of IList(Of FacDesgloseServicio))
            Return New ComandoConsultarFacDesgloseServiciosFiltro(FacDesgloseServicio)
        End Function
    End Class
End Namespace