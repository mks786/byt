Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.Cliente.Ayuda
Namespace Contratos.FacFacturaProformas
    Interface IAgregarFacFacturaProforma
        Inherits IPaginaBaseFac
        Property FacFacturaProforma() As Object

        Property Asociado() As Object

        Property Asociados() As Object

        Property Interesado() As Object

        Property Interesados() As Object

        Property Carta() As Object

        Property Cartas() As Object

        Property DetalleEnvio() As Object

        Property DetalleEnvios() As Object

        Property idAsociadoFiltrar() As String

        Property idInteresadoFiltrar() As String

        Property NombreAsociado() As String

        Property NombreInteresado() As String

        'Property IdAsociado() As String

        Property NombreAsociadoFiltrar() As String

        Property NombreInteresadoFiltrar() As String

        Property AsociadoImp() As Object

        Property AsociadosImp() As Object

        Property idAsociadoFiltrarImp() As String

        Property NombreAsociadoImp() As String

        Property NombreAsociadoFiltrarImp() As String

        Property Seleccion() As Boolean

        Property BIMulmon() As Boolean

        Property Desglose As Boolean

        'Property BBsel As Char
        '---
        Property Marca() As Object

        Property Marcas() As Object

        Property Patente() As Object

        Property Patentes() As Object

        Property idMarcaFiltrar() As String

        'Property NombreMarca() As String

        Property NombreMarcaFiltrar() As String

        Property CodigoInscripcion() As String

        Property Registro() As String
        '---

        Property idPatenteFiltrar() As String

        'Property NombrePatente() As String

        Property NombrePatenteFiltrar() As String

        Property ReferenciaCartaFiltrar() As String

        Property NombreCarta() As String

        ' Property NombreCartaFiltrar() As String

        Property FechaCartaFiltrar() As String

        Property idCartaFiltrar() As String

        Property VerTipo() As String

        ReadOnly Property DepartamentoServicio_2Seleccionado() As Object

        ReadOnly Property FacFactuDetaProforma_2Seleccionado() As Object

        ReadOnly Property Marcas_Seleccionado() As Object

        ReadOnly Property Patentes_Seleccionado() As Object

        ReadOnly Property DocumentoMarca_Seleccionado() As Object

        ReadOnly Property DocumentoPatente_Seleccionado() As Object

        ReadOnly Property DesgloseServicio_Seleccionado() As Object

        ReadOnly Property DocumentoTraduccion_Seleccionado() As Object

        ReadOnly Property Recurso_Seleccionado() As Object

        ReadOnly Property Material_Seleccionado() As Object

        ReadOnly Property Anualidad_Seleccionado() As Object

        Property FacFactuDetaProforma_Seleccionado() As Object

        ReadOnly Property FechaFactura() As DateTime

        'Property Persona() As Object

        'Property Personas() As Object

        Property Idioma() As Object

        Property Idiomas() As Object

        Property Moneda() As Object

        Property Monedas() As Object

        Property Tarifa() As Object

        Property Codeti() As Object

        Property Rif() As Object

        Property XNit() As Object

        Property XAsociado() As Object

        Property OrigenesProforma() As Object

        Property OrigenProforma() As Object

        'Property Tarifas() As Object

        'Property Banco() As Object

        'Property Bancos() As Object

        Property ResultadosDesgloseServicio2() As Object

        Property ResultadosDepartamentoServicio2() As Object

        Property ResultadosDocumentosMarca() As Object

        Property ResultadosDocumentosPatente() As Object

        Property ResultadosDocumentosTraduccion() As Object

        Property ResultadosRecurso() As Object

        Property ResultadosMaterial() As Object

        Property ResultadosFacFactuDetaProforma() As Object

        Property ResultadosMarca() As Object

        Property ResultadosMultiplesMarcas() As Object

        Property ResultadosPatente() As Object

        Property ResultadosMultiplesPatentes() As Object

        Property ResultadosAnualidad() As Object

        'Property ResultadosFactura() As Object

        'Property ResultadosFacturaCobro() As Object

        'Property ResultadosForma() As Object

        ReadOnly Property Cantidad() As String

        Property Ourref() As String

        Property Caso() As String

        Property MensajeError() As String

        Property NCantidad() As Integer

        Property Pu() As Double

        Property Descuento() As Double

        Property BDetalle() As Double

        Property MSubtimpo() As Double

        Property MSubtimpoBf() As Double

        Property PDescuento() As Double

        Property MDescuento() As Double

        Property MDescuentoBf() As Double

        Property MTbimp() As Double

        Property MTbimpBf() As Double

        Property Mtbexc() As Double

        Property MtbexcBf() As Double

        Property Msubtotal() As Double

        Property MsubtotalBf() As Double

        Property Impuesto() As String

        Property Mtimp() As Double

        Property MtimpBf() As Double

        Property Mttotal() As Double

        Property MttotalBf() As Double

        Property Desc() As Double

        'Property Bforma() As Double

        'Property BformaBf() As Double

        'Property Xforma() As String

        'Property Credito() As Integer

        WriteOnly Property Activar_Desactivar As Boolean

        WriteOnly Property Desactivar_Descuento As Boolean

        WriteOnly Property Desactivar_Precio As Boolean

        ReadOnly Property Localidad() As Char

        Property Tipo() As Char

        Property Localidad2() As Char

        Property ServicioId() As String

        Property ServicioCod_Cont() As String

        Property ServicioXreferencia() As String

        WriteOnly Property Departamento() As String

        WriteOnly Property DescripcionServicio() As String

        Property CurSortCol() As GridViewColumnHeader

        Property CurAdorner() As SortAdorner

        Property ListaResultados() As ListView

        Sub focos_elejir(ByVal valor As String)

        Sub Mensaje(ByVal mensaje__1 As String)

        Sub Mensaje(mensaje As String, tipo As Integer)

    End Interface
End Namespace