using System.Collections.Generic;
using ProfessorNelioAlves.JogoDeTabuleiro;

namespace ProfessorNelioAlves.JogoDeXadrez {
    class PartidaDeXadrez {
        private Tabuleiro tab;
        private int turno;
        private Cor jogadorAtual;
        private bool terminada;
        private IList<JogadaXadrez> historico;
        private ISet<Peca> pecas;

        public PartidaDeXadrez() {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            historico = new List<JogadaXadrez>();
            pecas = new HashSet<Peca>();
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

        public void realizarJogada(PosicaoXadrez origem, PosicaoXadrez destino) {
            bool xeque = false;
            bool xequeMate = false;

            Posicao posOrigem = origem.toPosicao();
            Posicao posDestino = destino.toPosicao();

            Peca p1 = tab.getPeca(posOrigem);

            if (!p1.podeMoverPara(posDestino)) {
                throw new AcaoInvalidaException("Posicao de destino invalida. Jogada cancelada.");
            }

            // realiza o movimento
            Peca p2 = tab.getPeca(posDestino);
            if (p2 != null) {
                p2 = tab.retirarPeca(posDestino);
            }
            p1 = tab.retirarPeca(posOrigem);
            tab.colocarPeca(posDestino, p1);

            if (testeXeque(jogadorAtual)) {
                // desfaz o movimento
                p1 = tab.retirarPeca(posDestino);
                if (p2 != null) {
                    tab.colocarPeca(posDestino, p2);
                }
                tab.colocarPeca(posOrigem, p1);
                throw new AcaoInvalidaException("Voce nao pode se colocar em xeque");
            }

            if (testeXeque(XadrezUtil.adversario(jogadorAtual))) {
                xeque = true;
            }

            if (testeXequeMate(XadrezUtil.adversario(jogadorAtual))) {
                xequeMate = true;
                terminada = true;
            }
            else {
                turno++;
                trocaJogador();
            }

            historico.Insert(historico.Count, new JogadaXadrez(origem, destino, p2, xeque, xequeMate));
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

        private bool testeXequeMate(Cor cor) {
            return false;
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
            colocarPecaXadrez('a', 2, new Peao(Cor.Branca, tab));
            colocarPecaXadrez('b', 2, new Peao(Cor.Branca, tab));
            colocarPecaXadrez('c', 2, new Peao(Cor.Branca, tab));
            colocarPecaXadrez('d', 2, new Peao(Cor.Branca, tab));
            colocarPecaXadrez('e', 2, new Peao(Cor.Branca, tab));
            colocarPecaXadrez('f', 2, new Peao(Cor.Branca, tab));
            colocarPecaXadrez('g', 2, new Peao(Cor.Branca, tab));
            colocarPecaXadrez('h', 2, new Peao(Cor.Branca, tab));
            colocarPecaXadrez('e', 1, new Rei(Cor.Branca, tab));

            colocarPecaXadrez('a', 7, new Peao(Cor.Preta, tab));
            colocarPecaXadrez('b', 7, new Peao(Cor.Preta, tab));
            colocarPecaXadrez('c', 7, new Peao(Cor.Preta, tab));
            colocarPecaXadrez('d', 7, new Peao(Cor.Preta, tab));
            colocarPecaXadrez('e', 7, new Peao(Cor.Preta, tab));
            colocarPecaXadrez('f', 7, new Peao(Cor.Preta, tab));
            colocarPecaXadrez('g', 7, new Peao(Cor.Preta, tab));
            colocarPecaXadrez('h', 7, new Peao(Cor.Preta, tab));
            colocarPecaXadrez('e', 8, new Rei(Cor.Preta, tab));
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
            foreach (JogadaXadrez j in historico) {
                if (j.getPecaCapturada()!=null && j.getPecaCapturada().getCor()==cor) {
                    bool ok = aux.Add(j.getPecaCapturada());
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
