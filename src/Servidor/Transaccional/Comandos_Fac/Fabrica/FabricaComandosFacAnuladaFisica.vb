Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacAnuladaFisica
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacAnuladaFisica
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacAnuladaFisica">FacAnuladaFisica a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacAnuladaFisica As FacAnuladaFisica) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacAnuladaFisica(FacAnuladaFisica)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacAnuladaFisicaes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacAnuladaFisicaes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacAnuladaFisica))
            Return New ComandoConsultarTodosFacAnuladaFisicas()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacAnuladaFisica
        '' ''' </summary>
        '' ''' <param name="usuario">FacAnuladaFisica que se va a FacAnuladaFisica</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacAnuladaFisica(ByVal FacAnuladaFisica As FacAnuladaFisica) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacAnuladaFisica(FacAnuladaFisica)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacAnuladaFisica por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacAnuladaFisica As FacAnuladaFisica) As ComandoBase(Of FacAnuladaFisica)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacAnuladaFisica">FacAnuladaFisica a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacAnuladaFisica(ByVal FacAnuladaFisica As FacAnuladaFisica) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacAnuladaFisica(FacAnuladaFisica)
        End Function

        Public Shared Function ObtenerComandoConsultarFacAnuladaFisicasFiltro(ByVal FacAnuladaFisica As FacAnuladaFisica) As ComandoBase(Of IList(Of FacAnuladaFisica))
            Return New ComandoConsultarFacAnuladaFisicasFiltro(FacAnuladaFisica)
        End Function

    End Class
End Namespace