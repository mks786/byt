Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacTarifa
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacTarifa
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="FacTarifa">FacTarifa a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacTarifa As FacTarifa) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacTarifaes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacTarifaes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacTarifa))
            Return New ComandoConsultarTodosFacTarifas()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un FacTarifa
        ' ''' </summary>
        ' ''' <param name="usuario">FacTarifa que se va a FacTarifa</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacTarifa(ByVal FacTarifa As FacTarifa) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacTarifa por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacTarifa As FacTarifa) As ComandoBase(Of FacTarifa)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="FacTarifa">FacTarifa a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacTarifa(ByVal FacTarifa As FacTarifa) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        Public Shared Function ObtenerComandoConsultarFacTarifasFiltro(ByVal FacTarifa As FacTarifa) As ComandoBase(Of IList(Of FacTarifa))
            Return New ComandoConsultarFacTarifasFiltro(FacTarifa)
        End Function
    End Class
End Namespace