Imports System.Windows
Imports System.Windows.Controls
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DataTable = System.Data.DataTable
Imports System.Data

Namespace Ventanas.FacReportes
    Class FacturaRpt2

        '      Private Sub reporte(ByVal sender As Object, ByVal e As RoutedEventArgs)
        '          Dim report As New ReportDocument()
        '          report.Load("../../CrystalReport1.rpt")
        '          Using db As New carta()
        'report.SetDataSource(From c In db.ClientsNew From { _
        '	c.CustomerName, _
        '	c.Address, _
        '	c.EmailId, _
        '	c.PassportNumber _
        '})
        '          End Using
        '          CrystalReportsViewer1.ViewerCore.ReportSource = report
        '      End Sub
        Public Sub New()
            InitializeComponent()
            'Aceptar()
        End Sub

        Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            'Aceptar()
        End Sub

        Private Function ObtenerEstructuraCarta1() As List(Of StructReporteCarta1)
            Dim retorno As IList(Of StructReporteCarta1) = New List(Of StructReporteCarta1)
            Dim estructura As New StructReporteCarta1()
            Try
                estructura.FechaCarta = "11-09-2012"

                estructura.Marca = "Marca prueba"

                estructura.CodigoRegistro = "Codigo:150"

                estructura.ClaseMarca = "Clase:78"

                estructura.FechaRenovacion = "11-09-2012"

                estructura.Interesado = "Interesado:150"

                estructura.Asociado = "asociado:22966"
                estructura.Referencia = "referencia:abc"
                estructura.DomicilioAsociado = "caracas venezuela"
                retorno.Add(estructura)

                estructura.FechaCarta = "12-09-2012"

                estructura.Marca = "Marca prueba222"

                estructura.CodigoRegistro = "Codigo:150000"

                estructura.ClaseMarca = "Clase:78888"

                estructura.FechaRenovacion = "12-09-2012"

                estructura.Interesado = "Interesado:150000"

                estructura.Asociado = "asociado:229669999"
                estructura.Referencia = "referencia:abccccc"
                estructura.DomicilioAsociado = "caracas venezuelaaaaa"
                retorno.Add(estructura)
            Catch ex As Exception
                'logger.Error(ex.Message)

            End Try

            Return retorno

        End Function

        'Private Function ObtenerEstructuraCarta1() As List(Of StructReporteCarta1)
        '    Dim retorno As IList(Of StructReporteCarta1) = New List(Of StructReporteCarta1)
        '    Dim estructura As New StructReporteCarta1()
        '    Try
        '        estructura.FechaCarta = "11-09-2012"

        '        estructura.Marca = "Marca prueba"

        '        estructura.CodigoRegistro = "Codigo:150"

        '        estructura.ClaseMarca = "Clase:78"

        '        estructura.FechaRenovacion = "11-09-2012"

        '        estructura.Interesado = "Interesado:150"

        '        estructura.Asociado = "asociado:22966"
        '        estructura.Referencia = "referencia:abc"
        '        estructura.DomicilioAsociado = "caracas venezuela"
        '        retorno.Add(estructura)

        '        estructura.FechaCarta = "12-09-2012"

        '        estructura.Marca = "Marca prueba222"

        '        estructura.CodigoRegistro = "Codigo:150000"

        '        estructura.ClaseMarca = "Clase:78888"

        '        estructura.FechaRenovacion = "12-09-2012"

        '        estructura.Interesado = "Interesado:150000"

        '        estructura.Asociado = "asociado:229669999"
        '        estructura.Referencia = "referencia:abccccc"
        '        estructura.DomicilioAsociado = "caracas venezuelaaaaa"
        '        retorno.Add(estructura)


        '    Catch ex As Exception
        '        'logger.Error(ex.Message)

        '    End Try

        '    Return retorno

        'End Function

        Private Function GetRutaReporte() As String
            Dim retorno As String
            'retorno = "../../Reportes/Carta1CR.rpt"
            retorno = "C:\DG_2012_09_13\DG\src\Cliente\Cliente_Fac\Reportes\prueba.rpt"
            Return retorno

        End Function

        Public Sub Aceptar()
            Mouse.OverrideCursor = Cursors.Wait
            Try

                Dim reporte As New ReportDocument()
                Dim estructuraDeDatos As IList(Of StructReporteCarta1) = New List(Of StructReporteCarta1)()
                'reporte.Load();                
                Dim datos As New DataTable("DataTable1")
                datos.Columns.Add("NombreMarca")
                datos.Columns.Add("CodigoRegistroMarca")
                datos.Columns.Add("FechaRenovacion")
                datos.Columns.Add("ClaseMarca")
                datos.Columns.Add("Interesado")
                datos.Columns.Add("Asociado")
                datos.Columns.Add("Domicilio")
                datos.Columns.Add("FechaCarta")
                datos.Columns.Add("Referencia")
                'Dim estructura As StructReporteCarta1 = ObtenerEstructuraCarta1()
                'estructuraDeDatos.Add(estructura)
                estructuraDeDatos = ObtenerEstructuraCarta1()

                '        'Dim estructura As StructReporteCarta1 = ObtenerEstructuraCarta1()
                '        'estructuraDeDatos.Add(estructura)
                '        estructuraDeDatos = ObtenerEstructuraCarta1()

                datos = ArmarReporte(datos, estructuraDeDatos)


                'reporte.PrintOptions.PrinterName = ConfigurationManager.AppSettings["ImpresoraReportes"];
                Dim ds As New DataSet()
                ds.Tables.Add(datos)
                Dim a As String = ""
                reporte.Load(GetRutaReporte())
                reporte.SetDataSource(datos)
                crystalReportsViewer1.ViewerCore.ReportSource = reporte
                'reporte.PrintToPrinter(1, False, 1, 0)                

                Mouse.OverrideCursor = Nothing

                '#End Region

            Catch ex As ApplicationException
                'logger.Error(ex.Message)
                'Me.Navegar(ex.Message, True)
                Mouse.OverrideCursor = Nothing
            Catch ex As Exception
                Mouse.OverrideCursor = Nothing
                '' Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)

            End Try
        End Sub
        'Public Sub Aceptar()
        '    Mouse.OverrideCursor = Cursors.Wait
        '    Try

        '        Dim reporte As New ReportDocument()
        '        Dim estructuraDeDatos As IList(Of StructReporteCarta1) = New List(Of StructReporteCarta1)()
        '        'reporte.Load();                
        '        Dim datos As New DataTable("DataTable1")
        '        datos.Columns.Add("NombreMarca")
        '        datos.Columns.Add("CodigoRegistroMarca")
        '        datos.Columns.Add("FechaRenovacion")
        '        datos.Columns.Add("ClaseMarca")
        '        datos.Columns.Add("Interesado")
        '        datos.Columns.Add("Asociado")
        '        datos.Columns.Add("Domicilio")
        '        datos.Columns.Add("FechaCarta")
        '        datos.Columns.Add("Referencia")
        '        'Dim estructura As StructReporteCarta1 = ObtenerEstructuraCarta1()
        '        'estructuraDeDatos.Add(estructura)
        '        estructuraDeDatos = ObtenerEstructuraCarta1()


        '        datos = ArmarReporte(datos, estructuraDeDatos)
        '        'reporte.PrintOptions.PrinterName = ConfigurationManager.AppSettings["ImpresoraReportes"];
        '        Dim ds As New DataSet()
        '        ds.Tables.Add(datos)
        '        Dim a As String = ""
        '        reporte.Load(GetRutaReporte())
        '        'reporte.SetDataSource(datos)                
        '        crystalReportsViewer1.ViewerCore.ReportSource = reporte
        '        'reporte.PrintToPrinter(1, False, 1, 0)                

        '        Mouse.OverrideCursor = Nothing

        '        '#End Region

        '    Catch ex As ApplicationException
        '        'logger.Error(ex.Message)
        '        'Me.Navegar(ex.Message, True)
        '        Mouse.OverrideCursor = Nothing
        '    Catch ex As Exception
        '        Mouse.OverrideCursor = Nothing
        '        '' Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)

        '    End Try
        'End Sub

        Private Function ArmarReporte(ByVal datos As DataTable, ByVal estructurasDeDatos As IList(Of StructReporteCarta1)) As DataTable
            For Each structura As StructReporteCarta1 In estructurasDeDatos
                Dim filaDatos As DataRow = datos.NewRow()
                filaDatos("FechaCarta") = structura.FechaCarta
                filaDatos("NombreMarca") = structura.Marca
                filaDatos("CodigoRegistroMarca") = structura.CodigoRegistro
                filaDatos("Asociado") = structura.Asociado
                filaDatos("Domicilio") = structura.DomicilioAsociado
                filaDatos("ClaseMarca") = structura.ClaseMarca
                filaDatos("Interesado") = structura.Interesado
                filaDatos("Referencia") = structura.Referencia
                filaDatos("FechaRenovacion") = structura.FechaRenovacion
                datos.Rows.Add(filaDatos)

            Next
            Return datos

        End Function



        Structure StructReporteCarta1
            Private _fechaCarta As String
            Private _marca As String
            Private _fechaRenovacion As String
            Private _codigoRegistro As String
            Private _referencia As String
            Private _claseMarca As String
            Private _interesado As String
            Private _domicilioAsociado As String
            Private _asociado As String

            Public Property FechaCarta() As String
                Get
                    Return Me._fechaCarta
                End Get
                Set(ByVal value As String)
                    Me._fechaCarta = value
                End Set
            End Property

            Public Property Marca() As String
                Get
                    Return Me._marca
                End Get
                Set(ByVal value As String)
                    Me._marca = value
                End Set
            End Property

            Public Property FechaRenovacion() As String
                Get
                    Return Me._fechaRenovacion
                End Get
                Set(ByVal value As String)
                    Me._fechaRenovacion = value
                End Set
            End Property


            Public Property CodigoRegistro() As String
                Get
                    Return Me._codigoRegistro
                End Get
                Set(ByVal value As String)
                    Me._codigoRegistro = value
                End Set
            End Property

            Public Property Referencia() As String
                Get
                    Return Me._referencia
                End Get
                Set(ByVal value As String)
                    Me._referencia = value
                End Set
            End Property

            Public Property ClaseMarca() As String
                Get
                    Return Me._claseMarca
                End Get
                Set(ByVal value As String)
                    Me._claseMarca = value
                End Set
            End Property

            Public Property Interesado() As String
                Get
                    Return Me._interesado
                End Get
                Set(ByVal value As String)
                    Me._interesado = value
                End Set
            End Property

            Public Property DomicilioAsociado() As String
                Get
                    Return Me._domicilioAsociado
                End Get
                Set(ByVal value As String)
                    Me._domicilioAsociado = value
                End Set
            End Property

            Public Property Asociado() As String
                Get
                    Return Me._asociado
                End Get
                Set(ByVal value As String)
                    Me._asociado = value
                End Set
            End Property
        End Structure

        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button1.Click
            Aceptar()
        End Sub
    End Class
End Namespace