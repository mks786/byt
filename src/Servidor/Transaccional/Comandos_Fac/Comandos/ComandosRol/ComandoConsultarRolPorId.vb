Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Diginsoft.Bolet.ObjetosComunes.Entidades

Namespace Comandos.ComandosRol
    Public Class ComandoConsultarRolPorId
        Inherits ComandoBase(Of Rol)
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _rol As Rol

        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="rol">Rol que contiene el Id</param>
        Public Sub New(ByVal rol As Rol)
            Me._rol = rol
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

                Dim dao As IDaoRol = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoRol()
                Me.Receptor = New Receptor(Of Rol)(dao.ObtenerPorId(Me._rol.Id))

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