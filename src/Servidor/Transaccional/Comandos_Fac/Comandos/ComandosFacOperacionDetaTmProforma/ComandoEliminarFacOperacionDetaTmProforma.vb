Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports NLog
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports System.Configuration
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Trascend.Bolet.Comandos.Comandos
Namespace Comandos.ComandosFacOperacionDetaTmProforma
    Public Class ComandoEliminarFacOperacionDetaTmProforma
        Inherits ComandoBase(Of Boolean)
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _FacOperacionDetaTmProforma As FacOperacionDetaTmProforma

        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="FacOperacionDetaTmProforma">FacOperacionDetaTmProforma a eliminar</param>
        Public Sub New(ByVal FacOperacionDetaTmProforma As FacOperacionDetaTmProforma)
            Me._FacOperacionDetaTmProforma = FacOperacionDetaTmProforma
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

                Dim dao As IDaoFacOperacionDetaTmProforma = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoFacOperacionDetaTmProforma()
                Me.Receptor = New Receptor(Of Boolean)(dao.Eliminar(Me._FacOperacionDetaTmProforma))

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