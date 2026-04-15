using System;
using System.Windows.Forms;

namespace JogoDaVelhaApp
{
    public partial class TelaPrincipal : Form
    {
        // Variável para controlar de quem é a vez. true = X, false = O.
        private bool _vezDoX = true;

        // Contador para saber se deu empate (chegou a 9 jogadas e ninguém ganhou)
        private int _jogadas = 0;

        public TelaPrincipal()
        {
            InitializeComponent();
        }

        // Este é o evento COMPARTILHADO. Os 9 botões do tabuleiro chamam este mesmo código!
        private void BotaoGrid_Click(object sender, EventArgs e)
        {
            // O "sender" é quem disparou o clique. Convertendo ele, sabemos exatamente em qual botão o
            //usuário clicou.
            Button botaoClicado = (Button)sender;

            // Se o botão já tiver texto (já foi jogado), não faz nada (ignora o clique)
            if (botaoClicado.Text != "") return;

            // Se for a vez do X, escreve "X", senão, escreve "O"
            botaoClicado.Text = _vezDoX ? "X" : "O";
            if (_vezDoX == true) //Condição para trocar as cores do X e do O
            {
                botaoClicado.ForeColor = System.Drawing.Color.Tomato; // Cor do X (Vermelho claro)
            }
            else
            {
                botaoClicado.ForeColor = System.Drawing.Color.CornflowerBlue; // Cor do O (Azul claro)
            }
            _jogadas++;
            // Verifica se essa jogada fez alguém ganhar
            if (VerificarVencedor())
            {
                // Mostra a mensagem com o vencedor da rodada
                string vencedor = _vezDoX ? "X" : "O";
                MessageBox.Show($"Fim de jogo! Jogador {vencedor} venceu!", "Temos um Vencedor",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                ZerarTabuleiro(); // Limpa a tela para a próxima partida
            }
            // Se ninguém ganhou e já foram 9 jogadas, então deu velha!
            else if (_jogadas == 9)
            {
                MessageBox.Show("Deu Velha! O jogo empatou.", "Empate", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                ZerarTabuleiro();
            }
            // Se ninguém ganhou e ainda tem espaço, passa a vez para o outro
            else
            {
                _vezDoX = !_vezDoX; // Inverte: se era true vira false, se era false vira true
                //lblVez.Text = $"Vez do Jogador: {(_vezDoX ? "X" : "O")}";

                // 1. Atualiza apenas a letra na nova Label
                lblJogadorAtual.Text = _vezDoX ? "X" : "O";

                // 2. Muda a cor da letra dependendo de quem é a vez
                if (_vezDoX == true)
                {
                    lblJogadorAtual.ForeColor = System.Drawing.Color.Tomato; // Mesma cor do X no tabuleiro
                }
                else
                {
                    lblJogadorAtual.ForeColor = System.Drawing.Color.CornflowerBlue; // Mesma cor do O no tabuleiro
                }
            }
        }
        // Método que checa todas as 8 possibilidades de vitória
        private bool VerificarVencedor()
        {
            // Verificação das 3 Linhas horizontais
            if (ChecarTrinca(btn1, btn2, btn3)) return true;
            if (ChecarTrinca(btn4, btn5, btn6)) return true;
            if (ChecarTrinca(btn7, btn8, btn9)) return true;
            // Verificação das 3 Colunas verticais
            if (ChecarTrinca(btn1, btn4, btn7)) return true;
            if (ChecarTrinca(btn2, btn5, btn8)) return true;
            if (ChecarTrinca(btn3, btn6, btn9)) return true;
            // Verificação das 2 Diagonais

            if (ChecarTrinca(btn1, btn5, btn9)) return true;
            if (ChecarTrinca(btn3, btn5, btn7)) return true;
            // Se não fechou nenhuma trinca, retorna falso
            return false;
        }
        // Método auxiliar que recebe 3 botões e verifica se os 3 têm o mesmo texto (e não estão vazios)
        private bool ChecarTrinca(Button b1, Button b2, Button b3)
        {
            // Se o primeiro botão estiver vazio, já sabemos que não tem trinca aqui
            if (b1.Text == "") return false;
            // Retorna verdadeiro apenas se B1 for igual a B2 E B2 for igual a B3
            return (b1.Text == b2.Text && b2.Text == b3.Text);
        }
        // Evento de clique do botão Reiniciar
        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            ZerarTabuleiro();
        }
        // Método que limpa todos os botões e reseta as variáveis
        private void ZerarTabuleiro()
        {
            // Limpamos o texto de todos os botões manualmente
            btn1.Text = ""; btn2.Text = ""; btn3.Text = "";
            btn4.Text = ""; btn5.Text = ""; btn6.Text = "";
            btn7.Text = ""; btn8.Text = ""; btn9.Text = "";
            // Reseta para o jogador X começar de novo e zera a contagem
            _vezDoX = true;
            _jogadas = 0;

            //Arrumando os textos
            lblVez.Text = "Vez do Jogador: ";
            lblJogadorAtual.Text = "X";

            //Colocando a cor no texto inicial
            lblJogadorAtual.ForeColor = System.Drawing.Color.Tomato;
        }
    }
}
