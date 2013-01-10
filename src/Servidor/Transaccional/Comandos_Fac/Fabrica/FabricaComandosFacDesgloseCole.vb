Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacDesgloseCole
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacDesgloseCole
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacDesgloseCole">FacDesgloseCole a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacDesgloseCole As FacDesgloseCole) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacDesgloseColees
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacDesgloseColees</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacDesgloseCole))
            Return New ComandoConsultarTodosFacDesgloseColes()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacDesgloseCole
        '' ''' </summary>
        '' ''' <param name="usuario">FacDesgloseCole que se va a FacDesgloseCole</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacDesgloseCole(ByVal FacDesgloseCole As FacDesgloseCole) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacDesgloseCole por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacDesgloseCole As FacDesgloseCole) As ComandoBase(Of FacDesgloseCole)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacDesgloseCole">FacDesgloseCole a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacDesgloseCole(ByVal FacDesgloseCole As FacDesgloseCole) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        Public Shared Function ObtenerComandoConsultarFacDesgloseColesFiltro(ByVal FacDesgloseCole As FacDesgloseCole) As ComandoBase(Of IList(Of FacDesgloseCole))
            Return New ComandoConsultarFacDesgloseColesFiltro(FacDesgloseCole)
        End Function

    End Class
End Namespace