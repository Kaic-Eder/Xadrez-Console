
namespace tabuleiro {
    internal abstract class Peca {

        public Posicao Posicao { get; set; }

        public Cor Cor { get; protected set; }

        public int QntMovimentos { get; protected set; }
        public Tabuleiro Tab {  get; protected set; }

        public Peca(Cor cor, Tabuleiro tab) {
            Cor = cor;
            Tab = tab;
            QntMovimentos = 0;
        }

        public void incrementarQntMovimentos() {
            QntMovimentos++;
        }

        public void decrementarQntMovimentos() {
            QntMovimentos--;
        }

        public bool existeMovimentosPossiveis() {
            bool[,] mat = movimentosPossiveis();
            for (int i = 0; i < Tab.Linhas; i++) {
                for (int j = 0; j < Tab.Colunas; j++) {
                    if (mat[i, j]) {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool movimentoPossivel(Posicao pos) {
            return movimentosPossiveis()[pos.linha, pos.coluna];
        }

        public abstract bool[,] movimentosPossiveis();

    }
}
