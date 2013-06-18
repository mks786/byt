Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.LogicaNegocio.Controladores
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports System.Runtime.Remoting

Namespace Implementacion
    Public Class FacTarifaServicios
        Inherits MarshalByRefObject
        Implements IFacTarifaServicios

        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Método que obtiene todos los FacTarifaes
        ''' </summary>
        ''' <returns>Todos los FacTarifaes</returns>
        Public Function ConsultarTodos() As IList(Of FacTarifa) Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacTarifa).ConsultarTodos
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim FacTarifas As IList(Of FacTarifa) = ControladorFacTarifa.ConsultarTodos()

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return FacTarifas
        End Function


        Public Function ConsultarPorId(ByVal entidad As FacTarifa) As FacTarifa Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacTarifa).ConsultarPorId
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que inserta o modifica un país
        ''' </summary>
        ''' <param name="entidad">País a insertar o modificar</param>
        ''' <param name="hash">hash del usuario logerad</param>
        ''' <returns></returns>
        Public Function InsertarOModificar(ByVal entidad As FacTarifa, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacTarifa).InsertarOModificar
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim exitoso As Boolean = ControladorFacTarifa.InsertarOModificar(entidad, hash)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return exitoso
        End Function

        ''' <summary>
        ''' Método que elimina un FacTarifa
        ''' </summary>
        ''' <param name="entidad">País a eliminar</param>
        ''' <param name="hash">Hash del usuario logeado</param>
        ''' <returns></returns>
        Public Function Eliminar(ByVal entidad As FacTarifa, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacTarifa).Eliminar
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim exitoso As Boolean = ControladorFacTarifa.Eliminar(entidad, hash)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return exitoso
        End Function


        Public Function VerificarExistencia(ByVal entidad As FacTarifa) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacTarifa).VerificarExistencia
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim exitoso As Boolean = ControladorFacTarifa.VerificarExistencia(entidad)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return exitoso
        End Function

        Public Function ObteneFacTarifasFiltro(ByVal FacTarifa As FacTarifa) As System.Collections.Generic.IList(Of Trascend.Bolet.ObjetosComunes.Entidades.FacTarifa) Implements ObjetosComunes.ContratosServicios.IFacTarifaServicios.ObtenerFacTarifasFiltro
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim FacTarifas As IList(Of FacTarifa)

            FacTarifas = ControladorFacTarifa.ConsultarFacTarifasFiltro(FacTarifa)

            Return FacTarifas

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Function

    End Class
End Namespace