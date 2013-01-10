Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacBanco
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacBanco
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="FacBanco">FacBanco a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacBanco As FacBanco) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacBancoes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacBancoes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacBanco))
            Return New ComandoConsultarTodosFacBancos()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un FacBanco
        ' ''' </summary>
        ' ''' <param name="usuario">FacBanco que se va a FacBanco</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacBanco(ByVal FacBanco As FacBanco) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacBanco por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacBanco As FacBanco) As ComandoBase(Of FacBanco)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="FacBanco">FacBanco a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacBanco(ByVal FacBanco As FacBanco) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace