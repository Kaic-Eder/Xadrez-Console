using tabuleiro;
using xadrez_console;
using Xadrez;

internal class Program {
    private static void Main(string[] args) {

        try {
            Tabuleiro tabuleiro = new Tabuleiro(8, 8);

            tabuleiro.colocarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(0, 0));
            tabuleiro.colocarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(1, 3));
            tabuleiro.colocarPeca(new Rei(Cor.Preta, tabuleiro), new Posicao(0, 2));


            Tela.imprimirTabuleiro(tabuleiro);
        }
        catch (TabuleiroException e) {
            Console.WriteLine(e.Message);
        }


    }
}