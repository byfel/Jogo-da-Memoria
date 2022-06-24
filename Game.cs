using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JogodaMemoria
{
    public partial class Game : Form
    {
        // firstClicked aponta para o primeiro controle Label
        // que o jogador clica, mas será nulo
        // se o player ainda não clicou em um marcador
        Label firstClicked = null;

        // secondClicked aponta para o segundo controle Label
        // que o jogador clica
        Label secondClicked = null;


        // Use este objeto Random para escolher ícones aleatórios para os quadrados
        Random random = new Random();

        // Cada uma dessas letras é um ícone interessante
        // na fonte Webdings,
        // e cada ícone aparece duas vezes nesta lista
        List<string> icons = new List<string>()
    {
        "!", "!", "N", "N", ",", ",", "k", "k",
        "b", "b", "v", "v", "w", "w", "z", "z"
    };

        /// <resumo>
        /// Atribui cada ícone da lista de ícones a um quadrado aleatório
        /// </resumo>
        private void AssignIconsToSquares()
        {
            // O TableLayoutPanel tem 16 rótulos,
            // e a lista de ícones tem 16 ícones,
            // então um ícone é puxado aleatoriamente da lista
            // e adicionado a cada rótulo
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        private void Game_Load(object sender, EventArgs e)
        {

        }

        /// <resumo>
        /// O evento Click de cada rótulo é tratado por este manipulador de eventos
        /// </summary>
        /// <param name="sender">O rótulo que foi clicado</param>
        /// <param name="e"></param>
        private void Label1_click(object sender, EventArgs e)
        {
            // O cronômetro é ativado apenas após dois não correspondentes
            // ícones foram mostrados ao jogador,
            // então ignore qualquer clique se o cronômetro estiver em execução
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // Se o marcador clicado for preto, o player clicou
                // um ícone que já foi revelado --
                // ignora o clique
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                // Se firstClicked for null, este é o primeiro ícone
                // no par que o jogador clicou,
                // então defina firstClicked para o rótulo que o jogador
                // clicado, muda sua cor para preto e retorna
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                // Se o jogador chegar até aqui, o cronômetro não está
                // running e firstClicked não é nulo,
                // então este deve ser o segundo ícone que o jogador clicou
                // Define sua cor para preto
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                CheckForWinner();

                // Se o jogador clicou em dois ícones correspondentes, mantenha-os
                // preto e redefinir firstClicked e secondClicked
                // para que o jogador possa clicar em outro ícone
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                // Se o jogador clicou em dois ícones correspondentes, mantenha-os
                // preto e redefinir firstClicked e secondClicked
                // para que o jogador possa clicar em outro ícone
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }
                // Se o jogador chegar até aqui, o jogador
                // clicou em dois ícones diferentes, então inicie o
                // timer (que vai esperar três quartos de
                // um segundo e, em seguida, ocultar os ícones)
                timer1.Start();
            }
        }

        /// <resumo>
        /// Verifica cada ícone para ver se ele corresponde, por
        /// comparando sua cor de primeiro plano com sua cor de fundo.
        /// Se todos os ícones corresponderem, o jogador ganha
        /// </summary>
        private void CheckForWinner()
        {
            // Passa por todos os rótulos no TableLayoutPanel,
            // verificando cada um para ver se seu ícone corresponde
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // Se o loop não retornou, não encontrou
            // qualquer ícone sem correspondência
            // Isso significa que o usuário ganhou. Mostrar uma mensagem e fechar o formulário
            MessageBox.Show("You matched all the icons!", "Congratulations");
            Close();
        }


        public Game()
        {
            InitializeComponent();
            AssignIconsToSquares();
            
        }

        /// <resumo>
        /// Este cronômetro é iniciado quando o jogador clica
        /// dois ícones que não combinam,
        /// então conta três quartos de segundo
        /// e depois se desliga e oculta os dois ícones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Stop the timer
            timer1.Stop();

            // Hide both icons
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // Redefinir firstClicked e secondClicked
            // então na próxima vez que um rótulo for
            // clicado, o programa sabe que é o primeiro clique
            firstClicked = null;
            secondClicked = null;
        }
    }
}
