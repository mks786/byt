Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports NLog
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports System.Configuration
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Fabrica
Imports Trascend.Bolet.Comandos.Comandos
Namespace Comandos.ComandosConceptoGestion
    Public Class ComandoEliminarConceptoGestion
        Inherits ComandoBase(Of Boolean)
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _ConceptoGestion As ConceptoGestion

        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ConceptoGestion">ConceptoGestion a eliminar</param>
        Public Sub New(ByVal ConceptoGestion As ConceptoGestion)
            Me._ConceptoGestion = ConceptoGestion
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

                Dim dao As IDaoConceptoGestion = FabricaDaoBaseFac.ObtenerFabricaDao().ObtenerDaoConceptoGestion()
                Me.Receptor = New Receptor(Of Boolean)(dao.Eliminar(Me._ConceptoGestion))

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