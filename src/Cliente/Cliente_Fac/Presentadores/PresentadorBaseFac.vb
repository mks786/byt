Imports System.Collections.Generic
Imports System.Windows.Controls
'Imports Trascend.Bolet.Cliente.Contratos.Principales
'Imports Trascend.Bolet.Cliente.Ventanas.Principales
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Diginsoft.Bolet.ObjetosComunes.Entidades

Namespace Trascend.Bolet.Cliente.Presentadores
    Public Class PresentadorBaseFac
        'Private Shared _ventanaPrincipal As IVentanaPrincipal = VentanaPrincipal.ObtenerInstancia
        'Private Shared _paginaPrincipal As IPaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared _usuarioLogeado As Usuario

        ''' <summary>
        ''' Propiedad que representa el usuario logeado en el sistema
        ''' </summary>
        Public Shared Property UsuarioLogeado() As Usuario
            Get
                Return _usuarioLogeado
            End Get
            Set(ByVal value As Usuario)
                _usuarioLogeado = value
            End Set
        End Property

        ''' <summary>
        ''' Método que permite navegar hasta la página principal
        ''' </summary>
        'Public Sub Navegar()
        '    _paginaPrincipal.MensajeError = ""
        '    _paginaPrincipal.MensajeUsuario = ""
        '    _ventanaPrincipal.Contenedor.Navigate(_paginaPrincipal)
        'End Sub

        ''' <summary>
        ''' Método que permite navegar hasta la página principal y 
        ''' colocar un mensaje de error en dicha página
        ''' </summary>
        ''' <param name="mensaje"></param>
        'Public Sub Navegar(ByVal mensaje As String, ByVal [error] As Boolean)
        '    If [error] Then
        '        _paginaPrincipal.MensajeError = mensaje
        '    Else
        '        _paginaPrincipal.MensajeUsuario = mensaje
        '    End If

        '    _ventanaPrincipal.Contenedor.Navigate(_paginaPrincipal)
        'End Sub

        ''' <summary>
        ''' Método que permite navegar a una página específica
        ''' </summary>
        ''' <param name="pagina">Página que se quiere mostrar</param>
        'Public Sub Navegar(ByVal pagina As Page)
        '    _ventanaPrincipal.Contenedor.Navigate(pagina)
        'End Sub

        ''' <summary>
        ''' Método que permite navegar hasta la página principal
        ''' </summary>
        'Public Sub Cancelar()
        '    Me.Navegar()
        'End Sub

        ''' <summary>
        ''' Método que verifica si se puede regresar a la página anterior,
        ''' Si se puede muestra la página anterior, en caso contrario muestra
        ''' la página principal
        ''' </summary>
        'Public Sub Regresar()
        '    If _ventanaPrincipal.Contenedor.CanGoBack Then
        '        _ventanaPrincipal.Contenedor.GoBack()
        '    Else
        '        Me.Navegar()
        '    End If
        'End Sub

        ''' <summary>
        ''' Método que actualiza el título de la ventana principal
        ''' </summary>
        ''' <param name="titulo">Título de la ventana que se está cargando</param>
        ''' <param name="id">Id de la ventana que se está cargando</param>
        'Public Sub ActualizarTituloVentanaPrincipal(ByVal titulo As String, ByVal id As String)
        '    Dim ventanaPrincipal__1 As VentanaPrincipal = VentanaPrincipal.ObtenerInstancia
        '    Dim tituloAColocar As String = Recursos.Etiquetas.titlePrincipal + " - " & titulo
        '    If Not String.IsNullOrEmpty(id) Then
        '        tituloAColocar += " (" & id & ")"
        '    End If
        '    ventanaPrincipal__1.Title = tituloAColocar
        'End Sub

        ''' <summary>
        ''' Método que busca un Rol dentro de una lista de roles
        ''' </summary>
        ''' <param name="roles">Lista de roles</param>
        ''' <param name="rolBuscado">rol a buscar</param>
        ''' <returns>Rol dentro de la lista</returns>
        Public Function BuscarRol(ByVal roles As IList(Of Rol), ByVal rolBuscado As Rol) As Rol
            Dim retorno As Rol = Nothing

            If rolBuscado IsNot Nothing Then
                For Each rol As Rol In roles
                    If rol.Id = rolBuscado.Id Then
                        retorno = rol
                        Exit For
                    End If
                Next
            End If

            Return retorno
        End Function

        ''' <summary>
        ''' Método que busca un Departamento dentro de una lista de Departamentos
        ''' </summary>
        ''' <param name="roles">Lista de departamentos</param>
        ''' <param name="departamentoBuscado">Departamento a buscar</param>
        ''' <returns>Departamento dentro de la lista</returns>
        Public Function BuscarDepartamento(ByVal departamentos As IList(Of Departamento), ByVal departamentoBuscado As Departamento) As Departamento
            Dim retorno As Departamento = Nothing

            If departamentos IsNot Nothing Then
                For Each departamento As Departamento In departamentos
                    If departamento.Id = departamentoBuscado.Id Then
                        retorno = departamento
                        Exit For
                    End If
                Next
            End If

            Return retorno
        End Function

        ''' <summary>
        ''' Método que busca un Departamento dentro de una lista de usuarios
        ''' </summary>
        ''' <param name="usuarios">Lista de usuarios</param>
        ''' <param name="iniciales">Iniciales a buscar</param>
        ''' <returns>Usuario dentro de la lista</returns>
        Public Function BuscarPersonaPorInicial(ByVal usuarios As IList(Of Usuario), ByVal iniciales As String) As Usuario
            Dim retorno As Usuario = Nothing

            If usuarios IsNot Nothing Then
                For Each usuario As Usuario In usuarios
                    If usuario.Iniciales.Equals(iniciales) Then
                        retorno = usuario
                        Exit For
                    End If
                Next
            End If

            Return retorno
        End Function

        ''' <summary>
        ''' Método que busca un Interesado dentro de una lista de interesados
        ''' </summary>
        ''' <param name="interesados">Lista de interesados</param>
        ''' <param name="interesadoBuscado">Interesado a buscar</param>
        ''' <returns>Interesado dentro de la lista</returns>
        Public Function BuscarInteresado(ByVal interesados As IList(Of Interesado), ByVal interesadoBuscado As Interesado) As Interesado
            Dim retorno As Interesado = Nothing

            If interesadoBuscado IsNot Nothing Then
                For Each interesado As Interesado In interesados
                    If interesado.Id = interesadoBuscado.Id Then
                        retorno = interesado
                        Exit For
                    End If
                Next
            End If

            Return retorno
        End Function

        ''' <summary>
        ''' Método que busca un Boletin dentro de una lista de Boletines
        ''' </summary>
        ''' <param name="boletines">Lista de boletines</param>
        ''' <param name="boletinBuscado">Boletin a buscar</param>
        ''' <returns>Boletin dentro de la lista</returns>
        Public Function BuscarBoletin(ByVal boletines As IList(Of Boletin), ByVal boletinBuscado As Boletin) As Boletin
            Dim retorno As Boletin = Nothing

            If boletinBuscado IsNot Nothing Then
                For Each boletin As Boletin In boletines
                    If boletin.Id = boletinBuscado.Id Then
                        retorno = boletin
                        Exit For
                    End If
                Next
            End If

            Return retorno
        End Function

        ''' <summary>
        ''' Busca la el sexo (género) correspondiente a la inicial que se le esté pasando
        ''' </summary>
        ''' <param name="sexo">Inicial del sexo (género)</param>
        ''' <returns>El sexo (género) correspondiente</returns>
        Public Function BuscarSexo(ByVal sexo As Char) As String
            Dim retorno As String

            If Recursos.Etiquetas.cbiMasculino(0).Equals(sexo) Then
                retorno = Recursos.Etiquetas.cbiMasculino
            Else
                retorno = Recursos.Etiquetas.cbiFemenino
            End If

            Return retorno
        End Function

        ''' <summary>
        ''' Busca el tipode persona correspondiente a la inicial que se le esté pasando
        ''' </summary>
        ''' <param name="tipoPersona">Inicial del tipo de persona</param>
        ''' <returns>El tipo de persona correspondiente</returns>
        Public Function BuscarTipoPersona(ByVal tipoPersona As Char) As String
            Dim retorno As String

            If Recursos.Etiquetas.cbiJuridica(0).Equals(tipoPersona) Then
                retorno = Recursos.Etiquetas.cbiJuridica
            Else
                retorno = Recursos.Etiquetas.cbiNatural
            End If

            Return retorno
        End Function


        ''' <summary>
        ''' Busca el tipo de destinatario correspondiente a la inicial que se le esté pasando
        ''' </summary>
        ''' <param name="tipoDestinatario">Inicial del tipo de Destinatario</param>
        ''' <returns>El tipo de destinatario correspondiente</returns>
        'Public Function BuscarTipoDestinatario(ByVal tipoDestinatario As Char) As String
        '    Dim retorno As String

        '    If Recursos.Etiquetas.cbiPersona(0) = tipoDestinatario Then
        '        retorno = Recursos.Etiquetas.cbiPersona
        '    ElseIf Recursos.Etiquetas.cbiDepartamento(0) = tipoDestinatario Then
        '        retorno = Recursos.Etiquetas.cbiDepartamento
        '    Else
        '        retorno = Recursos.Etiquetas.cbiNinguno
        '    End If

        '    Return retorno
        'End Function

        ''' <summary>
        ''' Busca el estado civil correspondiente a la inicial que se le esté pasando
        ''' </summary>
        ''' <param name="estadoCivil">Inicial del estado civil</param>
        ''' <returns>El estado civil correspondiente</returns>
        Public Function BuscarEstadoCivil(ByVal estadoCivil As Char) As String
            Dim retorno As String

            If Recursos.Etiquetas.cbiSoltero(0).Equals(estadoCivil) Then
                retorno = Recursos.Etiquetas.cbiSoltero
            ElseIf Recursos.Etiquetas.cbiCasado(0).Equals(estadoCivil) Then
                retorno = Recursos.Etiquetas.cbiCasado
            ElseIf Recursos.Etiquetas.cbiDivorciado(0).Equals(estadoCivil) Then
                retorno = Recursos.Etiquetas.cbiDivorciado
            Else
                retorno = Recursos.Etiquetas.cbiViudo
            End If

            Return retorno
        End Function

        ''' <summary>
        ''' Busca el tipo de remitente correspondiente a la inicial que se le esté pasando
        ''' </summary>
        ''' <param name="tipoRemitente">Inicial del tipo del remintente</param>
        '''' <returns>El tipo de remitente correspondiente</returns>
        'Public Function BuscarTipoRemitente(ByVal tipoRemitente As Char) As String
        '    Dim retorno As String

        '    If Recursos.Etiquetas.cbiProveedor(0).Equals(tipoRemitente) Then
        '        retorno = Recursos.Etiquetas.cbiProveedor
        '    ElseIf Recursos.Etiquetas.cbiOtro(0).Equals(tipoRemitente) Then
        '        retorno = Recursos.Etiquetas.cbiOtro
        '    Else
        '        retorno = Recursos.Etiquetas.cbiNinguno
        '    End If

        '    Return retorno
        'End Function

        ''' <summary>
        ''' Método que busca un Pais dentro de una lista de paises
        ''' </summary>
        ''' <param name="paises">Lista de paises</param>
        ''' <param name="paisBuscado">pais a buscar</param>
        ''' <returns>pais dentro de la lista</returns>
        Public Function BuscarPais(ByVal paises As IList(Of Pais), ByVal paisBuscado As Pais) As Pais
            Dim retorno As Pais = Nothing

            If paisBuscado IsNot Nothing Then
                For Each pais As Pais In paises
                    If pais.Id = paisBuscado.Id Then
                        retorno = pais
                        Exit For
                    End If
                Next
            End If

            Return retorno
        End Function

        ''' <summary>
        ''' Método que busca un estado dentro de una lista de estados
        ''' </summary>
        ''' <param name="estados">Lista de estados</param>
        ''' <param name="estadoBuscado">estado a buscar</param>
        ''' <returns>Estado dentro de la lista</returns>
        Public Function BuscarEstado(ByVal estados As IList(Of Estado), ByVal estadoBuscado As Estado) As Estado
            Dim retorno As Estado = Nothing

            If estadoBuscado IsNot Nothing Then
                For Each estado As Estado In estados
                    If estado.Id.Equals(estadoBuscado.Id) Then
                        retorno = estado
                        Exit For
                    End If
                Next
            End If

            Return retorno
        End Function

        ''' <summary>
        ''' Método que busca un estado dentro de una lista de Idiomas
        ''' </summary>
        ''' <param name="monedas">Lista de idiomas</param>
        ''' <param name="idiomaBuscado">Idioma a buscar</param>
        ''' <returns>Idioma dentro de la lista</returns>
        Public Function BuscarIdioma(ByVal idiomas As IList(Of Idioma), ByVal idiomaBuscado As Idioma) As Idioma
            Dim retorno As Idioma = Nothing

            If idiomaBuscado IsNot Nothing Then
                For Each idioma As Idioma In idiomas
                    If idioma.Id.Equals(idiomaBuscado.Id) Then
                        retorno = idioma
                        Exit For
                    End If
                Next
            End If

            Return retorno
        End Function

        ''' <summary>
        ''' Método que busca un estado dentro de una lista de remitentes
        ''' </summary>
        ''' <param name="medios">Lista de Medios</param>
        ''' <param name="medioBuscado">Medio a buscar</param>
        ''' <returns>Medio dentro de la lista</returns>
        Public Function BuscarMedios(ByVal medios As IList(Of Medio), ByVal medioBuscado As Medio) As Medio
            Dim retorno As Medio = Nothing

            If medioBuscado IsNot Nothing Then
                For Each remitente As Medio In medios
                    If remitente.Id.Equals(medioBuscado.Id) Then
                        retorno = remitente
                        Exit For
                    End If
                Next
            End If

            Return retorno
        End Function

        ''' <summary>
        ''' Método que busca un estado dentro de una lista de remitentes
        ''' </summary>
        ''' <param name="medios">Lista de Medios</param>
        ''' <param name="medioBuscado">Medio a buscar</param>
        ''' <returns>Medio dentro de la lista</returns>
        Public Function BuscarMedio(ByVal medios As IList(Of Medio), ByVal medioBuscado As Medio) As Medio
            Dim retorno As Medio = Nothing

            If medioBuscado IsNot Nothing Then
                For Each medio As Medio In medios
                    If medio.Id.Equals(medioBuscado.Id) Then
                        retorno = medio
                        Exit For
                    End If
                Next
            End If

            Return retorno
        End Function

        ''' <summary>
        ''' Método que busca un estado dentro de una lista de receptores
        ''' </summary>
        ''' <param name="receptores">Lista de receptores</param>
        ''' <param name="receptorBuscado">Receptor a buscar</param>
        ''' <returns>Receptor dentro de la lista</returns>
        Public Function BuscarReceptor(ByVal receptores As IList(Of Usuario), ByVal receptorBuscado As String) As Usuario
            Dim retorno As Usuario = Nothing

            If receptorBuscado IsNot Nothing Then
                For Each receptor As Usuario In receptores
                    If receptor.Iniciales.Equals(receptorBuscado) Then
                        retorno = receptor
                        Exit For
                    End If
                Next
            End If

            Return retorno
        End Function

        ''' <summary>
        ''' Método que busca un estado dentro de una lista de remitentes
        ''' </summary>
        ''' <param name="remitentes">Lista de remitentes</param>
        ''' <param name="remitenteBuscado">Remitente a buscar</param>
        ''' <returns>Remitente dentro de la lista</returns>
        Public Function BuscarRemitente(ByVal remitentes As IList(Of Remitente), ByVal remitenteBuscado As Remitente) As Remitente
            Dim retorno As Remitente = Nothing

            If remitenteBuscado IsNot Nothing Then
                For Each remitente As Remitente In remitentes
                    If remitente.Id.Equals(remitenteBuscado.Id) Then
                        retorno = remitente
                        Exit For
                    End If
                Next
            End If

            Return retorno
        End Function

        ''' <summary>
        ''' Método que busca un estado dentro de una lista de categorias
        ''' </summary>
        ''' <param name="categorias">Lista de categorias</param>
        ''' <param name="categoriaBuscado">Categoria a buscar</param>
        ''' <returns>Categoria dentro de la lista</returns>
        Public Function BuscarCategoria(ByVal categorias As IList(Of Categoria), ByVal categoriaBuscado As Categoria) As Categoria
            Dim retorno As Categoria = Nothing

            If categoriaBuscado IsNot Nothing Then
                For Each categoria As Categoria In categorias
                    If categoria.Id.Equals(categoriaBuscado.Id) Then
                        retorno = categoria
                        Exit For
                    End If
                Next
            End If

            Return retorno
        End Function

        ''' <summary>
        ''' Método que busca un estado dentro de una lista de Monedas
        ''' </summary>
        ''' <param name="monedas">Lista de idiomas</param>
        ''' <param name="monedaBuscado">Moneda a buscar</param>
        ''' <returns>Moneda dentro de la lista</returns>
        Public Function BuscarMoneda(ByVal monedas As IList(Of Moneda), ByVal monedaBuscado As Moneda) As Moneda
            Dim retorno As Moneda = Nothing

            If monedaBuscado IsNot Nothing Then
                For Each moneda As Moneda In monedas
                    If moneda.Id.Equals(monedaBuscado.Id) Then
                        retorno = moneda
                        Exit For
                    End If
                Next
            End If

            Return retorno
        End Function

        Public Sub CargarComboBoxTiempo(ByVal horas As Object, ByVal minutos As Object)
            DirectCast(horas, ComboBox).Items.Add("")
            DirectCast(minutos, ComboBox).Items.Add("")

            For i As Integer = 1 To 24
                DirectCast(horas, ComboBox).Items.Add(i.ToString())
            Next

            For i As Integer = 0 To 59
                DirectCast(minutos, ComboBox).Items.Add(i.ToString())
            Next
        End Sub

        ''' <summary>
        ''' Método que busca un estado dentro de una lista de TipoClientes
        ''' </summary>
        ''' <param name="tipoClientes">Lista de TipoClientes</param>
        ''' <param name="tipoClienteBuscado">TipoCliente a buscar</param>
        ''' <returns>TipoCliente dentro de la lista</returns>
        Public Function BuscarTipoCliente(ByVal tipoClientes As IList(Of TipoCliente), ByVal tipoClienteBuscado As TipoCliente) As TipoCliente
            Dim retorno As TipoCliente = Nothing

            If tipoClienteBuscado IsNot Nothing Then
                For Each tipoCliente As TipoCliente In tipoClientes
                    If tipoCliente.Id.Equals(tipoClienteBuscado.Id) Then
                        retorno = tipoCliente
                        Exit For
                    End If
                Next
            End If

            Return retorno
        End Function

        ''' <summary>
        ''' Método que busca un estado dentro de una lista de Tarifas
        ''' </summary>
        ''' <param name="tarifas">Lista de Tarifas</param>
        ''' <param name="tarifaBuscado">Tarifa a buscar</param>
        ''' <returns>Tarifa dentro de la lista</returns>
        Public Function BuscarTarifa(ByVal tarifas As IList(Of Tarifa), ByVal tarifaBuscado As Tarifa) As Tarifa
            Dim retorno As Tarifa = Nothing

            If tarifaBuscado IsNot Nothing Then
                For Each tarifa As Tarifa In tarifas
                    If tarifa.Id.Equals(tarifaBuscado.Id) Then
                        retorno = tarifa
                        Exit For
                    End If
                Next
            End If

            Return retorno
        End Function

        ''' <summary>
        ''' Método que busca un estado dentro de una lista de Etiquetas
        ''' </summary>
        ''' <param name="etiquetas">Lista de Etiquetas</param>
        ''' <param name="etiquetaBuscado">Etiqueta a buscar</param>
        ''' <returns>Etiqueta dentro de la lista</returns>
        Public Function BuscarEtiqueta(ByVal etiquetas As IList(Of Etiqueta), ByVal etiquetaBuscado As Etiqueta) As Etiqueta
            Dim retorno As Etiqueta = Nothing

            If etiquetaBuscado IsNot Nothing Then
                For Each tarifa As Etiqueta In etiquetas
                    If tarifa.Id.Equals(etiquetaBuscado.Id) Then
                        retorno = tarifa
                        Exit For
                    End If
                Next
            End If

            Return retorno
        End Function

        ''' <summary>
        ''' Método que busca un estado dentro de una lista de DetallesPagos
        ''' </summary>
        ''' <param name="detallesPagos">Lista de DetallesPagos</param>
        ''' <param name="detallePagoBuscado">DetallePago a buscar</param>
        ''' <returns>DetallePago dentro de la lista</returns>
        Public Function BuscarDetallePago(ByVal detallesPagos As IList(Of DetallePago), ByVal detallePagoBuscado As DetallePago) As DetallePago
            Dim retorno As DetallePago = Nothing

            If detallePagoBuscado IsNot Nothing Then
                For Each detallePago As DetallePago In detallesPagos
                    If detallePago.Id.Equals(detallePagoBuscado.Id) Then
                        retorno = detallePago
                        Exit For
                    End If
                Next
            End If

            Return retorno
        End Function

        ''' <summary>
        ''' Metodo que deshabilita las fechas en un calendario desde el dia 1 hasta el dia pasado por parametro
        ''' </summary>
        ''' <param name="calendario">Calendario a modificar</param>
        ''' <param name="dia">Dia hasta que se deshabilitaran las fechas</param>
        Public Sub DeshabilitarDias(ByVal calendario As DatePicker, ByVal dia As DateTime)
            If calendario.SelectedDate Is Nothing OrElse calendario.SelectedDate <= dia Then
                calendario.SelectedDate = dia.AddDays(1)
                calendario.Text = String.Empty
            End If

            calendario.BlackoutDates.Add(New CalendarDateRange(New DateTime(1, 1, 1), dia))
        End Sub

        ''' <summary>
        ''' Método que busca un concepto dentro de una lista de conceptos
        ''' </summary>
        ''' <param name="estados">Lista de conceptos</param>
        ''' <param name="estadoBuscado">concepto a buscar</param>
        ''' <returns>Concepto dentro de la lista</returns>
        Public Function BuscarConcepto(ByVal conceptos As IList(Of Concepto), ByVal conceptoBuscado As Concepto) As Concepto
            Dim retorno As Concepto = Nothing

            If conceptoBuscado IsNot Nothing Then
                For Each concepto As Concepto In conceptos
                    If concepto.Id.Equals(conceptoBuscado.Id) Then
                        retorno = concepto
                        Exit For
                    End If
                Next
            End If

            Return retorno
        End Function

        ''' <summary>
        ''' Método que busca un concepto dentro de una lista de conceptos
        ''' </summary>
        ''' <param name="estados">Lista de conceptos</param>
        ''' <param name="estadoBuscado">concepto a buscar</param>
        ''' <returns>Concepto dentro de la lista</returns>
        'Public Function BuscarDepartamentoContacto(ByVal departamentoBuscado As String) As String
        '    Dim retorno As String = "NGN"
        '    Select Case departamentoBuscado
        '        Case "LEG"
        '            retorno = Recursos.Etiquetas.cbiLegal
        '            Exit Select
        '        Case "MAR"
        '            retorno = Recursos.Etiquetas.cbiMarcas
        '            Exit Select
        '        Case "PAT"
        '            retorno = Recursos.Etiquetas.cbiPatentes
        '            Exit Select
        '        Case "ADM"
        '            retorno = Recursos.Etiquetas.cbiAdministracion
        '            Exit Select
        '        Case Else
        '            Exit Select
        '    End Select

        '    Return retorno
        'End Function

        'Public Function BuscarFuncionContacto(ByVal funcionBuscada As String) As String
        '    Dim retorno As String = "NGN"
        '    Select Case funcionBuscada
        '        Case "A"
        '            retorno = Recursos.Etiquetas.cbiSoloAdministracion
        '            Exit Select
        '        Case "EDO"
        '            retorno = Recursos.Etiquetas.cbiSoloEstadoDeCuenta
        '            Exit Select
        '        Case "M"
        '            retorno = Recursos.Etiquetas.cbiSoloMarca
        '            Exit Select
        '        Case "P"
        '            retorno = Recursos.Etiquetas.cbiSoloPatente
        '            Exit Select
        '        Case "AM"
        '            retorno = Recursos.Etiquetas.cbiAdministracionYPatentes
        '            Exit Select
        '        Case "AP"
        '            retorno = Recursos.Etiquetas.cbiAdministracionYPatentes
        '            Exit Select
        '        Case "MP"
        '            retorno = Recursos.Etiquetas.cbiMarcasYPatentes
        '            Exit Select
        '        Case "AMP"
        '            retorno = Recursos.Etiquetas.cbiAdministracionMarcasPatentes
        '            Exit Select
        '        Case Else
        '            Exit Select
        '    End Select

        '    Return retorno
        'End Function

        Public Function transformarDepartamento(ByVal departamento As String) As String
            Dim retorno As String = ""
            Select Case departamento
                Case "Legal"
                    retorno = "LEG"
                    Exit Select
                Case "Marcas"
                    retorno = "MAR"
                    Exit Select
                Case "Patentes"
                    retorno = "PAT"
                    Exit Select
                Case "Administración"
                    retorno = "ADM"
                    Exit Select
                Case Else
                    Exit Select
            End Select
            Return retorno
        End Function

        Public Function transformarFuncion(ByVal funcion As String) As String
            Dim retorno As String = ""
            Select Case funcion
                Case "Sólo Administración"
                    retorno = "A"
                    Exit Select
                Case "Sólo Estado De Cuenta"
                    retorno = "EDO"
                    Exit Select
                Case "Sólo Marcas"
                    retorno = "M"
                    Exit Select
                Case "Sólo Patentes"
                    retorno = "P"
                    Exit Select
                Case "Administración y Marcas"
                    retorno = "AM"
                    Exit Select
                Case "Administración y Patentes"
                    retorno = "AP"
                    Exit Select
                Case "Marcas y Patentes"
                    retorno = "MP"
                    Exit Select
                Case "Administración, Marcas y Patentes"
                    retorno = "AMP"
                    Exit Select
                Case Else
                    Exit Select
            End Select
            Return retorno
        End Function

        'Facturacion
        Public Function BuscarTipo(ByVal tipo As Char) As String
            Dim retorno As String
            If Recursos.Etiquetas.cbiMarcas(0).Equals(tipo) Then
                retorno = Recursos.Etiquetas.cbiMarcas
            ElseIf Recursos.Etiquetas.cbiPatentes(0).Equals(tipo) Then
                retorno = Recursos.Etiquetas.cbiPatentes
            ElseIf Recursos.Etiquetas.cbiCantidades(0).Equals(tipo) Then
                retorno = Recursos.Etiquetas.cbiCantidades
            ElseIf Recursos.Etiquetas.cbiExterna(0).Equals(tipo) Then
                retorno = Recursos.Etiquetas.cbiExterna
            Else
                retorno = Recursos.Etiquetas.cbiNinguno
            End If

            Return retorno
        End Function

        Public Function BuscarLocalidad(ByVal localidad As Char) As String
            Dim retorno As String
            If Recursos.Etiquetas.cbiNacional(0).Equals(localidad) Then
                retorno = Recursos.Etiquetas.cbiNacional
            ElseIf Recursos.Etiquetas.cbiInternacional(0).Equals(localidad) Then
                retorno = Recursos.Etiquetas.cbiInternacional
            Else
                retorno = Recursos.Etiquetas.cbiNacional
            End If

            Return retorno
        End Function

        Public Function BuscarEstructurasMultiples(ByVal estructurasmultiples As Char) As String
            Dim retorno As String
            If Recursos.Etiquetas.cbiMarcaCompleta(0).Equals(estructurasmultiples) Then
                retorno = Recursos.Etiquetas.cbiMarcaCompleta
            ElseIf Recursos.Etiquetas.cbiMarcaRegSolicitud(0).Equals(estructurasmultiples) Then
                retorno = Recursos.Etiquetas.cbiMarcaRegSolicitud
            ElseIf Recursos.Etiquetas.cbiPatenteRegSolicitud(0).Equals(estructurasmultiples) Then
                retorno = Recursos.Etiquetas.cbiPatenteRegSolicitud
            ElseIf Recursos.Etiquetas.cbiPatenteSolicitud(0).Equals(estructurasmultiples) Then
                retorno = Recursos.Etiquetas.cbiPatenteSolicitud
            Else
                retorno = Recursos.Etiquetas.cbiNinguno
            End If

            Return retorno
        End Function

        Public Function BuscarTipoDesgSer(ByVal TipoDesgSer As Char) As String
            Dim retorno As String
            If Recursos.Etiquetas.cbiGastos(0).Equals(TipoDesgSer) Then
                retorno = Recursos.Etiquetas.cbiGastos
            ElseIf Recursos.Etiquetas.cbiHonorarios(0).Equals(TipoDesgSer) Then
                retorno = Recursos.Etiquetas.cbiHonorarios
            Else
                retorno = Recursos.Etiquetas.cbiNinguno
            End If
            Return retorno
        End Function

        Public Function BuscarTarifa2(ByVal tarifas As IList(Of Tarifa), ByVal tarifaBuscado As Tarifa2) As Tarifa2
            Dim retorno As Tarifa2 = Nothing
            Dim retorno2 As Tarifa = Nothing

            If tarifaBuscado IsNot Nothing Then
                For Each tarifa As Tarifa In tarifas
                    If tarifa.Id.Equals(tarifaBuscado.Id) Then
                        retorno2 = tarifa
                        retorno.Descripcion = retorno2.Descripcion
                        retorno.Id = retorno2.Id
                        Exit For
                    End If
                Next
            End If

            Return retorno
        End Function

        Public Function BuscarServicio(ByVal servicios As IList(Of Servicio), ByVal servicioBuscado As Servicio) As Servicio
            Dim retorno As Servicio = Nothing

            If servicioBuscado IsNot Nothing Then
                For Each servicio As Servicio In servicios
                    If servicio.Id.Equals(servicioBuscado.Id) Then
                        retorno = servicio
                        Exit For
                    End If
                Next
            End If

            Return retorno
        End Function

        Public Function BuscarAsociado(ByVal Asociados As IList(Of Asociado), ByVal AsociadoBuscado As Asociado) As Asociado
            Dim retorno As Asociado = Nothing

            If AsociadoBuscado IsNot Nothing Then
                For Each Asociado As Asociado In Asociados
                    If Asociado.Id.Equals(AsociadoBuscado.Id) Then
                        retorno = Asociado
                        Exit For
                    End If
                Next
            End If

            Return retorno
        End Function

        Public Function BuscarBancoG(ByVal BancoGs As IList(Of BancoG), ByVal BancoGBuscado As BancoG) As BancoG
            Dim retorno As BancoG = Nothing

            If BancoGBuscado IsNot Nothing Then
                For Each BancoG As BancoG In BancoGs
                    If BancoG.Id.Equals(BancoGBuscado.Id) Then
                        retorno = BancoG
                        Exit For
                    End If
                Next
            End If

            Return retorno
        End Function


        Public Function transformarEstMul(ByVal funcion As String) As String
            Dim retorno As String = ""
            Select Case funcion
                Case "Marca Completa"
                    retorno = "M1"
                    Exit Select
                Case "Marca Reg o Solicitud"
                    retorno = "M2"
                    Exit Select
                Case "Patente Reg o Solicitud"
                    retorno = "P1"
                    Exit Select
                Case "Patente Solicitud"
                    retorno = "P2"
                    Exit Select
                Case Else
                    retorno = "NU"
                    Exit Select
            End Select
            Return retorno
        End Function

        Public Sub CalcularSaldoAsociado(ByVal casociado As asociado, ByVal p_dias As Integer, ByVal p_venmay_B As Double, ByVal p_venmay_D As Double, ByVal p_venmen_B As Double, ByVal p_venmen_D As Double, ByVal p_total_B As Double, ByVal p_total_D As Double)
            Mouse.OverrideCursor = Cursors.Wait

            Dim _asociadosServicios As IAsociadoServicios
            Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)

            Dim asociadoaux As New Asociado
            Dim asociado As Asociado = Nothing
            Dim i As Boolean = False

            'Dim p_venmay_B, p_venmay_D, p_venmen_B, p_venmen_D, p_total_B, p_total_D
            p_venmay_B = 0
            p_venmay_D = 0
            p_venmen_B = 0
            p_venmen_D = 0
            p_total_B = 0
            p_total_D = 0

            Mouse.OverrideCursor = Cursors.Wait
            asociadoaux.Id = casociado
            asociados = Me._asociadosServicios.ObtenerAsociadosFiltro(asociadoaux)
            If asociados IsNot Nothing Then

                Dim _FacFacturaPendienteConGruServicios As IFacFacturaPendienteConGruServicios
                Me._FacFacturaPendienteConGruServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaPendienteConGruServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaPendienteConGruServicios")), IFacFacturaPendienteConGruServicios)
                Dim FacFacturaPendienteConGru As list(Of FacFacturaPendienteConGru) = Nothing
                FacFacturaPendienteConGru = Me._FacFacturaPendienteConGruServicios.ObtenerFacFacturaPendienteConGruFiltro(asociadoaux)
                If FacFacturaPendienteConGru IsNot Nothing Then
                    If FacFacturaPendienteConGru.count > 0 Then
                        For i As Integer = 1 To FacFacturaPendienteConGru
                            If FacFacturaPendienteConGru(i).dias > p_dias Then
                                p_venmay_D = p_venmay_D + FacFacturaPendienteConGru(i).saldo '2
                                p_total_D = p_total_D + p_venmay_D

                                p_venmay_B = p_venmay_B + FacFacturaPendienteConGru(i).saldo_bf '1
                                p_total_B = p_total_B + p_venmay_B
                            Else
                                p_venmen_D = p_venmen_D + FacFacturaPendienteConGru(i).saldo '4
                                p_total_D = p_total_D + p_venmen_D

                                p_venmen_B = p_venmen_B + FacFacturaPendienteConGru(i).saldo_bf '3
                                p_total_B = p_total_B + p_venmen_B
                            End If
                        Next
                    End If

                    Dim _FacVistaFacturacionCxpInternaServicios As IFacVistaFacturacionCxpInternaServicios
                    Me._FacVistaFacturacionCxpInternaServicios = DirectCast(Activator.GetObject(GetType(IFacVistaFacturacionCxpInternaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacVistaFacturacionCxpInternaServicios")), IFacVistaFacturacionCxpInternaServicios)
                    Dim FacVistaFacturacionCxpInterna As list(Of FacVistaFacturacionCxpInterna) = Nothing
                    FacVistaFacturacionCxpInterna = Me._FacVistaFacturacionCxpInternaServicios.ObtenerFacVistaFacturacionCxpInternaFiltro(asociadoaux)

                End If

            End If

            Mouse.OverrideCursor = Nothing
        End Sub

    End Class
End Namespace
