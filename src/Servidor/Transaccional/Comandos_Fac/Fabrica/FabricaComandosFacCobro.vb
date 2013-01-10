Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacCobro
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacCobro
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacCobro">FacCobro a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacCobro As FacCobro) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacCobro(FacCobro)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacCobroes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacCobroes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacCobro))
            Return New ComandoConsultarTodosFacCobros()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacCobro
        '' ''' </summary>
        '' ''' <param name="usuario">FacCobro que se va a FacCobro</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacCobro(ByVal FacCobro As FacCobro) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacCobro(FacCobro)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacCobro por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacCobro As FacCobro) As ComandoBase(Of FacCobro)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacCobro">FacCobro a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacCobro(ByVal FacCobro As FacCobro) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacCobro(FacCobro)
        End Function

        Public Shared Function ObtenerComandoConsultarFacCobrosFiltro(ByVal FacCobro As FacCobro) As ComandoBase(Of IList(Of FacCobro))
            Return New ComandoConsultarFacCobrosFiltro(FacCobro)
        End Function

    End Class
End Namespace