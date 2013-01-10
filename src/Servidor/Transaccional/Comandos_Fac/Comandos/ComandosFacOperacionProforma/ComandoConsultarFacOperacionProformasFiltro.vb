Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Comandos.ComandosFacOperacionProforma
    Class ComandoConsultarFacOperacionProformasFiltro
        Inherits ComandoBase(Of IList(Of FacOperacionProforma))
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _FacOperacionProforma As FacOperacionProforma


        Public Sub New(ByVal FacOperacionProforma As FacOperacionProforma)
            Me._FacOperacionProforma = FacOperacionProforma
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

                Dim dao As IDaoFacOperacionProforma = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoFacOperacionProforma()
                Me.Receptor = New Receptor(Of IList(Of FacOperacionProforma))(dao.ObtenerFacOperacionProformasFiltro(Me._FacOperacionProforma))

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