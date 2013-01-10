Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.LogicaNegocio.Controladores
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports System.Runtime.Remoting

Namespace Implementacion
    Public Class FacContadorProServicios
        Inherits MarshalByRefObject
        Implements IFacContadorProServicios

        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ' ''' <summary>
        ' ''' Método que obtiene todos los FacContadorProes
        ' ''' </summary>
        ' ''' <returns>Todos los FacContadorProes</returns>
        'Public Function ConsultarTodos() As IList(Of FacContadorPro) Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacContadorPro).ConsultarTodos
        '    '#Region "trace"
        '    If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
        '        logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
        '    End If
        '    '#End Region

        '    Dim FacContadorPros As IList(Of FacContadorPro) = ControladorFacContadorPro.ConsultarTodos()

        '    '#Region "trace"
        '    If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
        '        logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
        '    End If
        '    '#End Region

        '    Return FacContadorPros
        'End Function


        Public Function ConsultarPorId(ByVal entidad As FacContadorPro) As FacContadorPro Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacContadorPro).ConsultarPorId
            'Throw New NotImplementedException()

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim FacContadorPro As FacContadorPro = ControladorFacContadorPro.ConsultarPorId(entidad)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return FacContadorPro
        End Function

        ''' <summary>
        ''' Método que inserta o modifica un país
        ''' </summary>
        ''' <param name="entidad">País a insertar o modificar</param>
        ''' <param name="hash">hash del usuario logerad</param>
        ''' <returns></returns>
        Public Function InsertarOModificar(ByVal entidad As FacContadorPro, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacContadorPro).InsertarOModificar
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim exitoso As Boolean = ControladorFacContadorPro.InsertarOModificar(entidad, hash)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return exitoso
        End Function

        ' ''' <summary>
        ' ''' Método que elimina un FacContadorPro
        ' ''' </summary>
        ' ''' <param name="entidad">País a eliminar</param>
        ' ''' <param name="hash">Hash del usuario logeado</param>
        ' ''' <returns></returns>
        'Public Function Eliminar(ByVal entidad As FacContadorPro, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacContadorPro).Eliminar
        '    '#Region "trace"
        '    If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
        '        logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
        '    End If
        '    '#End Region

        '    Dim exitoso As Boolean = ControladorFacContadorPro.Eliminar(entidad, hash)

        '    '#Region "trace"
        '    If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
        '        logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
        '    End If
        '    '#End Region

        '    Return exitoso
        'End Function


        'Public Function VerificarExistencia(ByVal entidad As FacContadorPro) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacContadorPro).VerificarExistencia
        '    '#Region "trace"
        '    If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
        '        logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
        '    End If
        '    '#End Region

        '    Dim exitoso As Boolean = ControladorFacContadorPro.VerificarExistencia(entidad)

        '    '#Region "trace"
        '    If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
        '        logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
        '    End If
        '    '#End Region

        '    Return exitoso
        'End Function

        Public Function ConsultarTodos() As System.Collections.Generic.IList(Of Trascend.Bolet.ObjetosComunes.Entidades.FacContadorPro) Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacContadorPro).ConsultarTodos
            Throw New NotImplementedException()
        End Function

        Public Function Eliminar(ByVal entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacContadorPro, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacContadorPro).Eliminar
            Throw New NotImplementedException()
        End Function

        Public Function VerificarExistencia(ByVal entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacContadorPro) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacContadorPro).VerificarExistencia
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace