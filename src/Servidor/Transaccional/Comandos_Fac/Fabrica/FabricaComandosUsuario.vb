Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosUsuario
Imports Diginsoft.Bolet.ObjetosComunes.Entidades

Namespace Fabrica
    Public NotInheritable Class FabricaComandosUsuario
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los usuarios
        ''' </summary>
        ''' <returns>El Comando para consultar todos los Usuarios</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of Usuario))
            Return New ComandoConsultarTodosUsuarios()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un Usuario por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal usuario As Usuario) As ComandoBase(Of Usuario)
            Return New ComandoConsultarUsuarioPorID(usuario)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para modificar un Usuario
        ''' </summary>
        ''' <param name="usuario">Usuario a insertar o modificar</param>
        ''' <returns>El comando para realizar la insercion o modificacion</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal usuario As Usuario) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarUsuario(usuario)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para elimnar un usuario
        ''' </summary>
        ''' <param name="usuario">Usuario que se va a eliminar</param>
        ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarUsuario(ByVal usuario As Usuario) As ComandoBase(Of Boolean)
            Return New ComandoEliminarUsuario(usuario)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para autentificar por usuario
        ''' </summary>
        ''' <param name="usuario">Usuario que se va a autentificar</param>
        ''' <returns>Comando para realizar la autentificacion</returns>
        Public Shared Function ObtenerComandoAutenticarUsuario(ByVal usuario As Usuario) As ComandoBase(Of Usuario)
            Return New ComandoAutentificarUsuario(usuario)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="usuario">Usuario a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaUsuario(ByVal usuario As Usuario) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaUsuario(usuario)
        End Function
    End Class

End Namespace
