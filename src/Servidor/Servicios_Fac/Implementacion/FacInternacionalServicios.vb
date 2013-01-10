Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.LogicaNegocio.Controladores
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports System.Runtime.Remoting

Namespace Implementacion
    Public Class FacInternacionalServicios
        Inherits MarshalByRefObject
        Implements IFacInternacionalServicios


        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Método que obtiene todos los FacInternacionales
        ''' </summary>
        ''' <returns>Todos los FacInternacionales</returns>
        Public Function ConsultarTodos() As IList(Of FacInternacional) Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacInternacional).ConsultarTodos
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim FacInternacionales As IList(Of FacInternacional) = ControladorFacInternacional.ConsultarTodos()

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return FacInternacionales
        End Function


        Public Function ConsultarPorId(ByVal entidad As FacInternacional) As FacInternacional Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacInternacional).ConsultarPorId
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que inserta o modifica un país
        ''' </summary>
        ''' <param name="entidad">País a insertar o modificar</param>
        ''' <param name="hash">hash del usuario logerad</param>
        ''' <returns></returns>
        Public Function InsertarOModificar(ByVal entidad As FacInternacional, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacInternacional).InsertarOModificar
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim exitoso As Boolean = ControladorFacInternacional.InsertarOModificar(entidad, hash)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return exitoso
        End Function

        ''' <summary>
        ''' Método que elimina un FacInternacional
        ''' </summary>
        ''' <param name="entidad">País a eliminar</param>
        ''' <param name="hash">Hash del usuario logeado</param>
        ''' <returns></returns>
        Public Function Eliminar(ByVal entidad As FacInternacional, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacInternacional).Eliminar
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim exitoso As Boolean = ControladorFacInternacional.Eliminar(entidad, hash)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return exitoso
        End Function


        Public Function VerificarExistencia(ByVal entidad As FacInternacional) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacInternacional).VerificarExistencia
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim exitoso As Boolean = ControladorFacInternacional.VerificarExistencia(entidad)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return exitoso
        End Function

        Public Function ObtenerFacInternacionalesFiltro(ByVal FacInternacional As FacInternacional) As System.Collections.Generic.IList(Of Trascend.Bolet.ObjetosComunes.Entidades.FacInternacional) Implements ObjetosComunes.ContratosServicios.IFacInternacionalServicios.ObtenerFacInternacionalesFiltro
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim FacInternacionales As IList(Of FacInternacional)

            FacInternacionales = ControladorFacInternacional.ConsultarFacInternacionalesFiltro(FacInternacional)

            Return FacInternacionales

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Function

        'Public Function ConsultarPorId(ByVal entidad As ObjetosComunes.Entidades.FacInternacional) As ObjetosComunes.Entidades.FacInternacional Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacInternacional).ConsultarPorId
        '    Throw New NotImplementedException()
        'End Function

        'Public Function Eliminar(ByVal entidad As ObjetosComunes.Entidades.FacInternacional, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacInternacional).Eliminar
        '    Throw New NotImplementedException()
        'End Function

        'Public Function InsertarOModificar(ByVal entidad As ObjetosComunes.Entidades.FacInternacional, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacInternacional).InsertarOModificar
        '    Throw New NotImplementedException()
        'End Function

        'Public Function VerificarExistencia(ByVal entidad As ObjetosComunes.Entidades.FacInternacional) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacInternacional).VerificarExistencia
        '    Throw New NotImplementedException()
        'End Function

    End Class
End Namespace