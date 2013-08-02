Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosCarpetaGestionAutomatica
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos

Namespace Fabrica
    Public NotInheritable Class FabricaComandosCarpetaGestionAutomatica
        Private Sub New()
        End Sub


        ''' <summary>
        ''' Método que devuelve el Comando para consultar las carpetas de Gestion Automatica por las iniciales del usuario
        ''' </summary>
        ''' <param name="Usuario">Usuario utilizado para consultar las carpetas</param>
        ''' <returns>Lista de Carpetas para Gestiones Automaticas</returns>
        Public Shared Function ObtenerComandoObtenerCarpetasPorIniciales(ByVal Usuario As Usuario) As ComandoBase(Of IList(Of CarpetaGestionAutomatica))
            Return New ComandoObtenerCarpetasPorIniciales(Usuario)
        End Function

    End Class
End Namespace

