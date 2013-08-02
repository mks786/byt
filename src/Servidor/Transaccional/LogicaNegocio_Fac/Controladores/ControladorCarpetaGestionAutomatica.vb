Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Fabrica
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Imports Trascend.Bolet.LogicaNegocio.Controladores
Imports Trascend.Bolet.Comandos.Fabrica

Namespace Controladores

    Public Class ControladorCarpetaGestionAutomatica
        Inherits ControladorBase
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Shared Function ObtenerCarpetasPorIniciales(ByVal Usuario As Usuario) As IList(Of CarpetaGestionAutomatica)
            Dim retorno As IList(Of CarpetaGestionAutomatica)
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region  

                Dim comando As ComandoBase(Of IList(Of CarpetaGestionAutomatica)) = FabricaComandosCarpetaGestionAutomatica.ObtenerComandoObtenerCarpetasPorIniciales(Usuario)
                comando.Ejecutar()
                retorno = comando.Receptor.ObjetoAlmacenado

                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As ApplicationException
                logger.[Error](ex.Message)
                Throw ex
            End Try
            Return retorno

        End Function


    End Class
End Namespace

