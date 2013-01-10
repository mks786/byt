Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports NLog
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports System.Configuration
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica

Namespace Comandos.ComandosUsuario
    Public Class ComandoAutentificarUsuario
        Inherits ComandoBase(Of Usuario)
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _usuario As Usuario

        Public Sub New(ByVal usuario As Usuario)
            Me._usuario = usuario
        End Sub

        ''' <summary>
        ''' Método que ejecuta el comando
        ''' </summary>
        Public Overrides Sub Ejecutar()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim dao As IDaoUsuario = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoUsuario()
                Me.Receptor = New Receptor(Of Usuario)(dao.Autenticar(Me._usuario))



                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As ApplicationException
                logger.[Error](ex.Message)
                Throw ex
            End Try
        End Sub
    End Class
End Namespace