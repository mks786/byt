Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports NLog
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports System.Configuration
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Trascend.Bolet.Comandos.Comandos
Namespace Comandos.ComandosFacAnuladaFisica
    Public Class ComandoEliminarFacAnuladaFisica
        Inherits ComandoBase(Of Boolean)
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _FacAnuladaFisica As FacAnuladaFisica

        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="FacAnuladaFisica">FacAnuladaFisica a eliminar</param>
        Public Sub New(ByVal FacAnuladaFisica As FacAnuladaFisica)
            Me._FacAnuladaFisica = FacAnuladaFisica
        End Sub

        ''' <summary>
        ''' Método que ejecuta el comando
        ''' </summary>
        Public Overrides Sub Ejecutar()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim dao As IDaoFacAnuladaFisica = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoFacAnuladaFisica()
                Me.Receptor = New Receptor(Of Boolean)(dao.Eliminar(Me._FacAnuladaFisica))

                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As ApplicationException
                logger.[Error](ex.Message)
                Throw ex
            End Try
        End Sub
    End Class
End Namespace