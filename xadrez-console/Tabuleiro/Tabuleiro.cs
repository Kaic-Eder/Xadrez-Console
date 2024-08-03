using System;

namespace tabuleiro {
    internal class Tabuleiro {

        public int Linhas {  get; set; }
        public int Colunas { get; set; }

        private Peca[,] Pecas;

        public Tabuleiro(int linhas, int colunas) {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas,colunas];
        }

        public Peca peca(int linha, int coluna) {
            return Pecas[linha, coluna];

        }

        public Peca peca(Posicao pos) {
            return Pecas[pos.linha, pos.coluna]; 
        }

        public bool ExistePeca(Posicao pos) {
            validarPosicao(pos);
            return peca(pos) != null;
        }

        public void colocarPeca(Peca p, Posicao pos) {
            if (ExistePeca(pos)) {
                throw new TabuleiroException("Já existe uma peça nessa posição!");
            }
            Pecas[pos.linha,pos.coluna] = p;
            p.Posicao = pos;
        }

        public Peca RetirarPeca(Posicao pos)
        {
            if(peca(pos) == null)
            {
                return null;
            }
            Peca aux = peca(pos);
            aux.Posicao = null;
            Pecas[pos.linha, pos.coluna] = null;
            return aux;
        }

        public bool PosicaoValida(Posicao pos) {
            if(pos.linha<0 || pos.linha>=Linhas || pos.coluna<0 || pos.coluna >= Colunas) {
                return false;
            }
            return true;
        }

        public void validarPosicao(Posicao pos) {
            if (!PosicaoValida(pos)) {
                throw new TabuleiroException("Posição Inválida!");
            }
        }

    }
}
