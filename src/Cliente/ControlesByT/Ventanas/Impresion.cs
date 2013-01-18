using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;

namespace Trascend.Bolet.ControlesByT.Ventanas
{
    public partial class Impresion : Form
    {
        //private bool _clickImprimir = false;

        private StreamReader _reader;
        private Font _fuente;
        private string _ruta;
        private int _tamanoFuente = 10;
        private int _anchoPagina;
        private int _altoPagina;

        //public bool ClickImprimir
        //{
        //    get { return _clickImprimir; }
        //    set { _clickImprimir = value; }
        //}

        public Impresion(string titulo, string folio, string ruta)
        {
            InitializeComponent();

            this._folio.ScrollBars = ScrollBars.Vertical;
            this.Text = titulo;
            this._folio.Text = folio;

            this._folio.SelectionStart = this._folio.Text.Length;
            this._folio.SelectionLength = 0;

            this._ruta = ruta;
            this._folio.AcceptsReturn = true;

        }

        private void _btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _imprimir_Click(object sender, EventArgs e)
        {
            try
            {
                //Genero el .txt utilizado para ejecutar el .bat
                System.IO.File.WriteAllText(_ruta, this._folio.Text);
                //System.IO.File.WriteAllText(@"C:\Users\KRUSTY\Documents\print.txt", this._folio.Text);
                //System.IO.File.WriteAllText(@"C:\Users\KRUSTY\Documents\print.txt", this._folio.Text);


                _reader = new StreamReader(_ruta);
                //_reader = new StreamReader(@"C:\Users\KRUSTY\Documents\print.txt");
                //_fuente = new Font("Courier New, Western, 9, regular", 10);
                _fuente = new Font("Courier New", _tamanoFuente, FontStyle.Regular);


                PrintDialog printDlg = new PrintDialog();
                printDlg.AllowSelection = true;
                printDlg.AllowSomePages = true;
                //Llamamos al dialogo de impresión
                if (printDlg.ShowDialog() == DialogResult.OK)
                {
                    PrintDocument documentoAImprimir = new PrintDocument();
                    //Agregamos el manejador de impresion
                    documentoAImprimir.PrintPage += new PrintPageEventHandler(this.ManejadorDeArchivoDeTexto);
                    documentoAImprimir.PrinterSettings = printDlg.PrinterSettings;
                    
                    
                    //Calculo de proporciones de la hoja "Oficio Vzla"
                    //foreach (PaperSize size in documentoAImprimir.PrinterSettings.PaperSizes)
                    //{
                    //    if (size.PaperName.Equals ("Oficio Vzla"))
                    //    {
                    //        string a = size.PaperName;
                    //        _anchoPagina = size.Width;
                    //        _altoPagina = size.Height;
                    //    }
                    //}

                    //Tamaño customizado (NO DESCOMENTAR)
                    //documentoAImprimir.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Oficio Vzla", _ancho, _alto);
                    
                    
                    //Mandamos a imprimir 
                    documentoAImprimir.Print();

                    this.Close();
                }

            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (UnauthorizedAccessException ex)
            {
                throw ex;
            }
            finally
            {
                if (null != _reader)
                {
                    _reader.Close();
                    System.IO.File.Delete(_ruta);
                }
            }

        }

        /// <summary>
        /// Método que se encarga de todo el manejo del archivo de texto para realizar
        /// la impresión
        /// </summary>
        /// <param name="sender">objeto que envia la petición</param>
        /// <param name="ppeArgs">Argumentos del evento para imprimir</param>
        private void ManejadorDeArchivoDeTexto(object sender, PrintPageEventArgs ppeArgs)
        {
            //Obtiene el objeto de graficos
            Graphics g = ppeArgs.Graphics;
            float lineasPorPagina = 0;
            float posicionY = 0;
            int count = 0;
            float margenIzquierdo = 0;
            float margenDerecho = 0;
            string linea = null;
            //Calculate the lines per page on the basis of the height of the page and the height of the font  
            lineasPorPagina = ppeArgs.MarginBounds.Height / _fuente.GetHeight(g);
            //Now read lines one by one, using StreamReader  
            while (lineasPorPagina > count && ((linea = _reader.ReadLine()) != null))
            {
                //Calculate the starting position  
                posicionY = margenDerecho + (count * _fuente.GetHeight(g));
                //Draw text  
                g.DrawString(linea, _fuente, Brushes.Black, margenIzquierdo, posicionY, new StringFormat());
                //Move to next line  
                count++;
            }
            //If PrintPageEventArgs has more pages to print  
            if (linea != null)
            {
                ppeArgs.HasMorePages = true;
            }
            else
            {
                ppeArgs.HasMorePages = false;
            }
        }

    }
}
