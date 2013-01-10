Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Diginsoft.Bolet.ObjetosComunes.Entidades

Namespace Comandos.ComandosDepartamento
    Public Class ComandoConsultarDepartamentoPorId
        Inherits ComandoBase(Of Departamento)
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _departamento As Departamento

        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="departamento"></param>
        Public Sub New(ByVal departamento As Departamento)
            Me._departamento = departamento
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

                Dim dao As IDaoDepartamento = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoDepartamento()
                Me.Receptor = New Receptor(Of Departamento)(dao.ObtenerPorId(Me._departamento.Id))

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