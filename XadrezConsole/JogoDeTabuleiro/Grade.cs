namespace ProfessorNelioAlves.JogoDeTabuleiro {
    class Grade {
        private bool[][] mat;
        private int linhas, colunas;

        public Grade(int linhas, int colunas) {
            this.linhas = linhas;
            this.colunas = colunas;
            mat = new bool[linhas][];
            for (int i = 0; i < linhas; i++) {
                mat[i] = new bool[colunas];
            }
        }

        public bool ligada(Posicao pos) {
            return mat[pos.linha][pos.coluna];
        }

        public bool ligada(int linha, int coluna) {
            return mat[linha][coluna];
        }

        public void ligar(Posicao pos) {
            mat[pos.linha][pos.coluna] = true;
        }

        public void ligar(int linha, int coluna) {
            mat[linha][coluna] = true;
        }

        public void desligar(int linha, int coluna) {
            mat[linha][coluna] = false;
        }

        public void desligar(Posicao pos) {
            mat[pos.linha][pos.coluna] = false;
        }

        public void somar(Grade outra) {
            for (int i = 0; i < linhas; i++) {
                for (int j = 0; j < colunas; j++) {
                    mat[i][j] = mat[i][j] || outra.ligada(i,j);
                }
            }
        }
    }
}
