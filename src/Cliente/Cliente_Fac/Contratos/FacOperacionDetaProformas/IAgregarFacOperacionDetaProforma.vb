Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacOperacionDetaProformas
    Interface IAgregarFacOperacionDetaProforma
        Inherits IPaginaBaseFac
        Property FacOperacionDetaProforma() As Object

        Property Asociado() As Object

        Property Asociados() As Object

        Property idAsociadoFiltrar() As String

        Property NombreAsociado() As String

        Property NombreAsociadoFiltrar() As String

        'Property Persona() As Object

        'Property Personas() As Object

        Property Idioma() As Object

        Property Idiomas() As Object

        Property Moneda() As Object

        Property Monedas() As Object

        Property Banco() As Object

        Property Bancos() As Object

        Property ResultadosFactura2() As Object

        Property ResultadosFactura() As Object

        Property ResultadosFacturaCobro() As Object

        Property ResultadosForma() As Object

        Property MensajeErrorCobro() As String

        Property SumaBono() As Double

        Property SumaBonoBf() As Double

        Property SumaBforma() As Double

        Property SumaBformaBf() As Double

        Property Bforma() As Double

        Property BformaBf() As Double

        Property Xforma() As String

        Property Credito() As Integer

        ReadOnly Property FacFormaSeleccionada As Object

        Sub Mensaje(ByVal mensaje__1 As String)
    End Interface
End Namespace