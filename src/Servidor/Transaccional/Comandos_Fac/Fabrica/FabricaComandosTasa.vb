Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosTasa
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosTasa
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="Tasa">Tasa a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal tasa As Tasa) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarTasa(tasa)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los Tasaes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los Tasaes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of Tasa))
            Return New ComandoConsultarTodosTasas()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un Tasa
        ' ''' </summary>
        ' ''' <param name="usuario">Tasa que se va a Tasa</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarTasa(ByVal tasa As Tasa) As ComandoBase(Of Boolean)
            Return New ComandoEliminarTasa(tasa)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un Tasa por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal tasa As Tasa) As ComandoBase(Of Tasa)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="Tasa">Tasa a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaTasa(ByVal tasa As Tasa) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaTasa(tasa)
        End Function

        Public Shared Function ObtenerComandoConsultarTasasFiltro(ByVal Tasa As Tasa) As ComandoBase(Of IList(Of Tasa))
            Return New ComandoConsultarTasasFiltro(Tasa)
        End Function
    End Class
End Namespace