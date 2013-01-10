﻿Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports NLog
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports System.Configuration
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Trascend.Bolet.Comandos.Comandos
Namespace Comandos.ComandosFacOperacionDetalle
    Public Class ComandoEliminarFacOperacionDetalle
        Inherits ComandoBase(Of Boolean)
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _FacOperacionDetalle As FacOperacionDetalle

        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="FacOperacionDetalle">FacOperacionDetalle a eliminar</param>
        Public Sub New(ByVal FacOperacionDetalle As FacOperacionDetalle)
            Me._FacOperacionDetalle = FacOperacionDetalle
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

                Dim dao As IDaoFacOperacionDetalle = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoFacOperacionDetalle()
                Me.Receptor = New Receptor(Of Boolean)(dao.Eliminar(Me._FacOperacionDetalle))

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