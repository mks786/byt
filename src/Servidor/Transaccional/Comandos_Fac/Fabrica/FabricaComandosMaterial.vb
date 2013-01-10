Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosMaterial
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosMaterial
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="Material">Material a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal Material As Material) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarMaterial(Material)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los Materiales
        ''' </summary>
        ''' <returns>El Comando para consultar todos los Materiales</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of Material))
            Return New ComandoConsultarTodosMateriales()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un Material
        ' ''' </summary>
        ' ''' <param name="usuario">Material que se va a Material</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarMaterial(ByVal Material As Material) As ComandoBase(Of Boolean)
            Return New ComandoEliminarMaterial(Material)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un Material por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal Material As Material) As ComandoBase(Of Material)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="Material">Material a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaMaterial(ByVal Material As Material) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaMaterial(Material)
        End Function
    End Class
End Namespace