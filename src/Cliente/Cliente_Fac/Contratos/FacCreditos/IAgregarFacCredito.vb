Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports System.Windows.Controls
Imports Trascend.Bolet.Cliente.Ayuda
Namespace Contratos.FacCreditos
    Interface IAgregarFacCredito
        Inherits IPaginaBaseFac
        Property FacCredito() As Object

        Property Asociados() As Object

        Property Asociado() As Object

        Property idAsociadoFiltrar() As String

        Property NombreAsociado() As String

        Property NombreAsociadoFiltrar() As String

        'Property Persona() As Object

        'Property Personas() As Object

        Property Banco() As Object

        Property Bancos() As Object

        Property Idioma() As Object

        Property Idiomas() As Object

        Property Moneda() As Object

        Property Monedas() As Object

        Property BMoneda() As Object

        Property BCreditoBf() As Double

        Property BCredito() As Double

        Property BSaldo() As Double

        Sub Mensaje(ByVal mensaje__1 As String)
    End Interface
End Namespace