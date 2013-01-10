Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Comandos.ComandosDesgloseServicio
    Class ComandoConsultarFacDesgloseServiciosFiltro
        Inherits ComandoBase(Of IList(Of FacDesgloseServicio))
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _FacDesgloseServicio As FacDesgloseServicio


        Public Sub New(ByVal FacDesgloseServicio As FacDesgloseServicio)
            Me._FacDesgloseServicio = FacDesgloseServicio
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

                Dim dao As IDaoDesgloseServicio = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoDesgloseServicio()
                Me.Receptor = New Receptor(Of IList(Of FacDesgloseServicio))(dao.ObtenerFacDesgloseServiciosFiltro(Me._FacDesgloseServicio))

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