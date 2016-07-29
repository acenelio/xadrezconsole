namespace ProfessorNelioAlves.JogoDeTabuleiro {

    abstract class Peca {
        protected Cor cor;
        protected Tabuleiro tab;
        protected Posicao posicao; // posicao desta peca

        public Cor getCor() {
            return cor;
        }

        public Posicao getPosicao() {
            return posicao;
        }

        public void setPosicao(Posicao posicao) {
            this.posicao = posicao;
        }

        public bool ehPossivelMover() {
            bool[][] mat = movimentosPossiveis();
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    if (mat[i][j]) {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool podeMoverPara(Posicao pos) {
            bool[][] mat = movimentosPossiveis();
            return mat[pos.linha][pos.coluna];
        }

        public abstract bool desbloqueada(Posicao pos);
        public abstract bool[][] movimentosPossiveis();
    }
}
