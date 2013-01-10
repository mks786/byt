Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacInternacionalAnulada
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacInternacionalAnulada
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacInternacionalAnulada">FacInternacionalAnulada a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacInternacionalAnulada As FacInternacionalAnulada) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacInternacionalAnulada(FacInternacionalAnulada)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacInternacionalAnuladaes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacInternacionalAnuladaes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacInternacionalAnulada))
            Return New ComandoConsultarTodosFacInternacionalAnuladaes()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacInternacionalAnulada
        '' ''' </summary>
        '' ''' <param name="usuario">FacInternacionalAnulada que se va a FacInternacionalAnulada</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacInternacionalAnulada(ByVal FacInternacionalAnulada As FacInternacionalAnulada) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacInternacionalAnulada(FacInternacionalAnulada)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacInternacionalAnulada por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacInternacionalAnulada As FacInternacionalAnulada) As ComandoBase(Of FacInternacionalAnulada)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacInternacionalAnulada">FacInternacionalAnulada a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacInternacionalAnulada(ByVal FacInternacionalAnulada As FacInternacionalAnulada) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacInternacionalAnulada(FacInternacionalAnulada)
        End Function

        Public Shared Function ObtenerComandoConsultarFacInternacionalAnuladaesFiltro(ByVal FacInternacionalAnulada As FacInternacionalAnulada) As ComandoBase(Of IList(Of FacInternacionalAnulada))
            Return New ComandoConsultarFacInternacionalAnuladaesFiltro(FacInternacionalAnulada)
        End Function

    End Class
End Namespace