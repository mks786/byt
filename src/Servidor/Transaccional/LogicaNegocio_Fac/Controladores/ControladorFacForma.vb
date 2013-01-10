Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Fabrica
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Imports Trascend.Bolet.LogicaNegocio.Controladores
Namespace Controladores
    Public Class ControladorFacForma
        Inherits ControladorBase
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Método que devuelve todos los Usuarios del sistema
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ConsultarTodos() As IList(Of FacForma)
            Dim retorno As IList(Of FacForma)
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region                
                Dim comando As ComandoBase(Of IList(Of FacForma)) = FabricaComandosFacForma.ObtenerComandoConsultarTodos()
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
        Public Shared Function InsertarOModificar(ByVal FacForma As FacForma, ByVal hash As Integer) As Boolean
            Dim exitoso As Boolean = False

            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim comando As ComandoBase(Of Boolean) = FabricaComandosFacForma.ObtenerComandoInsertarOModificar(FacForma)
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
        Public Shared Function ConsultarPorId(ByVal FacForma As FacForma) As FacForma
            Dim retorno As FacForma

            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim comando As ComandoBase(Of FacForma) = FabricaComandosFacForma.ObtenerComandoConsultarPorID(FacForma)
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
        Public Shared Function Eliminar(ByVal FacForma As FacForma, ByVal hash As Integer) As Boolean
            Dim exitoso As Boolean = False
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim comando As ComandoBase(Of Boolean) = FabricaComandosFacForma.ObtenerComandoEliminarFacForma(FacForma)
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
        Public Shared Function VerificarExistencia(ByVal FacForma As FacForma) As Boolean
            Dim existe As Boolean = False
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim comando As ComandoBase(Of Boolean) = FabricaComandosFacForma.ObtenerComandoVerificarExistenciaFacForma(FacForma)
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


        Public Shared Function ConsultarFacFormasFiltro(ByVal FacForma As FacForma) As IList(Of FacForma)
            Dim retorno As IList(Of FacForma)

            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim comando As ComandoBase(Of IList(Of FacForma)) = FabricaComandosFacForma.ObtenerComandoConsultarFacFormasFiltro(FacForma)
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