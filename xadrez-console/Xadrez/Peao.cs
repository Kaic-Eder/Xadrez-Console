using tabuleiro;

namespace Xadrez {
    internal class Peao : Peca {

        public Peao(Cor cor, Tabuleiro tab) : base(cor, tab) {
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

                pos.definirValores(Posicao.linha - 1, Posicao.coluna -1);
                if (Tab.PosicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(Posicao.linha - 1, Posicao.coluna + 1);
                if (Tab.PosicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
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
            }
            return mat;
        }

    }
}
