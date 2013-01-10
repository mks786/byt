Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
'Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Comandos.ComandosFacImpuesto
    Class ComandoConsultarFacImpuestosFiltro
        Inherits ComandoBase(Of IList(Of FacImpuesto))
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _FacImpuesto As FacImpuesto


        Public Sub New(ByVal FacImpuesto As FacImpuesto)
            Me._FacImpuesto = FacImpuesto
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

                Dim dao As IDaoFacImpuesto = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoFacImpuesto()
                Me.Receptor = New Receptor(Of IList(Of FacImpuesto))(dao.ObtenerFacImpuestosFiltro(Me._FacImpuesto))

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