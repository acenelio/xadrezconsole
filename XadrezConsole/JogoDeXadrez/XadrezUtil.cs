using ProfessorNelioAlves.JogoDeTabuleiro;

namespace ProfessorNelioAlves.JogoDeXadrez {
    class XadrezUtil {

        public static bool[][] novaMatrizBooleana8x8() {
            bool[][] mat;
            mat = new bool[8][];
            for (int i = 0; i < 8; i++) {
                mat[i] = new bool[8];
            }
            return mat;
        }

        public static bool[][] somaBooleana(bool[][] A, bool[][] B) {
            bool[][] mat;
            mat = new bool[8][];
            for (int i = 0; i < 8; i++) {
                mat[i] = new bool[8];
            }
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    mat[i][j] = A[i][j] || B[i][j];
                }
            }
            return mat;
        }

        public static Cor adversario(Cor cor) {
            if (cor == Cor.Preta) {
                return Cor.Branca;
            }
            else {
                return Cor.Preta;
            }
        }
    }
}
