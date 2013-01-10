Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Fabrica
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Imports Trascend.Bolet.LogicaNegocio.Controladores
Namespace Controladores
    Public Class ControladorCorrespondencia
        Inherits ControladorBase
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Método que devuelve todos los Usuarios del sistema
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ConsultarTodos() As IList(Of Correspondencia)
            Dim retorno As IList(Of Correspondencia)
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region                
                Dim comando As ComandoBase(Of IList(Of Correspondencia)) = FabricaComandosCorrespondencia.ObtenerComandoConsultarTodos()
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

        ' ''' <summary>
        ' ''' Método que modifica un los datos de un Usuario
        ' ''' </summary>
        ' ''' <param name="usuario">Usuario a modificar</param>
        ' ''' <param name="hash">Hash del usuario que va a realizar la operacion</param>
        ' ''' <returns>True si la modificación fue exitosa, en caso contrario False</returns>
        Public Shared Function InsertarOModificar(ByVal Correspondencia As Correspondencia, ByVal hash As Integer) As Boolean
            Dim exitoso As Boolean = False

            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim comando As ComandoBase(Of Boolean) = FabricaComandosCorrespondencia.ObtenerComandoInsertarOModificar(Correspondencia)
                comando.Ejecutar()
                exitoso = comando.Receptor.ObjetoAlmacenado

                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As ApplicationException
                logger.[Error](ex.Message)
                Throw ex
            End Try
            Return exitoso
        End Function

        ' ''' <summary>
        ' ''' Método que consulta un pais por su Id
        ' ''' </summary>
        ' ''' <param name="pais">Pais con el Id del pais buscado</param>
        ' ''' <returns>El pais solicitado</returns>
        Public Shared Function ConsultarPorId(ByVal Correspondencia As Correspondencia) As Correspondencia
            Dim retorno As Correspondencia

            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim comando As ComandoBase(Of Correspondencia) = FabricaComandosCorrespondencia.ObtenerComandoConsultarPorID(Correspondencia)
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

        ' ''' <summary>
        ' ''' Método que elimina un pais
        ' ''' </summary>
        ' ''' <param name="usuario">Pais a eliminar</param>
        ' ''' <param name="hash">Hash del pais que va a realizar la operacion</param>
        ' ''' <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        Public Shared Function Eliminar(ByVal Correspondencia As Correspondencia, ByVal hash As Integer) As Boolean
            Dim exitoso As Boolean = False
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim comando As ComandoBase(Of Boolean) = FabricaComandosCorrespondencia.ObtenerComandoEliminarCorrespondencia(Correspondencia)
                comando.Ejecutar()
                exitoso = True

                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As ApplicationException
                logger.[Error](ex.Message)
                Throw ex
            End Try

            Return exitoso
        End Function

        ' ''' <summary>
        ' ''' Verifica si el agente existe
        ' ''' </summary>
        ' ''' <param name="pais">Pais a verificar</param>
        ' ''' <returns>True de existir, false en caso conrario</returns>
        Public Shared Function VerificarExistencia(ByVal Correspondencia As Correspondencia) As Boolean
            Dim existe As Boolean = False
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim comando As ComandoBase(Of Boolean) = FabricaComandosCorrespondencia.ObtenerComandoVerificarExistenciaCorrespondencia(Correspondencia)
                comando.Ejecutar()
                existe = comando.Receptor.ObjetoAlmacenado

                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As ApplicationException
                logger.[Error](ex.Message)
                Throw ex
            End Try

            Return existe
        End Function
    End Class
End Namespace