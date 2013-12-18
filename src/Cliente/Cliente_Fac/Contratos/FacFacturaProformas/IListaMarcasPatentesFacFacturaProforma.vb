Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacFacturaProformas
    Interface IListaMarcasPatentesFacFacturaProforma
        Inherits IPaginaBaseFac

        Property Elementos As Object

        Property ElementoConsultado As Object

        Property IdElemento As String

        Property IdInternacionalElemento As String

        Property Solicitud As String

        Property Registro As String

        Property Marcas As Object

        ReadOnly Property MarcaSeleccionada As Object

        Property Patentes As Object

        ReadOnly Property PatenteSeleccionada As Object

        Sub HabilitarCampoInternacional(estado As Boolean)

        Sub Mensaje(mensaje As String, tipoMensaje As Integer)

        Sub GestionarVisibilidadListas(estado As Boolean)

    End Interface
End Namespace

