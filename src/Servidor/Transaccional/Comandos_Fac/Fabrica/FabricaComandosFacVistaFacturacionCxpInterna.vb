Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacVistaFacturacionCxpInterna
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacVistaFacturacionCxpInterna
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacVistaFacturacionCxpInterna">FacVistaFacturacionCxpInterna a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacVistaFacturacionCxpInterna As FacVistaFacturacionCxpInterna) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacVistaFacturacionCxpInternaes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacVistaFacturacionCxpInternaes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacVistaFacturacionCxpInterna))
            Return New ComandoConsultarTodosFacVistaFacturacionCxpInternas()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacVistaFacturacionCxpInterna
        '' ''' </summary>
        '' ''' <param name="usuario">FacVistaFacturacionCxpInterna que se va a FacVistaFacturacionCxpInterna</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacVistaFacturacionCxpInterna(ByVal FacVistaFacturacionCxpInterna As FacVistaFacturacionCxpInterna) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacVistaFacturacionCxpInterna por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacVistaFacturacionCxpInterna As FacVistaFacturacionCxpInterna) As ComandoBase(Of FacVistaFacturacionCxpInterna)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacVistaFacturacionCxpInterna">FacVistaFacturacionCxpInterna a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacVistaFacturacionCxpInterna(ByVal FacVistaFacturacionCxpInterna As FacVistaFacturacionCxpInterna) As ComandoBase(Of Boolean)
            Throw New NotImplementedException()
        End Function

        Public Shared Function ObtenerComandoConsultarFacVistaFacturacionCxpInternasFiltro(ByVal FacVistaFacturacionCxpInterna As FacVistaFacturacionCxpInterna) As ComandoBase(Of IList(Of FacVistaFacturacionCxpInterna))
            Return New ComandoConsultarFacVistaFacturacionCxpInternasFiltro(FacVistaFacturacionCxpInterna)
        End Function

    End Class
End Namespace