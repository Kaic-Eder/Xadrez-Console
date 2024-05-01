using tabuleiro;
using xadrez_console;
using Xadrez;
using xadrez_console.Xadrez;

internal class Program {
    private static void Main(string[] args) {

        PosicaoXadrez pos = new PosicaoXadrez('c', 7);


        Console.WriteLine(pos);
        Console.WriteLine(pos.toPosicao());


        Console.ReadLine();


    }
}