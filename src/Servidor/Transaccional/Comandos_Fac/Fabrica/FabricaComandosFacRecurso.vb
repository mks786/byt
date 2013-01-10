Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacRecurso
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacRecurso
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="FacRecurso">FacRecurso a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacRecurso As FacRecurso) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacRecurso(FacRecurso)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacRecursoes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacRecursoes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacRecurso))
            Return New ComandoConsultarTodosFacRecursos()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un FacRecurso
        ' ''' </summary>
        ' ''' <param name="usuario">FacRecurso que se va a FacRecurso</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacRecurso(ByVal FacRecurso As FacRecurso) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacRecurso(FacRecurso)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacRecurso por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacRecurso As FacRecurso) As ComandoBase(Of FacRecurso)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="FacRecurso">FacRecurso a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacRecurso(ByVal FacRecurso As FacRecurso) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacRecurso(FacRecurso)
        End Function
    End Class
End Namespace