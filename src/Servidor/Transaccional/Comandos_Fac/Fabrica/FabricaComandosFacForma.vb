Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacForma
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacForma
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacForma">FacForma a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacForma As FacForma) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacForma(FacForma)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacFormaes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacFormaes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacForma))
            Return New ComandoConsultarTodosFacFormas()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacForma
        '' ''' </summary>
        '' ''' <param name="usuario">FacForma que se va a FacForma</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacForma(ByVal FacForma As FacForma) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacForma(FacForma)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacForma por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacForma As FacForma) As ComandoBase(Of FacForma)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacForma">FacForma a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacForma(ByVal FacForma As FacForma) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacForma(FacForma)
        End Function

        Public Shared Function ObtenerComandoConsultarFacFormasFiltro(ByVal FacForma As FacForma) As ComandoBase(Of IList(Of FacForma))
            Return New ComandoConsultarFacFormasFiltro(FacForma)
        End Function

    End Class
End Namespace