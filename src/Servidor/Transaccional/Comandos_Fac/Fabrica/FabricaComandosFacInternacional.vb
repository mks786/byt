Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacInternacional
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacInternacional
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacInternacional">FacInternacional a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacInternacional As FacInternacional) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacInternacional(FacInternacional)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacInternacionales
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacInternacionales</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacInternacional))
            Return New ComandoConsultarTodosFacInternacionales()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacInternacional
        '' ''' </summary>
        '' ''' <param name="usuario">FacInternacional que se va a FacInternacional</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacInternacional(ByVal FacInternacional As FacInternacional) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacInternacional(FacInternacional)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacInternacional por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacInternacional As FacInternacional) As ComandoBase(Of FacInternacional)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacInternacional">FacInternacional a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacInternacional(ByVal FacInternacional As FacInternacional) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacInternacional(FacInternacional)
        End Function

        Public Shared Function ObtenerComandoConsultarFacInternacionalesFiltro(ByVal FacInternacional As FacInternacional) As ComandoBase(Of IList(Of FacInternacional))
            Return New ComandoConsultarFacInternacionalesFiltro(FacInternacional)
        End Function

    End Class
End Namespace