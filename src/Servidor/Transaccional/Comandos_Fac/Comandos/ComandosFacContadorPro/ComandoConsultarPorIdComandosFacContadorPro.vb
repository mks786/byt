Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports NLog
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports System.Configuration
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Trascend.Bolet.Comandos.Comandos
Namespace Comandos.ComandosFacContadorPro
    Class ComandoConsultarPorIdFacContadorPro
        Inherits ComandoBase(Of FacContadorPro)
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _id As String

        ' ''' <summary>
        ' ''' Constructor predeterminado
        ' ''' </summary>
        '''' <param name="contador"></param>
        Public Sub New(ByVal id As String)
            Me._id = id
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

                Dim dao As IDaoFacContadorPro = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoFacContadorPro()
                Me.Receptor = New Receptor(Of FacContadorPro)(dao.ObtenerPorId(Me._id))

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
