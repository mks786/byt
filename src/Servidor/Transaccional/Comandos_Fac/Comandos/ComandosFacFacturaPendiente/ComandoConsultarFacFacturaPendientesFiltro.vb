Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Comandos.ComandosFacFacturaPendiente
    Class ComandoConsultarFacFacturaPendientesFiltro
        Inherits ComandoBase(Of IList(Of FacFacturaPendiente))
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _FacFacturaPendiente As FacFacturaPendiente


        Public Sub New(ByVal FacFacturaPendiente As FacFacturaPendiente)
            Me._FacFacturaPendiente = FacFacturaPendiente
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

                Dim dao As IDaoFacFacturaPendiente = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoFacFacturaPendiente()
                Me.Receptor = New Receptor(Of IList(Of FacFacturaPendiente))(dao.ObtenerFacFacturaPendientesFiltro(Me._FacFacturaPendiente))

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