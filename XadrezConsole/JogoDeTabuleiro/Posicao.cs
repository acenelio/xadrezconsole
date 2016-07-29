namespace ProfessorNelioAlves.JogoDeTabuleiro {

    class Posicao {
        public int linha, coluna;

        public Posicao(int linha, int coluna) {
            this.linha = linha;
            this.coluna = coluna;
        }

        public void setValores(int linha, int coluna) {
            this.linha = linha;
            this.coluna = coluna;
        }

        public override string ToString() {
            return linha + "," + coluna;
        }
    }
}
