Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.LogicaNegocio.Controladores
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports System.Runtime.Remoting

Namespace Implementacion
    Public Class FacVistaFacturacionCxpInternaServicios
        Inherits MarshalByRefObject
        Implements IFacVistaFacturacionCxpInternaServicios



        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Método que obtiene todos los FacVistaFacturacionCxpInternaes
        ''' </summary>
        ''' <returns>Todos los FacVistaFacturacionCxpInternaes</returns>
        Public Function ConsultarTodos() As IList(Of FacVistaFacturacionCxpInterna) Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacVistaFacturacionCxpInterna).ConsultarTodos
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim FacVistaFacturacionCxpInternas As IList(Of FacVistaFacturacionCxpInterna) = ControladorFacVistaFacturacionCxpInterna.ConsultarTodos()

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return FacVistaFacturacionCxpInternas
        End Function

        Public Function ObteneFacVistaFacturacionCxpInternasFiltro(ByVal FacVistaFacturacionCxpInterna As FacVistaFacturacionCxpInterna) As System.Collections.Generic.IList(Of Trascend.Bolet.ObjetosComunes.Entidades.FacVistaFacturacionCxpInterna) Implements ObjetosComunes.ContratosServicios.IFacVistaFacturacionCxpInternaServicios.ObtenerFacVistaFacturacionCxpInternasFiltro
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim FacVistaFacturacionCxpInternas As IList(Of FacVistaFacturacionCxpInterna)

            FacVistaFacturacionCxpInternas = ControladorFacVistaFacturacionCxpInterna.ConsultarFacVistaFacturacionCxpInternasFiltro(FacVistaFacturacionCxpInterna)

            Return FacVistaFacturacionCxpInternas

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Function

        'Public Function ConsultarPorId(ByVal entidad As ObjetosComunes.Entidades.FacVistaFacturacionCxpInterna) As ObjetosComunes.Entidades.FacVistaFacturacionCxpInterna Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacVistaFacturacionCxpInterna).ConsultarPorId
        '    Throw New NotImplementedException()
        'End Function

        'Public Function Eliminar(ByVal entidad As ObjetosComunes.Entidades.FacVistaFacturacionCxpInterna, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacVistaFacturacionCxpInterna).Eliminar
        '    Throw New NotImplementedException()
        'End Function

        'Public Function InsertarOModificar(ByVal entidad As ObjetosComunes.Entidades.FacVistaFacturacionCxpInterna, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacVistaFacturacionCxpInterna).InsertarOModificar
        '    Throw New NotImplementedException()
        'End Function

        'Public Function VerificarExistencia(ByVal entidad As ObjetosComunes.Entidades.FacVistaFacturacionCxpInterna) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacVistaFacturacionCxpInterna).VerificarExistencia
        '    Throw New NotImplementedException()
        'End Function

        Public Function ConsultarPorId(ByVal entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacVistaFacturacionCxpInterna) As Trascend.Bolet.ObjetosComunes.Entidades.FacVistaFacturacionCxpInterna Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacVistaFacturacionCxpInterna).ConsultarPorId
            Throw New NotImplementedException()
        End Function

        Public Function Eliminar(ByVal entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacVistaFacturacionCxpInterna, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacVistaFacturacionCxpInterna).Eliminar
            Throw New NotImplementedException()
        End Function

        Public Function InsertarOModificar(ByVal entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacVistaFacturacionCxpInterna, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacVistaFacturacionCxpInterna).InsertarOModificar
            Throw New NotImplementedException()
        End Function

        Public Function VerificarExistencia(ByVal entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacVistaFacturacionCxpInterna) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacVistaFacturacionCxpInterna).VerificarExistencia
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace