Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacOperacionDetaTmProforma
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacOperacionDetaTmProforma
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacOperacionDetaTmProforma">FacOperacionDetaTmProforma a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacOperacionDetaTmProforma As FacOperacionDetaTmProforma) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacOperacionDetaTmProforma(FacOperacionDetaTmProforma)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacOperacionDetaTmProformaes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacOperacionDetaTmProformaes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacOperacionDetaTmProforma))
            Return New ComandoConsultarTodosFacOperacionDetaTmProformas()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacOperacionDetaTmProforma
        '' ''' </summary>
        '' ''' <param name="usuario">FacOperacionDetaTmProforma que se va a FacOperacionDetaTmProforma</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacOperacionDetaTmProforma(ByVal FacOperacionDetaTmProforma As FacOperacionDetaTmProforma) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacOperacionDetaTmProforma(FacOperacionDetaTmProforma)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacOperacionDetaTmProforma por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacOperacionDetaTmProforma As FacOperacionDetaTmProforma) As ComandoBase(Of FacOperacionDetaTmProforma)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacOperacionDetaTmProforma">FacOperacionDetaTmProforma a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacOperacionDetaTmProforma(ByVal FacOperacionDetaTmProforma As FacOperacionDetaTmProforma) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacOperacionDetaTmProforma(FacOperacionDetaTmProforma)
        End Function

        Public Shared Function ObtenerComandoConsultarFacOperacionDetaTmProformasFiltro(ByVal FacOperacionDetaTmProforma As FacOperacionDetaTmProforma) As ComandoBase(Of IList(Of FacOperacionDetaTmProforma))
            Return New ComandoConsultarFacOperacionDetaTmProformasFiltro(FacOperacionDetaTmProforma)
        End Function

    End Class
End Namespace