Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacOperacionDetalleTm
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacOperacionDetalleTm
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacOperacionDetalleTm">FacOperacionDetalleTm a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacOperacionDetalleTm As FacOperacionDetalleTm) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacOperacionDetalleTm(FacOperacionDetalleTm)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacOperacionDetalleTmes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacOperacionDetalleTmes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacOperacionDetalleTm))
            Return New ComandoConsultarTodosFacOperacionDetalleTms()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacOperacionDetalleTm
        '' ''' </summary>
        '' ''' <param name="usuario">FacOperacionDetalleTm que se va a FacOperacionDetalleTm</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacOperacionDetalleTm(ByVal FacOperacionDetalleTm As FacOperacionDetalleTm) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacOperacionDetalleTm(FacOperacionDetalleTm)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacOperacionDetalleTm por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacOperacionDetalleTm As FacOperacionDetalleTm) As ComandoBase(Of FacOperacionDetalleTm)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacOperacionDetalleTm">FacOperacionDetalleTm a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacOperacionDetalleTm(ByVal FacOperacionDetalleTm As FacOperacionDetalleTm) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacOperacionDetalleTm(FacOperacionDetalleTm)
        End Function

        Public Shared Function ObtenerComandoConsultarFacOperacionDetalleTmsFiltro(ByVal FacOperacionDetalleTm As FacOperacionDetalleTm) As ComandoBase(Of IList(Of FacOperacionDetalleTm))
            Return New ComandoConsultarFacOperacionDetalleTmsFiltro(FacOperacionDetalleTm)
        End Function

    End Class
End Namespace