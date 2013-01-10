Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacOperacionDetalle
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacOperacionDetalle
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacOperacionDetalle">FacOperacionDetalle a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacOperacionDetalle As FacOperacionDetalle) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacOperacionDetalle(FacOperacionDetalle)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacOperacionDetallees
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacOperacionDetallees</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacOperacionDetalle))
            Return New ComandoConsultarTodosFacOperacionDetalles()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacOperacionDetalle
        '' ''' </summary>
        '' ''' <param name="usuario">FacOperacionDetalle que se va a FacOperacionDetalle</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacOperacionDetalle(ByVal FacOperacionDetalle As FacOperacionDetalle) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacOperacionDetalle(FacOperacionDetalle)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacOperacionDetalle por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacOperacionDetalle As FacOperacionDetalle) As ComandoBase(Of FacOperacionDetalle)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacOperacionDetalle">FacOperacionDetalle a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacOperacionDetalle(ByVal FacOperacionDetalle As FacOperacionDetalle) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacOperacionDetalle(FacOperacionDetalle)
        End Function

        Public Shared Function ObtenerComandoConsultarFacOperacionDetallesFiltro(ByVal FacOperacionDetalle As FacOperacionDetalle) As ComandoBase(Of IList(Of FacOperacionDetalle))
            Return New ComandoConsultarFacOperacionDetallesFiltro(FacOperacionDetalle)
        End Function

    End Class
End Namespace