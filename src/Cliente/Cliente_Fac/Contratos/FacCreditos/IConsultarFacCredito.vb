Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacCreditos
    Interface IConsultarFacCredito
        Inherits IPaginaBaseFac
        Property FacCredito() As Object
        WriteOnly Property HabilitarCampos() As Boolean

        Property Banco() As Object

        Property Bancos() As Object

        Property Asociados() As Object

        Property Asociado() As Object

        Property idAsociadoFiltrar() As String

        Property NombreAsociado() As String

        Property NombreAsociadoFiltrar() As String

        Property Idioma() As Object

        Property Idiomas() As Object

        Property Moneda() As Object

        Property Monedas() As Object

        Property BCredito() As Double

        Property BCreditoBf() As Double

        Property BSaldo() As Double

        'Property Region() As String 

        Property TextoBotonModificar() As String

        Property Resultados() As Object

        Property ListaResultados() As ListView

        Property CobroSeleccionado() As Object

        WriteOnly Property OcultarBtnEliminar As Boolean

    End Interface
End Namespace