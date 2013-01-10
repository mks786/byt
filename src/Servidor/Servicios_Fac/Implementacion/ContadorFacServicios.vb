Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.LogicaNegocio.Controladores
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports System.Runtime.Remoting

Namespace Implementacion
    Public Class ContadorFacServicios
        Inherits MarshalByRefObject
        Implements IContadorFacServicios

        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ' ''' <summary>
        ' ''' Método que obtiene todos los ContadorFaces
        ' ''' </summary>
        ' ''' <returns>Todos los ContadorFaces</returns>
        'Public Function ConsultarTodos() As IList(Of ContadorFac) Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ContadorFac).ConsultarTodos
        '    '#Region "trace"
        '    If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
        '        logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
        '    End If
        '    '#End Region

        '    Dim ContadorFacs As IList(Of ContadorFac) = ControladorContadorFac.ConsultarTodos()

        '    '#Region "trace"
        '    If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
        '        logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
        '    End If
        '    '#End Region

        '    Return ContadorFacs
        'End Function


        Public Function ConsultarPorId(ByVal entidad As ContadorFac) As ContadorFac Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ContadorFac).ConsultarPorId
            'Throw New NotImplementedException()

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim ContadorFac As ContadorFac = ControladorContadorFac.ConsultarPorId(entidad)            

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return ContadorFac
        End Function

        ''' <summary>
        ''' Método que inserta o modifica un país
        ''' </summary>
        ''' <param name="entidad">País a insertar o modificar</param>
        ''' <param name="hash">hash del usuario logerad</param>
        ''' <returns></returns>
        Public Function InsertarOModificar(ByVal entidad As ContadorFac, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ContadorFac).InsertarOModificar
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim exitoso As Boolean = ControladorContadorFac.InsertarOModificar(entidad, hash)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return exitoso
        End Function

        ' ''' <summary>
        ' ''' Método que elimina un ContadorFac
        ' ''' </summary>
        ' ''' <param name="entidad">País a eliminar</param>
        ' ''' <param name="hash">Hash del usuario logeado</param>
        ' ''' <returns></returns>
        'Public Function Eliminar(ByVal entidad As ContadorFac, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ContadorFac).Eliminar
        '    '#Region "trace"
        '    If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
        '        logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
        '    End If
        '    '#End Region

        '    Dim exitoso As Boolean = ControladorContadorFac.Eliminar(entidad, hash)

        '    '#Region "trace"
        '    If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
        '        logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
        '    End If
        '    '#End Region

        '    Return exitoso
        'End Function


        'Public Function VerificarExistencia(ByVal entidad As ContadorFac) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ContadorFac).VerificarExistencia
        '    '#Region "trace"
        '    If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
        '        logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
        '    End If
        '    '#End Region

        '    Dim exitoso As Boolean = ControladorContadorFac.VerificarExistencia(entidad)

        '    '#Region "trace"
        '    If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
        '        logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
        '    End If
        '    '#End Region

        '    Return exitoso
        'End Function

        Public Function ConsultarTodos() As System.Collections.Generic.IList(Of Trascend.Bolet.ObjetosComunes.Entidades.ContadorFac) Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.ContadorFac).ConsultarTodos
            Throw New NotImplementedException()
        End Function

        Public Function Eliminar(ByVal entidad As Trascend.Bolet.ObjetosComunes.Entidades.ContadorFac, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.ContadorFac).Eliminar
            Throw New NotImplementedException()
        End Function

        Public Function VerificarExistencia(ByVal entidad As Trascend.Bolet.ObjetosComunes.Entidades.ContadorFac) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.ContadorFac).VerificarExistencia
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace