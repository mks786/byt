Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Comandos.ComandosChequeRecido
    Class ComandoConsultarChequeRecidosFiltro
        Inherits ComandoBase(Of IList(Of ChequeRecido))
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _ChequeRecido As ChequeRecido


        Public Sub New(ByVal ChequeRecido As ChequeRecido)
            Me._ChequeRecido = ChequeRecido
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

                Dim dao As IDaoChequeRecido = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoChequeRecido()
                Me.Receptor = New Receptor(Of IList(Of ChequeRecido))(dao.ObtenerChequeRecidosFiltro(Me._ChequeRecido))

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