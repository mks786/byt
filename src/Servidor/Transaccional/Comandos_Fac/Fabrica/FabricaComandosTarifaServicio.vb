Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosTarifaServicio
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosTarifaServicio
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="TarifaServicio">TarifaServicio a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal TarifaServicio As TarifaServicio) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarTarifaServicio(TarifaServicio)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los TarifaServicioes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los TarifaServicioes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of TarifaServicio))
            Return New ComandoConsultarTodosTarifaServicios()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un TarifaServicio
        ' ''' </summary>
        ' ''' <param name="usuario">TarifaServicio que se va a TarifaServicio</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarTarifaServicio(ByVal TarifaServicio As TarifaServicio) As ComandoBase(Of Boolean)
            Return New ComandoEliminarTarifaServicio(TarifaServicio)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un TarifaServicio por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal TarifaServicio As TarifaServicio) As ComandoBase(Of TarifaServicio)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="TarifaServicio">TarifaServicio a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaTarifaServicio(ByVal TarifaServicio As TarifaServicio) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaTarifaServicio(TarifaServicio)
        End Function

        Public Shared Function ObtenerComandoConsultarTarifaServiciosFiltro(ByVal TarifaServicio As TarifaServicio) As ComandoBase(Of IList(Of TarifaServicio))
            Return New ComandoConsultarTarifaServiciosFiltro(TarifaServicio)
        End Function
    End Class
End Namespace