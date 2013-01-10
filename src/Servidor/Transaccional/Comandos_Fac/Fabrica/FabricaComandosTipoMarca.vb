Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosTipoMarca
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosTipoMarca
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="TipoMarca">TipoMarca a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal TipoMarca As TipoMarca) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarTipoMarca(TipoMarca)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los TipoMarcaes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los TipoMarcaes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of TipoMarca))
            Return New ComandoConsultarTodosTipoMarcas()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un TipoMarca
        ' ''' </summary>
        ' ''' <param name="usuario">TipoMarca que se va a TipoMarca</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarTipoMarca(ByVal TipoMarca As TipoMarca) As ComandoBase(Of Boolean)
            Return New ComandoEliminarTipoMarca(TipoMarca)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un TipoMarca por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal TipoMarca As TipoMarca) As ComandoBase(Of TipoMarca)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="TipoMarca">TipoMarca a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaTipoMarca(ByVal TipoMarca As TipoMarca) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaTipoMarca(TipoMarca)
        End Function

        Public Shared Function ObtenerComandoConsultarTipoMarcasFiltro(ByVal TipoMarca As TipoMarca) As ComandoBase(Of IList(Of TipoMarca))
            Return New ComandoConsultarTipoMarcasFiltro(TipoMarca)
        End Function
    End Class
End Namespace