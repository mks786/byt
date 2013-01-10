Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.LogicaNegocio.Controladores
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports System.Runtime.Remoting

Namespace Implementacion
    Public Class ConceptoGestionServicios
        Inherits MarshalByRefObject
        Implements IConceptoGestionServicios

        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Método que obtiene todos los ConceptoGestiones
        ''' </summary>
        ''' <returns>Todos los ConceptoGestiones</returns>
        Public Function ConsultarTodos() As IList(Of ObjetosComunes.Entidades.ConceptoGestion) Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.ConceptoGestion).ConsultarTodos
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim ConceptoGestions As IList(Of ObjetosComunes.Entidades.ConceptoGestion) = ControladorConceptoGestion.ConsultarTodos()

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return ConceptoGestions
        End Function


        Public Function ConsultarPorId(ByVal entidad As ObjetosComunes.Entidades.ConceptoGestion) As ObjetosComunes.Entidades.ConceptoGestion Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.ConceptoGestion).ConsultarPorId
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que inserta o modifica un país
        ''' </summary>
        ''' <param name="entidad">País a insertar o modificar</param>
        ''' <param name="hash">hash del usuario logerad</param>
        ''' <returns></returns>
        Public Function InsertarOModificar(ByVal entidad As ObjetosComunes.Entidades.ConceptoGestion, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.ConceptoGestion).InsertarOModificar
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim exitoso As Boolean = ControladorConceptoGestion.InsertarOModificar(entidad, hash)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return exitoso
        End Function

        ''' <summary>
        ''' Método que elimina un ConceptoGestion
        ''' </summary>
        ''' <param name="entidad">País a eliminar</param>
        ''' <param name="hash">Hash del usuario logeado</param>
        ''' <returns></returns>
        Public Function Eliminar(ByVal entidad As ObjetosComunes.Entidades.ConceptoGestion, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.ConceptoGestion).Eliminar
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim exitoso As Boolean = ControladorConceptoGestion.Eliminar(entidad, hash)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return exitoso
        End Function


        Public Function VerificarExistencia(ByVal entidad As ObjetosComunes.Entidades.ConceptoGestion) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.ConceptoGestion).VerificarExistencia
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim exitoso As Boolean = ControladorConceptoGestion.VerificarExistencia(entidad)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return exitoso
        End Function
    End Class
End Namespace