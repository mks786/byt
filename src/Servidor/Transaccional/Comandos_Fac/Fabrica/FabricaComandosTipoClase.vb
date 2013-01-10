Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosTipoClase
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosTipoClase
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="TipoClase">TipoClase a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal TipoClase As TipoClase) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarTipoClase(TipoClase)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los TipoClasees
        ''' </summary>
        ''' <returns>El Comando para consultar todos los TipoClasees</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of TipoClase))
            Return New ComandoConsultarTodosTipoClases()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un TipoClase
        ' ''' </summary>
        ' ''' <param name="usuario">TipoClase que se va a TipoClase</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarTipoClase(ByVal TipoClase As TipoClase) As ComandoBase(Of Boolean)
            Return New ComandoEliminarTipoClase(TipoClase)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un TipoClase por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal TipoClase As TipoClase) As ComandoBase(Of TipoClase)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="TipoClase">TipoClase a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaTipoClase(ByVal TipoClase As TipoClase) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaTipoClase(TipoClase)
        End Function

        Public Shared Function ObtenerComandoConsultarTipoClasesFiltro(ByVal TipoClase As TipoClase) As ComandoBase(Of IList(Of TipoClase))
            Return New ComandoConsultarTipoClasesFiltro(TipoClase)
        End Function
    End Class
End Namespace