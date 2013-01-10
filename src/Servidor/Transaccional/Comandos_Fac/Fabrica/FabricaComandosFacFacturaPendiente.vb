Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacFacturaPendiente
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacFacturaPendiente
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacFacturaPendiente">FacFacturaPendiente a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacFacturaPendiente As FacFacturaPendiente) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacFacturaPendientees
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacFacturaPendientees</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacFacturaPendiente))
            Return New ComandoConsultarTodosFacFacturaPendientes()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacFacturaPendiente
        '' ''' </summary>
        '' ''' <param name="usuario">FacFacturaPendiente que se va a FacFacturaPendiente</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacFacturaPendiente(ByVal FacFacturaPendiente As FacFacturaPendiente) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacFacturaPendiente por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacFacturaPendiente As FacFacturaPendiente) As ComandoBase(Of FacFacturaPendiente)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacFacturaPendiente">FacFacturaPendiente a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacFacturaPendiente(ByVal FacFacturaPendiente As FacFacturaPendiente) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        Public Shared Function ObtenerComandoConsultarFacFacturaPendientesFiltro(ByVal FacFacturaPendiente As FacFacturaPendiente) As ComandoBase(Of IList(Of FacFacturaPendiente))
            Return New ComandoConsultarFacFacturaPendientesFiltro(FacFacturaPendiente)
        End Function

    End Class
End Namespace