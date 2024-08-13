using tabuleiro;
using xadrez_console.Xadrez;

namespace Xadrez {
    internal class Peao : Peca {

        private PartidaDeXadrez partida;

        public Peao(Cor cor, Tabuleiro tab, PartidaDeXadrez partida) : base(cor, tab) {
            this.partida = partida;
        }

        public override string ToString() {
            return "P";
        }

        private bool existeInimigo(Posicao pos) {
            Peca p = Tab.peca(pos);
            return p != null && p.Cor != Cor;
        }

        private bool livre(Posicao pos) {
            return Tab.peca(pos) == null;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0,0);

            if (Cor == Cor.Branca) {
                pos.definirValores(Posicao.linha - 1, Posicao.coluna);
                if (Tab.PosicaoValida(pos) && livre(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(Posicao.linha - 2, Posicao.coluna);
                if (Tab.PosicaoValida(pos) && livre(pos) && QntMovimentos == 0) {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(Posicao.linha - 1, Posicao.coluna - 1);
                if (Tab.PosicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(Posicao.linha - 1, Posicao.coluna + 1);
                if (Tab.PosicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }

                // #jogadaespecial en passant
                if (Posicao.linha == 3) {
                    Posicao esquerda = new Posicao(Posicao.linha, Posicao.coluna - 1);
                    if (Tab.PosicaoValida(esquerda) && existeInimigo(esquerda) && Tab.peca(esquerda) == partida.vuneravelenPassant) {
                        mat[esquerda.linha - 1, esquerda.coluna] = true;

                    }
                    Posicao direita = new Posicao(Posicao.linha, Posicao.coluna + 1);
                    if (Tab.PosicaoValida(direita) && existeInimigo(direita) && Tab.peca(direita) == partida.vuneravelenPassant) {
                        mat[direita.linha - 1, direita.coluna] = true;

                    }
                } 

            } else {
                pos.definirValores(Posicao.linha + 1, Posicao.coluna);
                if (Tab.PosicaoValida(pos) && livre(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(Posicao.linha + 2, Posicao.coluna);
                if (Tab.PosicaoValida(pos) && livre(pos) && QntMovimentos == 0) {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(Posicao.linha + 1, Posicao.coluna - 1);
                if (Tab.PosicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(Posicao.linha + 1, Posicao.coluna + 1);
                if (Tab.PosicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }

                // #jogadaespecial en passant
                if (Posicao.linha == 4) {
                    Posicao esquerda = new Posicao(Posicao.linha, Posicao.coluna - 1);
                    if (Tab.PosicaoValida(esquerda) && existeInimigo(esquerda) && Tab.peca(esquerda) == partida.vuneravelenPassant) {
                        mat[esquerda.linha +1, esquerda.coluna] = true;

                    }
                    Posicao direita = new Posicao(Posicao.linha, Posicao.coluna + 1);
                    if (Tab.PosicaoValida(direita) && existeInimigo(direita) && Tab.peca(direita) == partida.vuneravelenPassant) {
                        mat[direita.linha + 1, direita.coluna] = true;

                    }
                }
            }
            return mat;
        }

    }
}
