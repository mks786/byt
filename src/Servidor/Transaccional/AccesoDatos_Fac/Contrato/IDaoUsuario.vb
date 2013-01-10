Imports System.Collections.Generic
Imports Diginsoft.Bolet.ObjetosComunes.Entidades

Namespace Contrato
    Public Interface IDaoUsuario
        Inherits IDaoBase(Of Usuario, String)
        Function Autenticar(ByVal usuario As Usuario) As Usuario
    End Interface
End Namespace