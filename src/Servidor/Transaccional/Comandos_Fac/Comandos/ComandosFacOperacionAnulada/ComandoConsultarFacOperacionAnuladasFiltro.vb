Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Comandos.ComandosFacOperacionAnulada
    Class ComandoConsultarFacOperacionAnuladasFiltro
        Inherits ComandoBase(Of IList(Of FacOperacionAnulada))
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _FacOperacionAnulada As FacOperacionAnulada


        Public Sub New(ByVal FacOperacionAnulada As FacOperacionAnulada)
            Me._FacOperacionAnulada = FacOperacionAnulada
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

                Dim dao As IDaoFacOperacionAnulada = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoFacOperacionAnulada()
                Me.Receptor = New Receptor(Of IList(Of FacOperacionAnulada))(dao.ObtenerFacOperacionAnuladasFiltro(Me._FacOperacionAnulada))

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