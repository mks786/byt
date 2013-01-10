Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacCredito
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacCredito
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacCredito">FacCredito a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacCredito As FacCredito) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacCredito(FacCredito)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacCreditoes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacCreditoes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacCredito))
            Return New ComandoConsultarTodosFacCreditos()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacCredito
        '' ''' </summary>
        '' ''' <param name="usuario">FacCredito que se va a FacCredito</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacCredito(ByVal FacCredito As FacCredito) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacCredito(FacCredito)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacCredito por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacCredito As FacCredito) As ComandoBase(Of FacCredito)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacCredito">FacCredito a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacCredito(ByVal FacCredito As FacCredito) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacCredito(FacCredito)
        End Function

        Public Shared Function ObtenerComandoConsultarFacCreditosFiltro(ByVal FacCredito As FacCredito) As ComandoBase(Of IList(Of FacCredito))
            Return New ComandoConsultarFacCreditosFiltro(FacCredito)
        End Function

    End Class
End Namespace