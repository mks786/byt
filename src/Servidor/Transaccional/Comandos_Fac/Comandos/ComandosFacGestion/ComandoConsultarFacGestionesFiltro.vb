Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Comandos.ComandosFacGestion
    Class ComandoConsultarFacGestionesFiltro
        Inherits ComandoBase(Of IList(Of FacGestion))
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _FacGestion As FacGestion


        Public Sub New(ByVal FacGestion As FacGestion)
            Me._FacGestion = FacGestion
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

                Dim dao As IDaoFacGestion = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoFacGestion()
                Me.Receptor = New Receptor(Of IList(Of FacGestion))(dao.ObtenerFacGestionesFiltro(Me._FacGestion))

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