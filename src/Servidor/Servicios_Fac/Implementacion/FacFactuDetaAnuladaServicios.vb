Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.LogicaNegocio.Controladores
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports System.Runtime.Remoting

Namespace Implementacion
    Public Class FacFactuDetaAnuladaServicios
        Inherits MarshalByRefObject
        Implements IFacFactuDetaAnuladaServicios


        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Método que obtiene todos los FacFactuDetaAnuladaes
        ''' </summary>
        ''' <returns>Todos los FacFactuDetaAnuladaes</returns>
        Public Function ConsultarTodos() As IList(Of FacFactuDetaAnulada) Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacFactuDetaAnulada).ConsultarTodos
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim FacFactuDetaAnuladas As IList(Of FacFactuDetaAnulada) = ControladorFacFactuDetaAnulada.ConsultarTodos()

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return FacFactuDetaAnuladas
        End Function


        Public Function ConsultarPorId(ByVal entidad As FacFactuDetaAnulada) As FacFactuDetaAnulada Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacFactuDetaAnulada).ConsultarPorId
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que inserta o modifica un país
        ''' </summary>
        ''' <param name="entidad">País a insertar o modificar</param>
        ''' <param name="hash">hash del usuario logerad</param>
        ''' <returns></returns>
        Public Function InsertarOModificar(ByVal entidad As FacFactuDetaAnulada, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacFactuDetaAnulada).InsertarOModificar
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim exitoso As Boolean = ControladorFacFactuDetaAnulada.InsertarOModificar(entidad, hash)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return exitoso
        End Function

        ''' <summary>
        ''' Método que elimina un FacFactuDetaAnulada
        ''' </summary>
        ''' <param name="entidad">País a eliminar</param>
        ''' <param name="hash">Hash del usuario logeado</param>
        ''' <returns></returns>
        Public Function Eliminar(ByVal entidad As FacFactuDetaAnulada, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacFactuDetaAnulada).Eliminar
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim exitoso As Boolean = ControladorFacFactuDetaAnulada.Eliminar(entidad, hash)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return exitoso
        End Function


        Public Function VerificarExistencia(ByVal entidad As FacFactuDetaAnulada) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacFactuDetaAnulada).VerificarExistencia
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim exitoso As Boolean = ControladorFacFactuDetaAnulada.VerificarExistencia(entidad)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return exitoso
        End Function

        Public Function ObteneFacFactuDetaAnuladasFiltro(ByVal FacFactuDetaAnulada As FacFactuDetaAnulada) As System.Collections.Generic.IList(Of Trascend.Bolet.ObjetosComunes.Entidades.FacFactuDetaAnulada) Implements ObjetosComunes.ContratosServicios.IFacFactuDetaAnuladaServicios.ObtenerFacFactuDetaAnuladasFiltro
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim FacFactuDetaAnuladas As IList(Of FacFactuDetaAnulada)

            FacFactuDetaAnuladas = ControladorFacFactuDetaAnulada.ConsultarFacFactuDetaAnuladasFiltro(FacFactuDetaAnulada)

            Return FacFactuDetaAnuladas

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Function

        'Public Function ConsultarPorId(ByVal entidad As ObjetosComunes.Entidades.FacFactuDetaAnulada) As ObjetosComunes.Entidades.FacFactuDetaAnulada Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacFactuDetaAnulada).ConsultarPorId
        '    Throw New NotImplementedException()
        'End Function

        'Public Function Eliminar(ByVal entidad As ObjetosComunes.Entidades.FacFactuDetaAnulada, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacFactuDetaAnulada).Eliminar
        '    Throw New NotImplementedException()
        'End Function

        'Public Function InsertarOModificar(ByVal entidad As ObjetosComunes.Entidades.FacFactuDetaAnulada, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacFactuDetaAnulada).InsertarOModificar
        '    Throw New NotImplementedException()
        'End Function

        'Public Function VerificarExistencia(ByVal entidad As ObjetosComunes.Entidades.FacFactuDetaAnulada) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacFactuDetaAnulada).VerificarExistencia
        '    Throw New NotImplementedException()
        'End Function
    End Class
End Namespace