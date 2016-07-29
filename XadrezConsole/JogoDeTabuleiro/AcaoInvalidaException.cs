using System;

namespace ProfessorNelioAlves.JogoDeTabuleiro {
    class AcaoInvalidaException : TabuleiroException {

        public AcaoInvalidaException(string message) : base(message) {
        }

        public AcaoInvalidaException(string message, Exception inner) : base(message, inner) {
        }
    }
}
