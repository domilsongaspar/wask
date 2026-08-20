using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace WASK
{
    public partial class Form1 : Form
    {
        private TcpClient cliente;
        private bool conectado = false;
        private bool ligado = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Conectar()
        {
            // Endereço IP e porta reservada para a comunicação com o módulo Ethernet Shield
            string enderecoIP = "192.168.8.20";
            int porta = 80;

            try
            {
                cliente = new TcpClient(enderecoIP, porta);
                conectado = true;
                MessageBox.Show("Conectado!");
                EnviarComando("on", false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void Desconectar()
        {
            if (conectado)
            {
                EnviarComando("end");
                // Fecha a conexão
                cliente.Close();
                conectado = false;
            }
        }

        private void EnviarComando(string comando, bool feedback = true)
        {
            if (conectado)
            {
                NetworkStream stream = cliente.GetStream();
                byte[] dados = Encoding.ASCII.GetBytes(comando);

                // Envia os dados para o dispositivo
                stream.Write(dados, 0, dados.Length);
                stream.Flush();

                if (feedback)
                {
                    MessageBox.Show("Enviado!");
                }
            } else
            {
                MessageBox.Show("Conexão necessária!");
            }
        }

        private void Botao_Conectar_Desconectar(object sender, EventArgs e)
        {
            if (!conectado)
            {
                Conectar();

                if (conectado)
                {
                    //Muda as características do Botão
                    conectar_desconectar.Text = "Desconectar";
                    conectar_desconectar.BackColor = Color.Red;
                    conectar_desconectar.ForeColor = Color.White;
                }
            }
            else
            {
                Desconectar();
                
                if (!conectado)
                {
                    //Muda as características do Botão
                    conectar_desconectar.Text = "CONECTAR";
                    conectar_desconectar.BackColor = Color.Blue;
                    conectar_desconectar.ForeColor = Color.White;
                }
            }
        }

        private void LigarDesligar(object sender, EventArgs e)
        {
            if (!ligado)
            {
                EnviarComando("on", false);
                ligado = true;
            } else
            {
                EnviarComando("off", false);
                ligado = false;
            }
        }
    }
}
