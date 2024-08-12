﻿using tabuleiro;
using xadrez_console;
using Xadrez;
using xadrez_console.Xadrez;

internal class Program {
    private static void Main(string[] args) {

		try {

			PartidaDeXadrez partida = new PartidaDeXadrez();

            while (!partida.terminada) {

                try {
                    {
                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.tab);

                        Console.WriteLine(" ");
                        Console.WriteLine("turno: " + partida.turno);
                        Console.WriteLine("Aguardando jogada: " + partida.jogadorAtual);



                        Console.WriteLine();

                        Console.Write("Origem: ");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaodeOrigem(origem);


                        bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();

                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.tab, posicoesPossiveis);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeDestino(origem, destino);

                        partida.realizaJogada(origem, destino);
                    }


                } catch (TabuleiroException e) {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }

			

			
        }
		catch (TabuleiroException e) {
			Console.WriteLine(e.Message);
		}

		Console.ReadLine();


    }
}