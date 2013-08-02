Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos

Namespace Comandos.ComandosCarpetaGestionAutomatica

    Class ComandoObtenerCarpetasPorIniciales
        Inherits ComandoBase(Of IList(Of CarpetaGestionAutomatica))

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
                    logger.Debug("Entrando al Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region


                Dim dao As IDaoCarpetaGestionAutomatica = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoCarpetaGestionAutomatica()
                Me.Receptor = New Receptor(Of IList(Of CarpetaGestionAutomatica))(dao.ObtenerCarpetasPorIniciales(Me._usuario))


                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Saliendo del Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As ApplicationException
                logger.[Error](ex.Message)
                Throw ex
            End Try
        End Sub
    End Class
End Namespace

