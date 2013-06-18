Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Comandos.ComandosFacTarifa
    Class ComandoConsultarFacTarifasFiltro
        Inherits ComandoBase(Of IList(Of FacTarifa))
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _FacTarifa As FacTarifa


        Public Sub New(ByVal FacTarifa As FacTarifa)
            Me._FacTarifa = FacTarifa
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

                Dim dao As IDaoFacTarifa = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoFacTarifa()
                Me.Receptor = New Receptor(Of IList(Of FacTarifa))(dao.ObtenerFacTarifasFiltro(Me._FacTarifa))

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