namespace ProfessorNelioAlves.JogoDeTabuleiro {

    /**
     * Tabuleiro eh uma grade com pecas. Ele nao tem inteligencia das
     * regras do jogo. Ele so sabe colocar e retirar pecas. 
     */
    class Tabuleiro {
        Peca[][] pecas;
        int linhas, colunas;

        public Tabuleiro(int linhas, int colunas) {
            this.linhas = linhas;
            this.colunas = colunas;
            pecas = new Peca[linhas][];
            for (int i = 0; i < linhas; i++) {
                pecas[i] = new Peca[colunas];
            }
        }

        public int getLinhas() {
            return linhas;
        }

        public int getColunas() {
            return colunas;
        }

        public Peca getPeca(Posicao pos) {
            validarPosicao(pos);
            return pecas[pos.linha][pos.coluna];
        }

        public bool existePeca(Posicao pos) {
            validarPosicao(pos);
            return pecas[pos.linha][pos.coluna] != null;
        }

        public Cor corDaPeca(Posicao pos) {
            validarPosicao(pos);
            return pecas[pos.linha][pos.coluna].getCor();
        }

        public void colocarPeca(Posicao pos, Peca p) {
            if (existePeca(pos)) {
                throw new AcaoInvalidaException("Erro ao colocar peca. Ja existe uma peca em " + pos);
            }
            pecas[pos.linha][pos.coluna] = p;
            p.setPosicao(pos);
        }

        public Peca retirarPeca(Posicao pos) {
            if (!existePeca(pos)) {
                return null;
            }
            Peca aux = pecas[pos.linha][pos.coluna];
            aux.setPosicao(null);
            pecas[pos.linha][pos.coluna] = null;
            return aux;
        }

        public bool posicaoValida(Posicao pos) {
            if (pos.linha < 0 || pos.linha >= linhas || pos.coluna < 0 || pos.coluna >= colunas) {
                return false;
            }
            return true;
        }

        public void validarPosicao(Posicao pos) {
            if (!posicaoValida(pos)) {
                throw new PosicaoInvalidaException("Esta posicao nao existe: " + pos);
            }
        }
    }
}
