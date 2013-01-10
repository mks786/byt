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


                _reader = new StreamReader(_ruta);
                //reader = new StreamReader(@"C:\Users\KRUSTY\Documents\print.txt");
                _fuente = new Font("Verdana", 10);


                PrintDialog printDlg = new PrintDialog();
                printDlg.AllowSelection = true;
                printDlg.AllowSomePages = true;
                //Llamamos al dialogo de impresión
                if (printDlg.ShowDialog() == DialogResult.OK)
                {
                    PrintDocument pd = new PrintDocument();
                    //Agregamos el manejador de impresion
                    pd.PrintPage += new PrintPageEventHandler(this.ManejadorDeArchivoDeTexto);
                    pd.PrinterSettings = printDlg.PrinterSettings;
                    //Mandamos a imprimir 
                    pd.Print();


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
                    System.IO.File.Delete(@"C:\print.txt");
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
            //Get the Graphics object  
            Graphics g = ppeArgs.Graphics;
            float lineasPorPagina = 0;
            float posicionY = 0;
            int count = 0;
            //Read margins from PrintPageEventArgs  
            //float margenIzquierdo = ppeArgs.MarginBounds.Left;
            //float margenDerecho = ppeArgs.MarginBounds.Top;
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
