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
            Grade grade = movimentosPossiveis();
            for (int i = 0; i < tab.getLinhas(); i++) {
                for (int j = 0; j < tab.getColunas(); j++) {
                    if (grade.ligada(i,j)) {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool podeMoverPara(Posicao pos) {
            Grade grade = movimentosPossiveis();
            return grade.ligada(pos);
        }

        public abstract Grade movimentosPossiveis();
    }
}
