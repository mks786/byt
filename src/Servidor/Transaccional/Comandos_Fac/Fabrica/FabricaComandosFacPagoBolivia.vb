Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacPagoBolivia
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacPagoBolivia
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacPagoBolivia">FacPagoBolivia a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacPagoBolivia As FacPagoBolivia) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacPagoBolivia(FacPagoBolivia)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacPagoBoliviaes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacPagoBoliviaes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacPagoBolivia))
            Return New ComandoConsultarTodosFacPagoBolivias()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacPagoBolivia
        '' ''' </summary>
        '' ''' <param name="usuario">FacPagoBolivia que se va a FacPagoBolivia</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacPagoBolivia(ByVal FacPagoBolivia As FacPagoBolivia) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacPagoBolivia(FacPagoBolivia)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacPagoBolivia por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacPagoBolivia As FacPagoBolivia) As ComandoBase(Of FacPagoBolivia)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacPagoBolivia">FacPagoBolivia a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacPagoBolivia(ByVal FacPagoBolivia As FacPagoBolivia) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacPagoBolivia(FacPagoBolivia)
        End Function

        Public Shared Function ObtenerComandoConsultarFacPagoBoliviasFiltro(ByVal FacPagoBolivia As FacPagoBolivia) As ComandoBase(Of IList(Of FacPagoBolivia))
            Return New ComandoConsultarFacPagoBoliviasFiltro(FacPagoBolivia)
        End Function

    End Class
End Namespace