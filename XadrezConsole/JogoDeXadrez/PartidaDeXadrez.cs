using System.Collections.Generic;
using ProfessorNelioAlves.JogoDeTabuleiro;

namespace ProfessorNelioAlves.JogoDeXadrez {
    class PartidaDeXadrez {
        private Tabuleiro tab;
        private int turno;
        private Cor jogadorAtual;
        private bool terminada;
        private bool emXeque;
        private IList<JogadaXadrez> historico;
        private ISet<Peca> pecas;
        private ISet<Peca> capturadas;

        public PartidaDeXadrez() {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            emXeque = false;
            historico = new List<JogadaXadrez>();
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            instanciarPecas();
        }

        public Tabuleiro getTabuleiro() {
            return tab;
        }

        public int getTurno() {
            return turno;
        }

        public Cor getJogadorAtual() {
            return jogadorAtual;
        }

        public bool getTerminada() {
            return terminada;
        }

        public bool getEmXeque() {
            return emXeque;
        }

        public IList<JogadaXadrez> getHistorico() {
            return new List<JogadaXadrez>(historico);
        }

        public void validarPecaEscolhida(PosicaoXadrez posXadrez) {
            Posicao pos = posXadrez.toPosicao();
            if (!tab.existePeca(pos)) {
                throw new AcaoInvalidaException("Nao existe uma peca em " + pos);
            }
            if (jogadorAtual != tab.corDaPeca(pos)) {
                throw new AcaoInvalidaException("Esta peca nao eh sua");
            }
            Peca peca = tab.getPeca(pos);
            if (!peca.ehPossivelMover()) {
                throw new AcaoInvalidaException("Nao ha movimentos possiveis para essa peca");
            }
        }

        public void validarDestino(PosicaoXadrez origem, PosicaoXadrez destino) {
            Posicao posOrigem = origem.toPosicao();
            Posicao posDestino = destino.toPosicao();

            Peca p1 = tab.getPeca(posOrigem);

            if (!p1.podeMoverPara(posDestino)) {
                throw new AcaoInvalidaException("Posicao de destino invalida.");
            }
        }

        public void realizarJogada(PosicaoXadrez origem, PosicaoXadrez destino) {
            bool xeque = false;
            bool xequeMate = false;

            Posicao posOrigem = origem.toPosicao();
            Posicao posDestino = destino.toPosicao();

            Peca pecaCapturada = executaMovimento(posOrigem, posDestino);

            if (testeXeque(jogadorAtual)) {
                desfazMovimento(posOrigem, posDestino, pecaCapturada);
                throw new AcaoInvalidaException("Voce nao pode se colocar em xeque");
            }

            if (testeXeque(XadrezUtil.adversario(jogadorAtual))) {
                xeque = true;
                emXeque = true;
            }
            else {
                emXeque = false;
            }

            if (testeXequeMate(XadrezUtil.adversario(jogadorAtual))) {
                xequeMate = true;
                terminada = true;
            }
            else {
                turno++;
                trocaJogador();
            }

            historico.Insert(historico.Count, new JogadaXadrez(origem, destino, pecaCapturada, xeque, xequeMate));
        }

        private bool testeXeque(Cor cor) {
            Grade grade = new Grade(8,8);
            ISet<Peca> pecasAdversarias = pecasEmJogo(XadrezUtil.adversario(cor));
            foreach (Peca peca in pecasAdversarias) {
                Grade outra = peca.movimentosPossiveis();
                grade.somar(outra);
            }
            Peca rei = getRei(cor);
            if (rei==null) {
                throw new TabuleiroException("Nao existe rei dentre as pecas " + cor);
            }
            return grade.ligada(rei.getPosicao());
        }

        private Peca executaMovimento(Posicao origem, Posicao destino) {
            Peca p1 = tab.retirarPeca(origem);
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(destino, p1);
            if (pecaCapturada != null) {
                capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        private void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) {
            Peca p1 = tab.retirarPeca(destino);
            if (pecaCapturada != null) {
                tab.colocarPeca(destino, pecaCapturada);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(origem, p1);
        }

        private bool testeXequeMate(Cor cor) {
            if (!testeXeque(cor)) {
                return false;
            } 

            ISet<Peca> pecasDestaCor = pecasEmJogo(cor);
            foreach (Peca p in pecasDestaCor) {
                Grade grade = p.movimentosPossiveis();
                for (int i=0; i<8; i++) {
                    for (int j=0; j<8; j++) {
                        if (grade.ligada(i,j)) {
                            Posicao origem = p.getPosicao();
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            bool tx = testeXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!tx) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        private void trocaJogador() {
            if (jogadorAtual == Cor.Preta) {
                jogadorAtual = Cor.Branca;
            }
            else {
                jogadorAtual = Cor.Preta;
            }
        }

        private void instanciarPecas() {
            colocarPecaXadrez('a', 1, new Torre(Cor.Branca, tab));
            colocarPecaXadrez('c', 1, new Torre(Cor.Branca, tab));
            colocarPecaXadrez('h', 2, new Torre(Cor.Branca, tab));
            colocarPecaXadrez('e', 1, new Rei(Cor.Branca, tab));

            colocarPecaXadrez('a', 7, new Peao(Cor.Preta, tab));
            colocarPecaXadrez('c', 7, new Peao(Cor.Preta, tab));
            colocarPecaXadrez('d', 7, new Peao(Cor.Preta, tab));
            colocarPecaXadrez('e', 7, new Peao(Cor.Preta, tab));
            colocarPecaXadrez('f', 7, new Peao(Cor.Preta, tab));
            colocarPecaXadrez('g', 7, new Peao(Cor.Preta, tab));
            colocarPecaXadrez('h', 7, new Peao(Cor.Preta, tab));
            colocarPecaXadrez('b', 8, new Rei(Cor.Preta, tab));
        }

        private void colocarPecaXadrez(char coluna, int linha, Peca peca) {
            Posicao pos = new PosicaoXadrez(coluna, linha).toPosicao();
            tab.colocarPeca(pos, peca);
            pecas.Add(peca);
        }

        public ISet<Peca> pecasEmJogo(Cor cor) {
            ISet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas) {
                if (x.getCor()==cor) {
                    bool ok = aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        public ISet<Peca> pecasCapturadas(Cor cor) {
            ISet<Peca> aux = new HashSet<Peca>();
            foreach (Peca p in capturadas) {
                if (p.getCor()==cor) {
                    bool ok = aux.Add(p);
                }
            }
            return aux;
        }

        private Peca getRei(Cor cor) {
            ISet<Peca> aux = pecasEmJogo(cor);
            foreach (Peca p in aux) {
                if (p is Rei) {
                    return p;
                }
            }
            return null;
        } 
    }
}
