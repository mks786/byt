Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacFacturaTotalZ
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacFacturaTotalZ
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacFacturaTotalZ">FacFacturaTotalZ a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacFacturaTotalZ As FacFacturaTotalZ) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacFacturaTotalZes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacFacturaTotalZes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacFacturaTotalZ))
            Return New ComandoConsultarTodosFacFacturaTotalZs()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacFacturaTotalZ
        '' ''' </summary>
        '' ''' <param name="usuario">FacFacturaTotalZ que se va a FacFacturaTotalZ</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacFacturaTotalZ(ByVal FacFacturaTotalZ As FacFacturaTotalZ) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacFacturaTotalZ por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacFacturaTotalZ As FacFacturaTotalZ) As ComandoBase(Of FacFacturaTotalZ)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacFacturaTotalZ">FacFacturaTotalZ a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacFacturaTotalZ(ByVal FacFacturaTotalZ As FacFacturaTotalZ) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        Public Shared Function ObtenerComandoConsultarFacFacturaTotalZsFiltro(ByVal FacFacturaTotalZ As FacFacturaTotalZ) As ComandoBase(Of IList(Of FacFacturaTotalZ))
            Return New ComandoConsultarFacFacturaTotalZsFiltro(FacFacturaTotalZ)
        End Function

    End Class
End Namespace