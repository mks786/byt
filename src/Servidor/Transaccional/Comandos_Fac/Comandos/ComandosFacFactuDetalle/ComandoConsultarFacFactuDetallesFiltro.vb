Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Comandos.ComandosFacFactuDetalle
    Class ComandoConsultarFacFactuDetallesFiltro
        Inherits ComandoBase(Of IList(Of FacFactuDetalle))
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _FacFactuDetalle As FacFactuDetalle


        Public Sub New(ByVal FacFactuDetalle As FacFactuDetalle)
            Me._FacFactuDetalle = FacFactuDetalle
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

                Dim dao As IDaoFacFactuDetalle = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoFacFactuDetalle()
                Me.Receptor = New Receptor(Of IList(Of FacFactuDetalle))(dao.ObtenerFacFactuDetallesFiltro(Me._FacFactuDetalle))

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