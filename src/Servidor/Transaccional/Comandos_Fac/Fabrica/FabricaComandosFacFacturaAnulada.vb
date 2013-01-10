Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacFacturaAnulada
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacFacturaAnulada
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacFacturaAnulada">FacFacturaAnulada a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacFacturaAnulada As FacFacturaAnulada) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacFacturaAnulada(FacFacturaAnulada)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacFacturaAnuladaes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacFacturaAnuladaes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacFacturaAnulada))
            Return New ComandoConsultarTodosFacFacturaAnuladas()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacFacturaAnulada
        '' ''' </summary>
        '' ''' <param name="usuario">FacFacturaAnulada que se va a FacFacturaAnulada</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacFacturaAnulada(ByVal FacFacturaAnulada As FacFacturaAnulada) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacFacturaAnulada(FacFacturaAnulada)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacFacturaAnulada por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacFacturaAnulada As FacFacturaAnulada) As ComandoBase(Of FacFacturaAnulada)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacFacturaAnulada">FacFacturaAnulada a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacFacturaAnulada(ByVal FacFacturaAnulada As FacFacturaAnulada) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacFacturaAnulada(FacFacturaAnulada)
        End Function

        Public Shared Function ObtenerComandoConsultarFacFacturaAnuladasFiltro(ByVal FacFacturaAnulada As FacFacturaAnulada) As ComandoBase(Of IList(Of FacFacturaAnulada))
            Return New ComandoConsultarFacFacturaAnuladasFiltro(FacFacturaAnulada)
        End Function

    End Class
End Namespace