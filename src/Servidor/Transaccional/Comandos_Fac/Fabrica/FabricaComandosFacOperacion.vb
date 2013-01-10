Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacOperacion
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacOperacion
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacOperacion">FacOperacion a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacOperacion As FacOperacion) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacOperacion(FacOperacion)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacOperaciones
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacOperaciones</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacOperacion))
            Return New ComandoConsultarTodosFacOperaciones()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacOperacion
        '' ''' </summary>
        '' ''' <param name="usuario">FacOperacion que se va a FacOperacion</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacOperacion(ByVal FacOperacion As FacOperacion) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacOperacion(FacOperacion)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacOperacion por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacOperacion As FacOperacion) As ComandoBase(Of FacOperacion)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacOperacion">FacOperacion a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacOperacion(ByVal FacOperacion As FacOperacion) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacOperacion(FacOperacion)
        End Function

        Public Shared Function ObtenerComandoConsultarFacOperacionesFiltro(ByVal FacOperacion As FacOperacion) As ComandoBase(Of IList(Of FacOperacion))
            Return New ComandoConsultarFacOperacionesFiltro(FacOperacion)
        End Function

    End Class
End Namespace