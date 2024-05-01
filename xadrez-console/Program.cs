using tabuleiro;
using xadrez_console;
using Xadrez;

internal class Program {
    private static void Main(string[] args) {

        Posicao P;

        P = new Posicao(3, 4);
        Tabuleiro tabuleiro = new Tabuleiro(8,8);

        tabuleiro.colocarPeca(new Torre(Cor.Preta,tabuleiro), new Posicao(0, 0));
        tabuleiro.colocarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(1, 3));
        tabuleiro.colocarPeca(new Rei(Cor.Preta, tabuleiro), new Posicao(2, 4));


        Tela.imprimirTabuleiro(tabuleiro);



    }
}