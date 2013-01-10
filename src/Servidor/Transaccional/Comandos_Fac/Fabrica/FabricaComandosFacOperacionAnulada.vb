Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacOperacionAnulada
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacOperacionAnulada
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacOperacionAnulada">FacOperacionAnulada a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacOperacionAnulada As FacOperacionAnulada) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacOperacionAnulada(FacOperacionAnulada)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacOperacionAnuladaes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacOperacionAnuladaes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacOperacionAnulada))
            Return New ComandoConsultarTodosFacOperacionAnuladas()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacOperacionAnulada
        '' ''' </summary>
        '' ''' <param name="usuario">FacOperacionAnulada que se va a FacOperacionAnulada</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacOperacionAnulada(ByVal FacOperacionAnulada As FacOperacionAnulada) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacOperacionAnulada(FacOperacionAnulada)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacOperacionAnulada por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacOperacionAnulada As FacOperacionAnulada) As ComandoBase(Of FacOperacionAnulada)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacOperacionAnulada">FacOperacionAnulada a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacOperacionAnulada(ByVal FacOperacionAnulada As FacOperacionAnulada) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacOperacionAnulada(FacOperacionAnulada)
        End Function

        Public Shared Function ObtenerComandoConsultarFacOperacionAnuladasFiltro(ByVal FacOperacionAnulada As FacOperacionAnulada) As ComandoBase(Of IList(Of FacOperacionAnulada))
            Return New ComandoConsultarFacOperacionAnuladasFiltro(FacOperacionAnulada)
        End Function

    End Class
End Namespace