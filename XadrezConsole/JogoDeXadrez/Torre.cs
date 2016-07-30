using ProfessorNelioAlves.JogoDeTabuleiro;

namespace ProfessorNelioAlves.JogoDeXadrez {

    class Torre : Peca {
        public Torre(Cor cor, Tabuleiro tab) {
            this.cor = cor;
            this.tab = tab;
            posicao = null;
        }

        public override string ToString() {
            return "T";
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

            // esquerda
            pos.setValores(posicao.linha, posicao.coluna - 1);
            while (tab.posicaoValida(pos) && (livre(pos) || existeInimigo(pos))) {
                grade.ligar(pos);
                if (!livre(pos) && existeInimigo(pos)) {
                    break;
                }
                pos.setValores(pos.linha, pos.coluna - 1);
            }

            // direita
            pos.setValores(posicao.linha, posicao.coluna + 1);
            while (tab.posicaoValida(pos) && (livre(pos) || existeInimigo(pos))) {
                grade.ligar(pos);
                if (!livre(pos) && existeInimigo(pos)) {
                    break;
                }
                pos.setValores(pos.linha, pos.coluna + 1);
            }

            // acima
            pos.setValores(posicao.linha - 1, posicao.coluna);
            while (tab.posicaoValida(pos) && (livre(pos) || existeInimigo(pos))) {
                grade.ligar(pos);
                if (!livre(pos) && existeInimigo(pos)) {
                    break;
                }
                pos.setValores(pos.linha - 1, pos.coluna);
            }

            // abaixo
            pos.setValores(posicao.linha + 1, posicao.coluna);
            while (tab.posicaoValida(pos) && (livre(pos) || existeInimigo(pos))) {
                grade.ligar(pos);
                if (!livre(pos) && existeInimigo(pos)) {
                    break;
                }
                pos.setValores(pos.linha + 1, pos.coluna);
            }

            return grade;
        }
    }
}
