Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos

Namespace Comandos.ComandosFacAsociadoIntConsolidadoCxPInt
    Public Class ComandoInsertarOModificarFacAsociadoConsolidadoCxPInt
        Inherits ComandoBase(Of Boolean)
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _FacAsociadoConsolidadoCxPInt As FacAsociadoIntConsolidadoCxPInt

        ''' <summary>
        ''' Constructor por defecto que recibe los datos de un Asociado Internacional Consolidado
        ''' </summary>
        ''' <param name="FacAsociadoConsolidadoCxPInt">Datos de facturacion de un Asociado Internacional Consolidado</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal FacAsociadoConsolidadoCxPInt As FacAsociadoIntConsolidadoCxPInt)

            Me._FacAsociadoConsolidadoCxPInt = FacAsociadoConsolidadoCxPInt

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
                Me.Receptor = New Receptor(Of Boolean)(dao.InsertarOModificar(Me._FacAsociadoConsolidadoCxPInt))

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
