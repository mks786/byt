Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.LogicaNegocio.Controladores
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports System.Runtime.Remoting

Namespace Implementacion
    Public Class ViGestionAsociadoServicios
        Inherits MarshalByRefObject
        Implements IViGestionAsociadoServicios

        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Método que obtiene todos los ViGestionAsociadoes
        ''' </summary>
        ''' <returns>Todos los ViGestionAsociadoes</returns>
        Public Function ConsultarTodos() As IList(Of ViGestionAsociado) Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ViGestionAsociado).ConsultarTodos
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim ViGestionAsociados As IList(Of ViGestionAsociado) = ControladorViGestionAsociado.ConsultarTodos()

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return ViGestionAsociados
        End Function


        'Public Function ConsultarPorId(ByVal entidad As ObjetosComunes.Entidades.ViGestionAsociado) As ObjetosComunes.Entidades.ViGestionAsociado Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.ViGestionAsociado).ConsultarPorId
        '    Throw New NotImplementedException()
        'End Function

        '''' <summary>
        '''' Método que inserta o modifica un país
        '''' </summary>
        '''' <param name="entidad">País a insertar o modificar</param>
        '''' <param name="hash">hash del usuario logerad</param>
        '''' <returns></returns>
        'Public Function InsertarOModificar(ByVal entidad As ObjetosComunes.Entidades.ViGestionAsociado, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.ViGestionAsociado).InsertarOModificar
        '    '#Region "trace"
        '    If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
        '        logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
        '    End If
        '    '#End Region

        '    Dim exitoso As Boolean = ControladorViGestionAsociado.InsertarOModificar(entidad, hash)

        '    '#Region "trace"
        '    If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
        '        logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
        '    End If
        '    '#End Region

        '    Return exitoso
        'End Function

        '''' <summary>
        '''' Método que elimina un ViGestionAsociado
        '''' </summary>
        '''' <param name="entidad">País a eliminar</param>
        '''' <param name="hash">Hash del usuario logeado</param>
        '''' <returns></returns>
        'Public Function Eliminar(ByVal entidad As ObjetosComunes.Entidades.ViGestionAsociado, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.ViGestionAsociado).Eliminar
        '    '#Region "trace"
        '    If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
        '        logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
        '    End If
        '    '#End Region

        '    Dim exitoso As Boolean = ControladorViGestionAsociado.Eliminar(entidad, hash)

        '    '#Region "trace"
        '    If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
        '        logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
        '    End If
        '    '#End Region

        '    Return exitoso
        'End Function


        'Public Function VerificarExistencia(ByVal entidad As ObjetosComunes.Entidades.ViGestionAsociado) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.ViGestionAsociado).VerificarExistencia
        '    '#Region "trace"
        '    If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
        '        logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
        '    End If
        '    '#End Region

        '    Dim exitoso As Boolean = ControladorViGestionAsociado.VerificarExistencia(entidad)

        '    '#Region "trace"
        '    If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
        '        logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
        '    End If
        '    '#End Region

        '    Return exitoso
        'End Function

        Public Function ConsultarPorId(ByVal entidad As ViGestionAsociado) As ViGestionAsociado Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ViGestionAsociado).ConsultarPorId
            Throw New NotImplementedException()
        End Function

        Public Function Eliminar(ByVal entidad As ViGestionAsociado, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ViGestionAsociado).Eliminar
            Throw New NotImplementedException()
        End Function

        Public Function InsertarOModificar(ByVal entidad As ViGestionAsociado, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ViGestionAsociado).InsertarOModificar
            Throw New NotImplementedException()
        End Function

        Public Function VerificarExistencia(ByVal entidad As ViGestionAsociado) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ViGestionAsociado).VerificarExistencia
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace