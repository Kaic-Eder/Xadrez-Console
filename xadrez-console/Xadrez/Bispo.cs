using tabuleiro;

namespace Xadrez {
    internal class Bispo : Peca{

        public Bispo(Cor cor, Tabuleiro tab) : base(cor, tab) {
        }

        public override string ToString() {
            return "B";
        }

        private bool podeMover(Posicao pos) {
            Peca p = Tab.peca(pos);
            return p == null || p.Cor != Cor;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            //NO
            pos.definirValores(Posicao.linha - 1, Posicao.coluna - 1);
            while (Tab.PosicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor) {
                    break;
                }
                pos.definirValores(pos.linha - 1, pos.coluna - 1);
            }

            //NE
            pos.definirValores(Posicao.linha - 1, Posicao.coluna + 1);
            while (Tab.PosicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor) {
                    break;
                }
                pos.definirValores(pos.linha - 1, pos.coluna + 1);
            }

            //SE
            pos.definirValores(Posicao.linha + 1, Posicao.coluna + 1);
            while (Tab.PosicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor) {
                    break;
                }
                pos.definirValores(pos.linha + 1, pos.coluna + 1);
            }

            //SO
            pos.definirValores(Posicao.linha + 1, Posicao.coluna - 1);
            while (Tab.PosicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor) {
                    break;
                }
                pos.definirValores(pos.linha + 1, pos.coluna - 1);
            }

            return mat;

        }

    }
}
