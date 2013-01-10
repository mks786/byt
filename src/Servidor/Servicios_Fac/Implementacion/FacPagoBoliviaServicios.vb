Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.LogicaNegocio.Controladores
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports System.Runtime.Remoting

Namespace Implementacion
    Public Class FacPagoBoliviaServicios
        Inherits MarshalByRefObject
        Implements IFacPagoBoliviaServicios


        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Método que obtiene todos los FacPagoBoliviaes
        ''' </summary>
        ''' <returns>Todos los FacPagoBoliviaes</returns>
        Public Function ConsultarTodos() As IList(Of FacPagoBolivia) Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacPagoBolivia).ConsultarTodos
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim FacPagoBolivias As IList(Of FacPagoBolivia) = ControladorFacPagoBolivia.ConsultarTodos()

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return FacPagoBolivias
        End Function


        Public Function ConsultarPorId(ByVal entidad As FacPagoBolivia) As FacPagoBolivia Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacPagoBolivia).ConsultarPorId
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que inserta o modifica un país
        ''' </summary>
        ''' <param name="entidad">País a insertar o modificar</param>
        ''' <param name="hash">hash del usuario logerad</param>
        ''' <returns></returns>
        Public Function InsertarOModificar(ByVal entidad As FacPagoBolivia, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacPagoBolivia).InsertarOModificar
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim exitoso As Boolean = ControladorFacPagoBolivia.InsertarOModificar(entidad, hash)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return exitoso
        End Function

        ''' <summary>
        ''' Método que elimina un FacPagoBolivia
        ''' </summary>
        ''' <param name="entidad">País a eliminar</param>
        ''' <param name="hash">Hash del usuario logeado</param>
        ''' <returns></returns>
        Public Function Eliminar(ByVal entidad As FacPagoBolivia, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacPagoBolivia).Eliminar
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim exitoso As Boolean = ControladorFacPagoBolivia.Eliminar(entidad, hash)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return exitoso
        End Function


        Public Function VerificarExistencia(ByVal entidad As FacPagoBolivia) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacPagoBolivia).VerificarExistencia
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim exitoso As Boolean = ControladorFacPagoBolivia.VerificarExistencia(entidad)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return exitoso
        End Function

        Public Function ObteneFacPagoBoliviasFiltro(ByVal FacPagoBolivia As FacPagoBolivia) As System.Collections.Generic.IList(Of Trascend.Bolet.ObjetosComunes.Entidades.FacPagoBolivia) Implements ObjetosComunes.ContratosServicios.IFacPagoBoliviaServicios.ObtenerFacPagoBoliviasFiltro
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim FacPagoBolivias As IList(Of FacPagoBolivia)

            FacPagoBolivias = ControladorFacPagoBolivia.ConsultarFacPagoBoliviasFiltro(FacPagoBolivia)

            Return FacPagoBolivias

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Function

        'Public Function ConsultarPorId(ByVal entidad As ObjetosComunes.Entidades.FacPagoBolivia) As ObjetosComunes.Entidades.FacPagoBolivia Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacPagoBolivia).ConsultarPorId
        '    Throw New NotImplementedException()
        'End Function

        'Public Function Eliminar(ByVal entidad As ObjetosComunes.Entidades.FacPagoBolivia, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacPagoBolivia).Eliminar
        '    Throw New NotImplementedException()
        'End Function

        'Public Function InsertarOModificar(ByVal entidad As ObjetosComunes.Entidades.FacPagoBolivia, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacPagoBolivia).InsertarOModificar
        '    Throw New NotImplementedException()
        'End Function

        'Public Function VerificarExistencia(ByVal entidad As ObjetosComunes.Entidades.FacPagoBolivia) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacPagoBolivia).VerificarExistencia
        '    Throw New NotImplementedException()
        'End Function
    End Class
End Namespace