Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacFactuDetaAnulada
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacFactuDetaAnulada
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacFactuDetaAnulada">FacFactuDetaAnulada a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacFactuDetaAnulada As FacFactuDetaAnulada) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacFactuDetaAnulada(FacFactuDetaAnulada)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacFactuDetaAnuladaes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacFactuDetaAnuladaes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacFactuDetaAnulada))
            Return New ComandoConsultarTodosFacFactuDetaAnuladas()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacFactuDetaAnulada
        '' ''' </summary>
        '' ''' <param name="usuario">FacFactuDetaAnulada que se va a FacFactuDetaAnulada</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacFactuDetaAnulada(ByVal FacFactuDetaAnulada As FacFactuDetaAnulada) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacFactuDetaAnulada(FacFactuDetaAnulada)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacFactuDetaAnulada por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacFactuDetaAnulada As FacFactuDetaAnulada) As ComandoBase(Of FacFactuDetaAnulada)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacFactuDetaAnulada">FacFactuDetaAnulada a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacFactuDetaAnulada(ByVal FacFactuDetaAnulada As FacFactuDetaAnulada) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacFactuDetaAnulada(FacFactuDetaAnulada)
        End Function

        Public Shared Function ObtenerComandoConsultarFacFactuDetaAnuladasFiltro(ByVal FacFactuDetaAnulada As FacFactuDetaAnulada) As ComandoBase(Of IList(Of FacFactuDetaAnulada))
            Return New ComandoConsultarFacFactuDetaAnuladasFiltro(FacFactuDetaAnulada)
        End Function

    End Class
End Namespace