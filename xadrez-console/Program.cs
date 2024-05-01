using tabuleiro;

internal class Program {
    private static void Main(string[] args) {

        Posicao P;

        P = new Posicao(3, 4);
        Tabuleiro tabuleiro = new Tabuleiro(8,8);

        Console.WriteLine("Posição: " + P);



    }
}