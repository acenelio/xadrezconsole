using System;

namespace ProfessorNelioAlves.JogoDeTabuleiro {
    class PosicaoInvalidaException : TabuleiroException {

        public PosicaoInvalidaException(string message) : base(message) {
        }

        public PosicaoInvalidaException(string message, Exception inner) : base(message, inner) {
        }
    }
}
