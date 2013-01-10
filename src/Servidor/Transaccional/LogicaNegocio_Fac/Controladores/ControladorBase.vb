Imports System.Collections.Generic
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports System.Configuration
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Fabrica
Imports NLog

Namespace Controladores
    Public Class ControladorBase
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private Shared _usuarios As IList(Of Usuario) = New List(Of Usuario)()

        ''' <summary>
        ''' Método que crea la sesión del cliente
        ''' </summary>
        ''' <param name="usuario">Usuario autenticado</param>
        ''' <returns>Usuario autenticado con el hash</returns>
        Public Shared Function CrearSesion(ByVal usuario As Usuario) As Usuario
            Dim agregar As Boolean = True
            usuario.Hash = usuario.GetHashCode()

            For Each usu As Usuario In _usuarios
                If usu.Id = usuario.Id Then
                    agregar = False
                End If
            Next

            If agregar Then
                _usuarios.Add(usuario)
            End If

            System.Console.WriteLine("Usuario agregado: " & Convert.ToString(usuario.Hash))
            Return usuario
        End Function

        ''' <summary>
        ''' Método que obtiene el usuario autenticado por su hash
        ''' </summary>
        ''' <param name="hash">Hash del usuario autenticado</param>
        ''' <returns>El usuario autenticado</returns>
        Public Shared Function ObtenerUsuarioPorHash(ByVal hash As Integer) As Usuario
            Dim retorno As Usuario = Nothing

            For Each usuario As Usuario In _usuarios
                If hash = usuario.Hash Then
                    retorno = usuario
                    Exit For
                End If
            Next

            Return retorno
        End Function

        ''' <summary>
        ''' Método que cierra la sesion del cliente
        ''' </summary>
        ''' <param name="hash"></param>
        Public Shared Sub CerrarSesion(ByVal hash As Integer)
            For Each usuario As Usuario In _usuarios
                If hash = usuario.Hash Then
                    _usuarios.Remove(usuario)
                    System.Console.WriteLine("Usuario eliminado: " + usuario.Hash)
                    Exit For
                End If
            Next
        End Sub


        'Public Shared Function AuditoriaPorFkyTabla(ByVal auditoria As Auditoria) As IList(Of Auditoria)
        '    Dim retorno As IList(Of Auditoria)
        '    Try
        '        '#Region "trace"
        '        If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
        '            logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
        '        End If
        '        '#End Region

        '        Dim comando As ComandoBase(Of IList(Of Auditoria)) = FabricaComandosAuditoria.ObtenerComandoAuditoriaPorFkyTabla(auditoria)
        '        comando.Ejecutar()
        '        retorno = comando.Receptor.ObjetoAlmacenado

        '        '#Region "trace"
        '        If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
        '            logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
        '            '#End Region
        '        End If
        '    Catch ex As ApplicationException
        '        logger.[Error](ex.Message)
        '        Throw ex
        '    End Try
        '    Return retorno
        'End Function

    End Class
End Namespace