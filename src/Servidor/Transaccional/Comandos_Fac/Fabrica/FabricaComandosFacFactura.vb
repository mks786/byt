Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacFactura
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacFactura
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacFactura">FacFactura a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacFactura As FacFactura) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacFactura(FacFactura)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacFacturaes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacFacturaes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacFactura))
            Return New ComandoConsultarTodosFacFacturas()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacFactura
        '' ''' </summary>
        '' ''' <param name="usuario">FacFactura que se va a FacFactura</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacFactura(ByVal FacFactura As FacFactura) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacFactura(FacFactura)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacFactura por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacFactura As FacFactura) As ComandoBase(Of FacFactura)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacFactura">FacFactura a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacFactura(ByVal FacFactura As FacFactura) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacFactura(FacFactura)
        End Function

        Public Shared Function ObtenerComandoConsultarFacFacturasFiltro(ByVal FacFactura As FacFactura) As ComandoBase(Of IList(Of FacFactura))
            Return New ComandoConsultarFacFacturasFiltro(FacFactura)
        End Function

    End Class
End Namespace