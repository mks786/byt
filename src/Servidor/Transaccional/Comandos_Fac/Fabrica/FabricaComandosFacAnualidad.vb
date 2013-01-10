Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacAnualidad
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacAnualidad
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="FacAnualidad">FacAnualidad a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacAnualidad As FacAnualidad) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacAnualidad(FacAnualidad)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacAnualidades
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacAnualidades</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacAnualidad))
            Return New ComandoConsultarTodosFacAnualidades()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un FacAnualidad
        ' ''' </summary>
        ' ''' <param name="usuario">FacAnualidad que se va a FacAnualidad</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacAnualidad(ByVal FacAnualidad As FacAnualidad) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacAnualidad(FacAnualidad)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacAnualidad por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacAnualidad As FacAnualidad) As ComandoBase(Of FacAnualidad)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="FacAnualidad">FacAnualidad a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacAnualidad(ByVal FacAnualidad As FacAnualidad) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacAnualidad(FacAnualidad)
        End Function
    End Class
End Namespace