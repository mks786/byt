Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosTipoPatente
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosTipoPatente
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="TipoPatente">TipoPatente a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal TipoPatente As TipoPatente) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarTipoPatente(TipoPatente)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los TipoPatentees
        ''' </summary>
        ''' <returns>El Comando para consultar todos los TipoPatentees</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of TipoPatente))
            Return New ComandoConsultarTodosTipoPatentes()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un TipoPatente
        ' ''' </summary>
        ' ''' <param name="usuario">TipoPatente que se va a TipoPatente</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarTipoPatente(ByVal TipoPatente As TipoPatente) As ComandoBase(Of Boolean)
            Return New ComandoEliminarTipoPatente(TipoPatente)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un TipoPatente por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal TipoPatente As TipoPatente) As ComandoBase(Of TipoPatente)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="TipoPatente">TipoPatente a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaTipoPatente(ByVal TipoPatente As TipoPatente) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaTipoPatente(TipoPatente)
        End Function

        Public Shared Function ObtenerComandoConsultarTipoPatentesFiltro(ByVal TipoPatente As TipoPatente) As ComandoBase(Of IList(Of TipoPatente))
            Return New ComandoConsultarTipoPatentesFiltro(TipoPatente)
        End Function
    End Class
End Namespace