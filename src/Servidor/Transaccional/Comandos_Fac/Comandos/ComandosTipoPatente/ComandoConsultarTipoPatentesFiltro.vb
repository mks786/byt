Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Comandos.ComandosTipoPatente
    Class ComandoConsultarTipoPatentesFiltro
        Inherits ComandoBase(Of IList(Of TipoPatente))
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _TipoPatente As TipoPatente


        Public Sub New(ByVal TipoPatente As TipoPatente)
            Me._TipoPatente = TipoPatente
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

                Dim dao As IDaoTipoPatente = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoTipoPatente()
                Me.Receptor = New Receptor(Of IList(Of TipoPatente))(dao.ObtenerTipoPatentesFiltro(Me._TipoPatente))

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