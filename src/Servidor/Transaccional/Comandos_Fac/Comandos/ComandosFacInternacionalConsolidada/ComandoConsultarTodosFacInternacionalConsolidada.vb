Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos

Namespace Comandos.ComandosFacInternacionalConsolidada
    Public Class ComandoConsultarTodosFacInternacionalConsolidada
        Inherits ComandoBase(Of IList(Of FacInternacionalConsolidada))
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Overrides Sub Ejecutar()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                'Dim dao As IDaoFacInternacional = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoFacInternacional()
                'Me.Receptor = New Receptor(Of IList(Of FacInternacional))(dao.ObtenerTodos())

                Dim dao As IDaoFacInternacionalConsolidada = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoFacInternacionalConsolidada()
                Me.Receptor = New Receptor(Of IList(Of FacInternacionalConsolidada))(dao.ObtenerTodos())

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

