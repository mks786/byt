Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports NLog
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports System.Configuration
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Trascend.Bolet.Comandos.Comandos

Namespace Comandos.ComandosFacInternacionalConsolidada
    Public Class ComandoEliminarFacInternacionalConsolidada
        Inherits ComandoBase(Of Boolean)
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _FacInternacionalConsolidada As FacInternacionalConsolidada

        ''' <summary>
        ''' Constructor por defecto que obtiene un registro del tipo FacInternacionalConsolidada
        ''' </summary>
        ''' <param name="FacInternacionalConsolidada">FacInternacionalConsolidada a eliminar</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal FacInternacionalConsolidada As FacInternacionalConsolidada)
            Me._FacInternacionalConsolidada = FacInternacionalConsolidada
        End Sub

        ''' <summary>
        ''' Método que ejecuta el comando
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub Ejecutar()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                'Dim dao As IDaoFacInternacional = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoFacInternacional()
                'Me.Receptor = New Receptor(Of Boolean)(dao.Eliminar(Me._FacInternacional))

                Dim dao As IDaoFacInternacionalConsolidada = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoFacInternacionalConsolidada()
                Me.Receptor = New Receptor(Of Boolean)(dao.Eliminar(Me._FacInternacionalConsolidada))

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

