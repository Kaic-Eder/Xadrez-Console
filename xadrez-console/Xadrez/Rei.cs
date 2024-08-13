using System;
using tabuleiro;
using xadrez_console.Xadrez;

namespace Xadrez {
    internal class Rei : Peca {

   
        private PartidaDeXadrez partida;
        public Rei(Cor cor, Tabuleiro tab, PartidaDeXadrez partida) : base(cor, tab) {
            this.partida = partida;
        }

        public override string ToString() {
            return "R";
        }

        private bool podeMover(Posicao pos) {
            Peca p = Tab.peca(pos);
            return p == null || p.Cor != Cor;
        }

        private bool testeTorreParaRoque(Posicao pos) {
            Peca p = Tab.peca(pos);
            return p != null && p is Torre && p.Cor == Cor && p.QntMovimentos == 0;
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

            //#joagadaespecial Roque
            if(QntMovimentos == 0 && !partida.xeque) {
                //Roque pequeno
                Posicao posT1 = new Posicao(Posicao.linha, Posicao.coluna+3);
                if (testeTorreParaRoque(posT1)) {
                    Posicao p1 = new Posicao(Posicao.linha, Posicao.coluna +1);
                    Posicao p2 = new Posicao(Posicao.linha, Posicao.coluna + 2);
                    if (Tab.peca(p1) == null && Tab.peca(p2) == null) {
                        mat[Posicao.linha, Posicao.coluna + 2] = true ;
                    }
                }
            

                //Roque Grande
                Posicao posT2 = new Posicao(Posicao.linha, Posicao.coluna -4);
                if (testeTorreParaRoque(posT2)) {
                    Posicao p1 = new Posicao(Posicao.linha, Posicao.coluna - 1);
                    Posicao p2 = new Posicao(Posicao.linha, Posicao.coluna - 2);
                    Posicao p3 = new Posicao(Posicao.linha, Posicao.coluna - 3);
                    if (Tab.peca(p1) == null && Tab.peca(p2) == null && Tab.peca(p3) == null) {
                        mat[Posicao.linha, Posicao.coluna -2] = true;
                    }
                }
            }

            return mat;

        }
    }
}