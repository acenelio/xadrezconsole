using System;

namespace ProfessorNelioAlves.JogoDeTabuleiro {
    class TabuleiroException : Exception {

        public TabuleiroException(string message) : base(message) {
        }

        public TabuleiroException(string message, Exception inner) : base(message, inner) {
        }
    }
}
