Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacFacturaProforma
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacFacturaProforma
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacFacturaProforma">FacFacturaProforma a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacFacturaProforma As FacFacturaProforma) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacFacturaProforma(FacFacturaProforma)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacFacturaProformaes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacFacturaProformaes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacFacturaProforma))
            Return New ComandoConsultarTodosFacFacturaProformas()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacFacturaProforma
        '' ''' </summary>
        '' ''' <param name="usuario">FacFacturaProforma que se va a FacFacturaProforma</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacFacturaProforma(ByVal FacFacturaProforma As FacFacturaProforma) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacFacturaProforma(FacFacturaProforma)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacFacturaProforma por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacFacturaProforma As FacFacturaProforma) As ComandoBase(Of FacFacturaProforma)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacFacturaProforma">FacFacturaProforma a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacFacturaProforma(ByVal FacFacturaProforma As FacFacturaProforma) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacFacturaProforma(FacFacturaProforma)
        End Function

        Public Shared Function ObtenerComandoConsultarFacFacturaProformasFiltro(ByVal FacFacturaProforma As FacFacturaProforma) As ComandoBase(Of IList(Of FacFacturaProforma))
            Return New ComandoConsultarFacFacturaProformasFiltro(FacFacturaProforma)
        End Function

    End Class
End Namespace