Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacCobroFactura
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacCobroFactura
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacCobroFactura">FacCobroFactura a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacCobroFactura As FacCobroFactura) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacCobroFactura(FacCobroFactura)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacCobroFacturaes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacCobroFacturaes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacCobroFactura))
            Return New ComandoConsultarTodosFacCobroFacturas()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacCobroFactura
        '' ''' </summary>
        '' ''' <param name="usuario">FacCobroFactura que se va a FacCobroFactura</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacCobroFactura(ByVal FacCobroFactura As FacCobroFactura) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacCobroFactura(FacCobroFactura)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacCobroFactura por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacCobroFactura As FacCobroFactura) As ComandoBase(Of FacCobroFactura)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacCobroFactura">FacCobroFactura a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacCobroFactura(ByVal FacCobroFactura As FacCobroFactura) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacCobroFactura(FacCobroFactura)
        End Function

        Public Shared Function ObtenerComandoConsultarFacCobroFacturasFiltro(ByVal FacCobroFactura As FacCobroFactura) As ComandoBase(Of IList(Of FacCobroFactura))
            Return New ComandoConsultarFacCobroFacturasFiltro(FacCobroFactura)
        End Function

    End Class
End Namespace