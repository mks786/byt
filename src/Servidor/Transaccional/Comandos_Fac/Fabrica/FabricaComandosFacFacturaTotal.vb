Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacFacturaTotal
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacFacturaTotal
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacFacturaTotal">FacFacturaTotal a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacFacturaTotal As FacFacturaTotal) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacFacturaTotales
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacFacturaTotales</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacFacturaTotal))
            Return New ComandoConsultarTodosFacFacturaTotals()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacFacturaTotal
        '' ''' </summary>
        '' ''' <param name="usuario">FacFacturaTotal que se va a FacFacturaTotal</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacFacturaTotal(ByVal FacFacturaTotal As FacFacturaTotal) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacFacturaTotal por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacFacturaTotal As FacFacturaTotal) As ComandoBase(Of FacFacturaTotal)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacFacturaTotal">FacFacturaTotal a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacFacturaTotal(ByVal FacFacturaTotal As FacFacturaTotal) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        Public Shared Function ObtenerComandoConsultarFacFacturaTotalsFiltro(ByVal FacFacturaTotal As FacFacturaTotal) As ComandoBase(Of IList(Of FacFacturaTotal))
            Return New ComandoConsultarFacFacturaTotalsFiltro(FacFacturaTotal)
        End Function

    End Class
End Namespace