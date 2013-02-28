Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacVistaFacturaServicio
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacVistaFacturaServicio
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacVistaFacturaServicio">FacVistaFacturaServicio a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacVistaFacturaServicio As FacVistaFacturaServicio) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacVistaFacturaServicioes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacVistaFacturaServicioes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacVistaFacturaServicio))
            Return New ComandoConsultarTodosFacVistaFacturaServicios()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacVistaFacturaServicio
        '' ''' </summary>
        '' ''' <param name="usuario">FacVistaFacturaServicio que se va a FacVistaFacturaServicio</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacVistaFacturaServicio(ByVal FacVistaFacturaServicio As FacVistaFacturaServicio) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacVistaFacturaServicio por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacVistaFacturaServicio As FacVistaFacturaServicio) As ComandoBase(Of FacVistaFacturaServicio)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacVistaFacturaServicio">FacVistaFacturaServicio a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacVistaFacturaServicio(ByVal FacVistaFacturaServicio As FacVistaFacturaServicio) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        Public Shared Function ObtenerComandoConsultarFacVistaFacturaServiciosFiltro(ByVal FacVistaFacturaServicio As FacVistaFacturaServicio) As ComandoBase(Of IList(Of FacVistaFacturaServicio))
            Return New ComandoConsultarFacVistaFacturaServiciosFiltro(FacVistaFacturaServicio)
        End Function

    End Class
End Namespace