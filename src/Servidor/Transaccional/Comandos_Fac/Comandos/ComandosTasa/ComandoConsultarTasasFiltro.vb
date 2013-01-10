Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
'Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Comandos.ComandosTasa
    Class ComandoConsultarTasasFiltro
        Inherits ComandoBase(Of IList(Of Tasa))
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _Tasa As Tasa


        Public Sub New(ByVal Tasa As Tasa)
            Me._Tasa = Tasa
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

                Dim dao As IDaoTasa = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoTasa()
                Me.Receptor = New Receptor(Of IList(Of Tasa))(dao.ObtenerTasasFiltro(Me._Tasa))

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