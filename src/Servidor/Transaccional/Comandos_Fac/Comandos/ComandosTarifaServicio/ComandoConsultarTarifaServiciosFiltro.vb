Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Namespace Comandos.ComandosTarifaServicio
    Class ComandoConsultarTarifaServiciosFiltro
        Inherits ComandoBase(Of IList(Of TarifaServicio))
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _TarifaServicio As TarifaServicio


        Public Sub New(ByVal TarifaServicio As TarifaServicio)
            Me._TarifaServicio = TarifaServicio
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

                Dim dao As IDaoTarifaServicio = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoTarifaServicio()
                Me.Receptor = New Receptor(Of IList(Of TarifaServicio))(dao.ObtenerTarifaServiciosFiltro(Me._TarifaServicio))

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