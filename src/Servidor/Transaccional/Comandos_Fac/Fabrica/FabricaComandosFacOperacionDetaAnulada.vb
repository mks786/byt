Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacOperacionDetaAnulada
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacOperacionDetaAnulada
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacOperacionDetaAnulada">FacOperacionDetaAnulada a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacOperacionDetaAnulada As FacOperacionDetaAnulada) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacOperacionDetaAnulada(FacOperacionDetaAnulada)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacOperacionDetaAnuladaes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacOperacionDetaAnuladaes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacOperacionDetaAnulada))
            Return New ComandoConsultarTodosFacOperacionDetaAnuladas()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacOperacionDetaAnulada
        '' ''' </summary>
        '' ''' <param name="usuario">FacOperacionDetaAnulada que se va a FacOperacionDetaAnulada</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacOperacionDetaAnulada(ByVal FacOperacionDetaAnulada As FacOperacionDetaAnulada) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacOperacionDetaAnulada(FacOperacionDetaAnulada)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacOperacionDetaAnulada por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacOperacionDetaAnulada As FacOperacionDetaAnulada) As ComandoBase(Of FacOperacionDetaAnulada)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacOperacionDetaAnulada">FacOperacionDetaAnulada a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacOperacionDetaAnulada(ByVal FacOperacionDetaAnulada As FacOperacionDetaAnulada) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacOperacionDetaAnulada(FacOperacionDetaAnulada)
        End Function

        Public Shared Function ObtenerComandoConsultarFacOperacionDetaAnuladasFiltro(ByVal FacOperacionDetaAnulada As FacOperacionDetaAnulada) As ComandoBase(Of IList(Of FacOperacionDetaAnulada))
            Return New ComandoConsultarFacOperacionDetaAnuladasFiltro(FacOperacionDetaAnulada)
        End Function

    End Class
End Namespace