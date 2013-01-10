Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacFactuDetaProforma
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacFactuDetaProforma
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacFactuDetaProforma">FacFactuDetaProforma a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacFactuDetaProforma As FacFactuDetaProforma) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacFactuDetaProforma(FacFactuDetaProforma)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacFactuDetaProformaes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacFactuDetaProformaes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacFactuDetaProforma))
            Return New ComandoConsultarTodosFacFactuDetaProformas()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacFactuDetaProforma
        '' ''' </summary>
        '' ''' <param name="usuario">FacFactuDetaProforma que se va a FacFactuDetaProforma</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacFactuDetaProforma(ByVal FacFactuDetaProforma As FacFactuDetaProforma) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacFactuDetaProforma(FacFactuDetaProforma)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacFactuDetaProforma por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacFactuDetaProforma As FacFactuDetaProforma) As ComandoBase(Of FacFactuDetaProforma)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacFactuDetaProforma">FacFactuDetaProforma a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacFactuDetaProforma(ByVal FacFactuDetaProforma As FacFactuDetaProforma) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacFactuDetaProforma(FacFactuDetaProforma)
        End Function

        Public Shared Function ObtenerComandoConsultarFacFactuDetaProformasFiltro(ByVal FacFactuDetaProforma As FacFactuDetaProforma) As ComandoBase(Of IList(Of FacFactuDetaProforma))
            Return New ComandoConsultarFacFactuDetaProformasFiltro(FacFactuDetaProforma)
        End Function

    End Class
End Namespace