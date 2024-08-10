using System;
using tabuleiro;

namespace Xadrez {
    internal class Rei : Peca {
        public Rei(Cor cor, Tabuleiro tab) : base(cor, tab) { }

        public override string ToString() {
            return "R";
        }

        private bool podeMover(Posicao pos) {
            Peca p = Tab.peca(pos);
            return p == null || p.Cor != Cor;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            //acima
            pos.definirValores(Posicao.linha - 1, Posicao.coluna);
            if (Tab.PosicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //nordeste
            pos.definirValores(Posicao.linha - 1, Posicao.coluna + 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //Direita
            pos.definirValores(Posicao.linha, Posicao.coluna + 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            //sudeste
            pos.definirValores(Posicao.linha + 1, Posicao.coluna + 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            //abaixo
            pos.definirValores(Posicao.linha + 1, Posicao.coluna);
            if (Tab.PosicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            //sudoeste
            pos.definirValores(Posicao.linha + 1, Posicao.coluna - 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            //esquerda
            pos.definirValores(Posicao.linha, Posicao.coluna - 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            //noroeste
            pos.definirValores(Posicao.linha - 1, Posicao.coluna - 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            return mat;

        }
    }
}