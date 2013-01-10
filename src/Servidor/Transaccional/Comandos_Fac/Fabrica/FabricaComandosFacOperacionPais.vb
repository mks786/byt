Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacOperacionPais
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacOperacionPais
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacOperacionPais">FacOperacionPais a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacOperacionPais As FacOperacionPais) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacOperacionPais(FacOperacionPais)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacOperacionPaises
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacOperacionPaises</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacOperacionPais))
            Return New ComandoConsultarTodosFacOperacionPaises()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacOperacionPais
        '' ''' </summary>
        '' ''' <param name="usuario">FacOperacionPais que se va a FacOperacionPais</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacOperacionPais(ByVal FacOperacionPais As FacOperacionPais) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacOperacionPais(FacOperacionPais)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacOperacionPais por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacOperacionPais As FacOperacionPais) As ComandoBase(Of FacOperacionPais)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacOperacionPais">FacOperacionPais a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacOperacionPais(ByVal FacOperacionPais As FacOperacionPais) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacOperacionPais(FacOperacionPais)
        End Function

        Public Shared Function ObtenerComandoConsultarFacOperacionPaisesFiltro(ByVal FacOperacionPais As FacOperacionPais) As ComandoBase(Of IList(Of FacOperacionPais))
            Return New ComandoConsultarFacOperacionPaisesFiltro(FacOperacionPais)
        End Function

    End Class
End Namespace