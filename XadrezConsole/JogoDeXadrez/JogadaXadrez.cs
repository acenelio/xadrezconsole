using ProfessorNelioAlves.JogoDeTabuleiro;

namespace ProfessorNelioAlves.JogoDeXadrez {
    class JogadaXadrez {
        private PosicaoXadrez origem, destino;
        private Peca pecaCapturada;
        private bool xeque;
        private bool xequeMate;

        public JogadaXadrez(PosicaoXadrez origem, PosicaoXadrez destino, Peca pecaCapturada, bool xeque, bool xequeMate) {
            this.origem = origem;
            this.destino = destino;
            this.pecaCapturada = pecaCapturada;
            this.xeque = xeque;
            this.xequeMate = xequeMate;
        }

        public Peca getPecaCapturada() {
            return pecaCapturada;
        }

        public PosicaoXadrez getOrigem() {
            return origem;
        }

        public PosicaoXadrez getDestino() {
            return destino;
        }

        public bool isXeque() {
            return xeque;
        }

        public bool isXequeMate() {
            return xequeMate;
        }
    }
}
