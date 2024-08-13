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
            

            //#joagadaespecial roque pequeno
            if (p is Rei && destino.coluna == Origem.coluna + 2) {
                Posicao OrigemT = new Posicao(Origem.linha, Origem.coluna+3);
                Posicao DestinoT = new Posicao(Origem.linha, Origem.coluna+1);
                Peca T = tab.RetirarPeca(OrigemT);
                T.incrementarQntMovimentos();
                tab.colocarPeca(T, DestinoT);

            }

            //#joagadaespecial roque Grande
            if (p is Rei && destino.coluna == Origem.coluna - 2) {
                Posicao OrigemT = new Posicao(Origem.linha, Origem.coluna - 4);
                Posicao DestinoT = new Posicao(Origem.linha, Origem.coluna - 1);
                Peca T = tab.RetirarPeca(OrigemT);
                T.incrementarQntMovimentos();
                tab.colocarPeca(T, DestinoT);

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

            //#joagadaespecial roque pequeno
            if (p is Rei && destino.coluna == origem.coluna + 2) {
                Posicao OrigemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao DestinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.RetirarPeca(DestinoT);
                T.decrementarQntMovimentos();
                tab.colocarPeca(T, OrigemT);

            }

            //#joagadaespecial roque Grande
            if (p is Rei && destino.coluna == origem.coluna - 2) {
                Posicao OrigemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao DestinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.RetirarPeca(DestinoT);
                T.incrementarQntMovimentos();
                tab.colocarPeca(T, OrigemT);

            }

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

            if (testeXequemate(adversaria(jogadorAtual))) {
                terminada = true;
            } else {
                turno++;
                mudaJogador();
            }
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
            if (!tab.peca(origem).movimentoPossivel(destino)) {
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

        public bool testeXequemate(Cor cor) {
            if (!estaEmXeque(cor)) {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor)) {
                bool[,] mat = x.movimentosPossiveis();
                for (int i = 0; i < tab.Linhas; i++) {
                    for (int j = 0; j < tab.Colunas; j++) {
                        if (mat[i,j]) {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(x.Posicao, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
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
            // Colocando as torres
            colocarNovaPeca(new Torre(Cor.Branca, tab), 'a', 1);
            colocarNovaPeca(new Torre(Cor.Branca, tab), 'h', 1);
            colocarNovaPeca(new Torre(Cor.Preta, tab), 'a', 8);
            colocarNovaPeca(new Torre(Cor.Preta, tab), 'h', 8);

            // Colocando os cavalos
            colocarNovaPeca(new Cavalo(Cor.Branca, tab), 'b', 1);
            colocarNovaPeca(new Cavalo(Cor.Branca, tab), 'g', 1);
            colocarNovaPeca(new Cavalo(Cor.Preta, tab), 'b', 8);
            colocarNovaPeca(new Cavalo(Cor.Preta, tab), 'g', 8);

            // Colocando os bispos
            colocarNovaPeca(new Bispo(Cor.Branca, tab), 'c', 1);
            colocarNovaPeca(new Bispo(Cor.Branca, tab), 'f', 1);
            colocarNovaPeca(new Bispo(Cor.Preta, tab), 'c', 8);
            colocarNovaPeca(new Bispo(Cor.Preta, tab), 'f', 8);

            // Colocando a dama
            colocarNovaPeca(new Dama(Cor.Branca, tab), 'd', 1);
            colocarNovaPeca(new Dama(Cor.Preta, tab), 'd', 8);

            // Colocando o rei
            colocarNovaPeca(new Rei(Cor.Branca, tab, this), 'e', 1);
            colocarNovaPeca(new Rei(Cor.Preta, tab, this), 'e', 8);

            // Colocando os peões
            for (char coluna = 'a'; coluna <= 'h'; coluna++) {
                colocarNovaPeca(new Peao(Cor.Branca, tab), coluna, 2);
                colocarNovaPeca(new Peao(Cor.Preta, tab), coluna, 7);
            }
            

            
        }


    }
}
