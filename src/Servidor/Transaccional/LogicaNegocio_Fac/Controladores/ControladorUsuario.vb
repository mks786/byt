Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Fabrica
Imports Diginsoft.Bolet.ObjetosComunes.Entidades

Namespace Controladores
    Public Class ControladorUsuario
        Inherits ControladorBase
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Método que devuelve todos los Usuarios
        ''' </summary>
        ''' <returns>Lista de usuarios</returns>
        Public Shared Function ConsultarTodos() As IList(Of Usuario)
            Dim retorno As IList(Of Usuario)
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim comando As ComandoBase(Of IList(Of Usuario)) = FabricaComandosUsuario.ObtenerComandoConsultarTodos()
                comando.Ejecutar()
                retorno = comando.Receptor.ObjetoAlmacenado

                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As ApplicationException
                logger.[Error](ex.Message)
                Throw ex
            End Try
            Return retorno
        End Function

        ''' <summary>
        ''' Método que modifica un los datos de un Usuario
        ''' </summary>
        ''' <param name="usuario">Usuario a modificar</param>
        ''' <param name="hash">Hash del usuario que va a realizar la operacion</param>
        ''' <returns>True si la modificación fue exitosa, en caso contrario False</returns>
        Public Shared Function InsertarOModificar(ByVal usuario As Usuario, ByVal hash As Integer) As Boolean
            Dim exitoso As Boolean = False

            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                'Si el usuario no tiene contraseña (Agregación), se coloca como contraseña el Id
                If String.IsNullOrEmpty(usuario.Password) Then
                    usuario.Password = usuario.Id
                End If

                Dim comando As ComandoBase(Of Boolean) = FabricaComandosUsuario.ObtenerComandoInsertarOModificar(usuario)
                comando.Ejecutar()
                exitoso = comando.Receptor.ObjetoAlmacenado

                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As ApplicationException
                logger.[Error](ex.Message)
                Throw ex
            End Try
            Return exitoso
        End Function


        ''' <summary>
        ''' Método que elimina un usuario
        ''' </summary>
        ''' <param name="usuario">Usuario a eliminar</param>
        ''' <param name="hash">Hash del usuario que va a realizar la operacion</param>
        ''' <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        Public Shared Function Eliminar(ByVal usuario As Usuario, ByVal hash As Integer) As Boolean
            Dim exitoso As Boolean = False
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim comando As ComandoBase(Of Boolean) = FabricaComandosUsuario.ObtenerComandoEliminarUsuario(usuario)
                comando.Ejecutar()
                exitoso = True

                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As ApplicationException
                logger.[Error](ex.Message)
                Throw ex
            End Try

            Return exitoso
        End Function

        ''' <summary>
        ''' Método que autentica un usuario
        ''' </summary>
        ''' <param name="usuario">Usuario a atuenticar</param>
        ''' <returns>True si la autenticacion fue exitosa, en caso contrario False</returns>
        Public Shared Function Autenticar(ByVal usuario As Usuario) As Usuario
            Dim usuarioAutenticado As Usuario
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim comando As ComandoBase(Of Usuario) = FabricaComandosUsuario.ObtenerComandoAutenticarUsuario(usuario)
                comando.Ejecutar()
                usuarioAutenticado = comando.Receptor.ObjetoAlmacenado
                If usuarioAutenticado IsNot Nothing Then
                    usuarioAutenticado = CrearSesion(usuarioAutenticado)
                End If

                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As ApplicationException
                logger.[Error](ex.Message)
                Throw ex
            End Try

            Return usuarioAutenticado
        End Function

        ''' <summary>
        ''' Verifica si el usuario existe
        ''' </summary>
        ''' <param name="usuario">Usuario a verificar</param>
        ''' <returns>True de existir, false en caso conrario</returns>
        Public Shared Function VerificarExistencia(ByVal usuario As Usuario) As Boolean
            Dim existe As Boolean = False
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim comando As ComandoBase(Of Boolean) = FabricaComandosUsuario.ObtenerComandoVerificarExistenciaUsuario(usuario)
                comando.Ejecutar()
                existe = comando.Receptor.ObjetoAlmacenado

                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As ApplicationException
                logger.[Error](ex.Message)
                Throw ex
            End Try

            Return existe
        End Function
    End Class
End Namespace