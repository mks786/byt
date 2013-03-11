Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Comandos.ComandosBancoG
    Class ComandoConsultarBancoGsFiltro
        Inherits ComandoBase(Of IList(Of BancoG))
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _BancoG As BancoG


        Public Sub New(ByVal BancoG As BancoG)
            Me._BancoG = BancoG
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

                Dim dao As IDaoBancoG = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoBancoG()
                Me.Receptor = New Receptor(Of IList(Of BancoG))(dao.ObtenerBancoGsFiltro(Me._BancoG))

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