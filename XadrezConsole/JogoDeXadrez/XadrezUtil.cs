using ProfessorNelioAlves.JogoDeTabuleiro;

namespace ProfessorNelioAlves.JogoDeXadrez {
    class XadrezUtil {

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
