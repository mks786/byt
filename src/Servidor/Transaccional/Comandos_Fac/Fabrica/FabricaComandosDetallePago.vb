Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosDetallePago
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosDetallePago
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="DetallePago">DetallePago a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal DetallePago As DetallePago) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarDetallePago(DetallePago)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los DetallePagoes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los DetallePagoes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of DetallePago))
            Return New ComandoConsultarTodosDetallePagos()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un DetallePago
        ' ''' </summary>
        ' ''' <param name="usuario">DetallePago que se va a DetallePago</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarDetallePago(ByVal DetallePago As DetallePago) As ComandoBase(Of Boolean)
            Return New ComandoEliminarDetallePago(DetallePago)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un DetallePago por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal DetallePago As DetallePago) As ComandoBase(Of DetallePago)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="DetallePago">DetallePago a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaDetallePago(ByVal DetallePago As DetallePago) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaDetallePago(DetallePago)
        End Function
    End Class
End Namespace