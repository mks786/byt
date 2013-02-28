Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacFacturaPendienteConGru
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacFacturaPendienteConGru
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacFacturaPendienteConGru">FacFacturaPendienteConGru a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacFacturaPendienteConGru As FacFacturaPendienteConGru) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacFacturaPendienteConGrues
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacFacturaPendienteConGrues</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacFacturaPendienteConGru))
            Return New ComandoConsultarTodosFacFacturaPendienteConGrus()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacFacturaPendienteConGru
        '' ''' </summary>
        '' ''' <param name="usuario">FacFacturaPendienteConGru que se va a FacFacturaPendienteConGru</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacFacturaPendienteConGru(ByVal FacFacturaPendienteConGru As FacFacturaPendienteConGru) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacFacturaPendienteConGru por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacFacturaPendienteConGru As FacFacturaPendienteConGru) As ComandoBase(Of FacFacturaPendienteConGru)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacFacturaPendienteConGru">FacFacturaPendienteConGru a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacFacturaPendienteConGru(ByVal FacFacturaPendienteConGru As FacFacturaPendienteConGru) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        Public Shared Function ObtenerComandoConsultarFacFacturaPendienteConGrusFiltro(ByVal FacFacturaPendienteConGru As FacFacturaPendienteConGru) As ComandoBase(Of IList(Of FacFacturaPendienteConGru))
            Return New ComandoConsultarFacFacturaPendienteConGrusFiltro(FacFacturaPendienteConGru)
        End Function

    End Class
End Namespace