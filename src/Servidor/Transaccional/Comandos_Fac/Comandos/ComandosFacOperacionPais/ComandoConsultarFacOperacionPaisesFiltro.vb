Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Comandos.ComandosFacOperacionPais
    Class ComandoConsultarFacOperacionPaisesFiltro
        Inherits ComandoBase(Of IList(Of FacOperacionPais))
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _FacOperacionPais As FacOperacionPais


        Public Sub New(ByVal FacOperacionPais As FacOperacionPais)
            Me._FacOperacionPais = FacOperacionPais
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

                Dim dao As IDaoFacOperacionPais = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoFacOperacionPais()
                Me.Receptor = New Receptor(Of IList(Of FacOperacionPais))(dao.ObtenerFacOperacionPaisesFiltro(Me._FacOperacionPais))

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