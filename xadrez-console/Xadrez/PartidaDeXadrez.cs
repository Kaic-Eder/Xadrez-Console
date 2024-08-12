using System.Collections.Generic;
using tabuleiro;
using Xadrez;

namespace xadrez_console.Xadrez {
    internal class PartidaDeXadrez {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; } 
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque {  get; private set; }

        public PartidaDeXadrez() {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }
        public Peca executaMovimento(Posicao Origem, Posicao destino) {
            Peca p = tab.RetirarPeca(Origem);
            p.incrementarQntMovimentos();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null) {
                capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) {
            Peca p = tab.RetirarPeca(destino);
            p.decrementarQntMovimentos();
            if (pecaCapturada != null) { 
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);
        }
        public void realizaJogada(Posicao origem, Posicao destino) {
            Peca pecaCapturada = executaMovimento(origem, destino);

            if (estaEmXeque(jogadorAtual)) {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
                
            }

            if (estaEmXeque(adversaria(jogadorAtual))) {
                xeque = true;
            } else {
                xeque = false;
            }

            turno++;
            mudaJogador();
        }

        public void validarPosicaodeOrigem(Posicao pos) {
            if(tab.peca(pos) == null) {
                throw new TabuleiroException("Não existe uma peça na psoição de origem escolhida!");
            }
            if(jogadorAtual != tab.peca(pos).Cor) {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis()){
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino) {
            if (!tab.peca(origem).podeMoverPara(destino)) {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void mudaJogador() {
            if(jogadorAtual == Cor.Branca) {
                jogadorAtual = Cor.Preta;
            } else {
                jogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas) {
                if(x.Cor == cor) {
                    aux.Add(x);
                }
            }

            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas) {
                if (x.Cor == cor) {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));

            return aux;
        }

        private Peca Rei(Cor cor) {
            foreach (Peca x in pecasEmJogo(cor)) {
                if ( x is Rei){
                    return x;
                }
            }
            return null;
        }

        public bool estaEmXeque(Cor cor) {

            Peca R = Rei(cor);
            if (R == null) {
                throw new TabuleiroException("Não tem rei da cor" + cor + " no tabuleiro");
            }

            foreach (Peca x in pecasEmJogo(adversaria(cor))) {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.Posicao.linha, R.Posicao.coluna]) {
                    return true;
                }
            }
            return false;
        }

        private Cor adversaria(Cor cor) {
            if (cor == Cor.Branca) {
                return Cor.Preta;
            } else {
                return Cor.Branca;
            }
        }

        public void colocarNovaPeca(Peca peca, char coluna, int linha) {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas() {
            colocarNovaPeca(new Torre(Cor.Branca, tab), 'c', 1);
            colocarNovaPeca(new Torre(Cor.Branca, tab), 'c', 2);
            colocarNovaPeca(new Torre(Cor.Branca, tab), 'd', 2);
            colocarNovaPeca(new Torre(Cor.Branca, tab), 'e', 2);
            colocarNovaPeca(new Torre(Cor.Branca, tab), 'e', 1);
            colocarNovaPeca(new Rei(Cor.Branca, tab), 'd', 1);

            colocarNovaPeca(new Torre(Cor.Preta, tab), 'c', 7);
            colocarNovaPeca(new Torre(Cor.Preta, tab), 'c', 8);
            colocarNovaPeca(new Torre(Cor.Preta, tab), 'd', 7);
            colocarNovaPeca(new Torre(Cor.Preta, tab), 'e', 7);
            colocarNovaPeca(new Torre(Cor.Preta, tab), 'e', 8);
            colocarNovaPeca(new Rei(Cor.Preta, tab), 'd', 8);
        }

    }
}
