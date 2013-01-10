Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Fabrica
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Imports Trascend.Bolet.LogicaNegocio.Controladores
Namespace Controladores
    Public Class ControladorChequeRecido
        Inherits ControladorBase
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Método que devuelve todos los Usuarios del sistema
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ConsultarTodos() As IList(Of ChequeRecido)
            Dim retorno As IList(Of ChequeRecido)
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region                
                Dim comando As ComandoBase(Of IList(Of ChequeRecido)) = FabricaComandosChequeRecido.ObtenerComandoConsultarTodos()
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
        Public Shared Function InsertarOModificar(ByVal ChequeRecido As ChequeRecido, ByVal hash As Integer) As Boolean
            Dim exitoso As Boolean = False

            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim comando As ComandoBase(Of Boolean) = FabricaComandosChequeRecido.ObtenerComandoInsertarOModificar(ChequeRecido)
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
        Public Shared Function ConsultarPorId(ByVal ChequeRecido As ChequeRecido) As ChequeRecido
            Dim retorno As ChequeRecido

            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim comando As ComandoBase(Of ChequeRecido) = FabricaComandosChequeRecido.ObtenerComandoConsultarPorID(ChequeRecido)
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
        Public Shared Function Eliminar(ByVal ChequeRecido As ChequeRecido, ByVal hash As Integer) As Boolean
            Dim exitoso As Boolean = False
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim comando As ComandoBase(Of Boolean) = FabricaComandosChequeRecido.ObtenerComandoEliminarChequeRecido(ChequeRecido)
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
        Public Shared Function VerificarExistencia(ByVal ChequeRecido As ChequeRecido) As Boolean
            Dim existe As Boolean = False
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim comando As ComandoBase(Of Boolean) = FabricaComandosChequeRecido.ObtenerComandoVerificarExistenciaChequeRecido(ChequeRecido)
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


        Public Shared Function ConsultarChequeRecidosFiltro(ByVal ChequeRecido As ChequeRecido) As IList(Of ChequeRecido)
            Dim retorno As IList(Of ChequeRecido)

            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim comando As ComandoBase(Of IList(Of ChequeRecido)) = FabricaComandosChequeRecido.ObtenerComandoConsultarChequeRecidosFiltro(ChequeRecido)
                comando.Ejecutar()
                retorno = comando.Receptor.ObjetoAlmacenado

                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Saliendo del Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
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