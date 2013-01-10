Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacJustificacion
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacJustificacion
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="FacJustificacion">FacJustificacion a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacJustificacion As FacJustificacion) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacJustificacion(FacJustificacion)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacJustificaciones
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacJustificaciones</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacJustificacion))
            Return New ComandoConsultarTodosFacJustificaciones()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un FacJustificacion
        ' ''' </summary>
        ' ''' <param name="usuario">FacJustificacion que se va a FacJustificacion</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacJustificacion(ByVal FacJustificacion As FacJustificacion) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacJustificacion(FacJustificacion)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacJustificacion por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacJustificacion As FacJustificacion) As ComandoBase(Of FacJustificacion)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="FacJustificacion">FacJustificacion a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacJustificacion(ByVal FacJustificacion As FacJustificacion) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacJustificacion(FacJustificacion)
        End Function
    End Class
End Namespace