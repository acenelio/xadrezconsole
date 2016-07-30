using ProfessorNelioAlves.JogoDeTabuleiro;

namespace ProfessorNelioAlves.JogoDeXadrez {

    class Peao : Peca {
        public Peao(Cor cor, Tabuleiro tab) {
            this.cor = cor;
            this.tab = tab;
            posicao = null;
        }

        public override string ToString() {
            return "P";
        }

        private bool existeInimigo(Posicao pos) {
            Peca p = tab.getPeca(pos);
            return p != null && p.getCor() != cor;
        }

        private bool livre(Posicao pos) {
            return tab.getPeca(pos) == null;
        }

        public override Grade movimentosPossiveis() {
            if (posicao == null) {
                throw new AcaoInvalidaException("Erro: tentou-se verificar os movimentos possiveis de uma peca que nao estah no tabuleiro: " + this);
            }
            tab.validarPosicao(posicao); // nunca deveria dar erro aqui, mas... just in case

            Posicao pos = new Posicao(0, 0);
            Grade grade = new Grade(8, 8);

            if (cor == Cor.Branca) {
                pos.setValores(posicao.linha - 1, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos)) {
                    grade.ligar(pos);
                }
                pos.setValores(posicao.linha - 1, posicao.coluna - 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                    grade.ligar(pos);
                }
                pos.setValores(posicao.linha - 1, posicao.coluna + 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                    grade.ligar(pos);
                }
            }
            else {
                pos.setValores(posicao.linha + 1, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos)) {
                    grade.ligar(pos);
                }
                pos.setValores(posicao.linha + 1, posicao.coluna - 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                    grade.ligar(pos);
                }
                pos.setValores(posicao.linha + 1, posicao.coluna + 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                    grade.ligar(pos);
                }
            }

            return grade;
        }
    }
}
