Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.LogicaNegocio.Controladores
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades

Namespace Implementacion
    Public Class DepartamentoServicios
        Inherits MarshalByRefObject
        Implements IDepartamentoServicios

        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()


        Public Function ConsultarPorId(ByVal entidad As ObjetosComunes.Entidades.Departamento) As ObjetosComunes.Entidades.Departamento Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.Departamento).ConsultarPorId
            Throw New NotImplementedException()
        End Function

        Public Function ConsultarTodos() As System.Collections.Generic.IList(Of ObjetosComunes.Entidades.Departamento) Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.Departamento).ConsultarTodos
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim departamentos As IList(Of Departamento) = ControladorDepartamento.ConsultarTodos()

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return departamentos
        End Function

        Public Function Eliminar(ByVal entidad As ObjetosComunes.Entidades.Departamento, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.Departamento).Eliminar
            Throw New NotImplementedException()
        End Function

        Public Function InsertarOModificar(ByVal entidad As ObjetosComunes.Entidades.Departamento, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.Departamento).InsertarOModificar
            Throw New NotImplementedException()
        End Function

        Public Function VerificarExistencia(ByVal entidad As ObjetosComunes.Entidades.Departamento) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.Departamento).VerificarExistencia
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace