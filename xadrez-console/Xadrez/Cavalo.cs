using tabuleiro;

namespace Xadrez {
    internal class Cavalo : Peca {
        public Cavalo(Cor cor, Tabuleiro tab) : base(cor, tab) {
        }

        public override string ToString() {
            return "C";
        }

        private bool podeMover(Posicao pos) {
            Peca p = Tab.peca(pos);
            return p == null || p.Cor != Cor;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0, 0);


            pos.definirValores(Posicao.linha - 1, Posicao.coluna - 2);
            if (Tab.PosicaoValida(pos) && podeMover(pos)){
                mat[pos.linha, pos.coluna] = true;
            }
            
            pos.definirValores(Posicao.linha + 1, Posicao.coluna - 2);
            if (Tab.PosicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            pos.definirValores(Posicao.linha + 1, Posicao.coluna + 2);
            if (Tab.PosicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            pos.definirValores(Posicao.linha - 1, Posicao.coluna + 2);
            if (Tab.PosicaoValida(pos) && podeMover(pos)){
                mat[pos.linha, pos.coluna] = true;
            }

            pos.definirValores(Posicao.linha - 2, Posicao.coluna - 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos)){
                mat[pos.linha, pos.coluna] = true;
            }

            pos.definirValores(Posicao.linha + 2, Posicao.coluna - 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos)){
                mat[pos.linha, pos.coluna] = true;
            }

            pos.definirValores(Posicao.linha + 2, Posicao.coluna + 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos)){
                mat[pos.linha, pos.coluna] = true;
            }
            
            pos.definirValores(Posicao.linha - 2, Posicao.coluna + 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos)){
                mat[pos.linha, pos.coluna] = true;
            }

            return mat;
        }
    }
}