﻿Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Comandos.ComandosFacFactura
    Public Class ComandoVerificarExistenciaFacFactura
        Inherits ComandoBase(Of Boolean)
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _FacFactura As FacFactura

        ' ''' <summary>
        ' ''' Constructor predeterminado
        ' ''' </summary>
        ' ''' <param name="pais">Pais a verificar</param>
        Public Sub New(ByVal FacFactura As FacFactura)
            Me._FacFactura = FacFactura
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

                Dim dao As IDaoFacFactura = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoFacFactura()
                Me.Receptor = New Receptor(Of Boolean)(dao.VerificarExistencia(Me._FacFactura.Id))

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