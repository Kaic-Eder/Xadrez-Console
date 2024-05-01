using tabuleiro;
using xadrez_console;

internal class Program {
    private static void Main(string[] args) {

        Posicao P;

        P = new Posicao(3, 4);
        Tabuleiro tabuleiro = new Tabuleiro(8,8);

        Tela.imprimirTabuleiro(tabuleiro);

        Console.WriteLine("Posição: " + P);



    }
}