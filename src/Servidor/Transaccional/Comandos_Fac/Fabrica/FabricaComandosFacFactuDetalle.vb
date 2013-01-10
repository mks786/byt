Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacFactuDetalle
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacFactuDetalle
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacFactuDetalle">FacFactuDetalle a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacFactuDetalle As FacFactuDetalle) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacFactuDetalle(FacFactuDetalle)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacFactuDetallees
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacFactuDetallees</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacFactuDetalle))
            Return New ComandoConsultarTodosFacFactuDetalles()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacFactuDetalle
        '' ''' </summary>
        '' ''' <param name="usuario">FacFactuDetalle que se va a FacFactuDetalle</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacFactuDetalle(ByVal FacFactuDetalle As FacFactuDetalle) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacFactuDetalle(FacFactuDetalle)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacFactuDetalle por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacFactuDetalle As FacFactuDetalle) As ComandoBase(Of FacFactuDetalle)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacFactuDetalle">FacFactuDetalle a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacFactuDetalle(ByVal FacFactuDetalle As FacFactuDetalle) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacFactuDetalle(FacFactuDetalle)
        End Function

        Public Shared Function ObtenerComandoConsultarFacFactuDetallesFiltro(ByVal FacFactuDetalle As FacFactuDetalle) As ComandoBase(Of IList(Of FacFactuDetalle))
            Return New ComandoConsultarFacFactuDetallesFiltro(FacFactuDetalle)
        End Function

    End Class
End Namespace