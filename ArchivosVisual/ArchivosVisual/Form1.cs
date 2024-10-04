using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArchivosVisual
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Abrimos el archivo para escribir en modo binario
            using (FileStream mArchivoEscritor = new FileStream("datos.dat", FileMode.OpenOrCreate, FileAccess.Write))
            using (BinaryWriter Escritor = new BinaryWriter(mArchivoEscritor))
            {
                // Obtenemos los datos del formulario
                string nombre = txtNombre.Text;
                int edad = int.Parse(txtEdad.Text);
                int nota = int.Parse(txtNota.Text);
                char genero = char.Parse(txtGenero.Text);

                // Escribimos los datos en el archivo binario
                Escritor.Write(nombre.Length); // Longitud del nombre
                Escritor.Write(nombre.ToCharArray()); // Nombre como arreglo de caracteres
                Escritor.Write(edad);
                Escritor.Write(nota);
                Escritor.Write(genero);

                MessageBox.Show("Datos guardados correctamente.");
            }
        }

        private void btnLeer_Click(object sender, EventArgs e)
        {
            // Abrimos el archivo para leer en modo binario
            using (FileStream mArchivoLector = new FileStream("datos.dat", FileMode.Open, FileAccess.Read))
            using (BinaryReader Lector = new BinaryReader(mArchivoLector))
            {
                while (mArchivoLector.Position != mArchivoLector.Length)
                {
                    int length = Lector.ReadInt32(); // Leemos la longitud del nombre
                    char[] nombreArray = Lector.ReadChars(length); // Leemos el nombre
                    string nombre = new string(nombreArray); // Convertimos a string
                    int edad = Lector.ReadInt32();
                    int nota = Lector.ReadInt32();
                    char genero = Lector.ReadChar();

                    // Mostramos los datos leídos en el cuadro de texto o en consola
                    MessageBox.Show($"Nombre: {nombre}\nEdad: {edad}\nNota: {nota}\nGénero: {genero}");
                }
            }
        }
    }
}
