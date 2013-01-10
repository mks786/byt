Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosRol
Imports Diginsoft.Bolet.ObjetosComunes.Entidades

Namespace Fabrica
    Public NotInheritable Class FabricaComandosRol
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un Rol
        ''' </summary>
        ''' <param name="rol">Rol a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el rol en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal rol As Rol) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarRol(rol)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para eliminar un Rol
        ''' </summary>
        ''' <param name="rol">Rol a eliminar en la base de datos</param>
        ''' <returns>El Comando que permite elimnar el rol en la base de datos</returns>
        Public Shared Function ObtenerComandoEliminarRol(ByVal rol As Rol) As ComandoBase(Of Boolean)
            Return New ComandoElimarRol(rol)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los roles
        ''' </summary>
        ''' <returns>El Comando para consultar todos los Roles</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of Rol))
            Return New ComandoConsultarTodosRoles()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un Rol por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal rol As Rol) As ComandoBase(Of Rol)
            Return New ComandoConsultarRolPorId(rol)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar los roles con los agentes
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarRolesYObjetos() As ComandoBase(Of IList(Of Rol))
            Return New ComandoConsultarRolesYObjetos()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="rol">Rol a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaRol(ByVal rol As Rol) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaRol(rol)
        End Function
    End Class
End Namespace