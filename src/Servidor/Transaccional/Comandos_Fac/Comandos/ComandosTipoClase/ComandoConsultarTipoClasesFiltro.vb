Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Comandos.ComandosTipoClase
    Class ComandoConsultarTipoClasesFiltro
        Inherits ComandoBase(Of IList(Of TipoClase))
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _TipoClase As TipoClase


        Public Sub New(ByVal TipoClase As TipoClase)
            Me._TipoClase = TipoClase
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

                Dim dao As IDaoTipoClase = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoTipoClase()
                Me.Receptor = New Receptor(Of IList(Of TipoClase))(dao.ObtenerTipoClasesFiltro(Me._TipoClase))

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