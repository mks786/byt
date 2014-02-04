Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports NLog
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports System.Configuration
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Trascend.Bolet.Comandos.Comandos

Namespace Comandos.ComandosFacAsociadoIntConsolidadoCxPInt
    Public Class ComandoEliminarFacAsociadoIntConsolidadoCxPInt
        Inherits ComandoBase(Of Boolean)
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _FacAsociadoInternacionalCxPInt As FacAsociadoIntConsolidadoCxPInt

        ''' <summary>
        ''' Constructor por defecto
        ''' </summary>
        ''' <param name="FacAsociadoInternacionalCxPInt"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal FacAsociadoInternacionalCxPInt As FacAsociadoIntConsolidadoCxPInt)
            Me._FacAsociadoInternacionalCxPInt = FacAsociadoInternacionalCxPInt
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

                Dim dao As IDaoFacAsociadoIntConsolidadoCxPInt = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoFacAsociadoIntConsolidadoCxPInt()
                Me.Receptor = New Receptor(Of Boolean)(dao.Eliminar(Me._FacAsociadoInternacionalCxPInt))

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
