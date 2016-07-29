using ProfessorNelioAlves.JogoDeTabuleiro;
using System;

namespace ProfessorNelioAlves.JogoDeXadrez {

    class Rei : Peca {
        public Rei(Cor cor, Tabuleiro tab) {
            this.cor = cor;
            this.tab = tab;
            posicao = null;
        }

        public override string ToString() {
            return "R";
        }

        public override bool desbloqueada(Posicao pos) {
            Peca p = tab.getPeca(pos);
            return p == null || p.getCor() != cor;
        }

        public override bool[][] movimentosPossiveis() {
            if (posicao == null) {
                throw new AcaoInvalidaException("Erro: tentou-se verificar os movimentos possiveis de uma peca que nao estah no tabuleiro.");
            }
            tab.validarPosicao(posicao); // nunca deveria dar erro aqui, mas... just in case

            Posicao pos = new Posicao(0,0);
            bool[][] mat = XadrezUtil.novaMatrizBooleana8x8();

            pos.setValores(posicao.linha - 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && desbloqueada(pos)) {
                mat[pos.linha][pos.coluna] = true;
                Console.WriteLine("PODE: " + pos.linha + "," + pos.coluna);
            }
            pos.setValores(posicao.linha - 1, posicao.coluna);
            if (tab.posicaoValida(pos) && desbloqueada(pos)) {
                mat[pos.linha][pos.coluna] = true;
                Console.WriteLine("PODE: " + pos.linha + "," + pos.coluna);
            }
            pos.setValores(posicao.linha-1, posicao.coluna+1);
            if (tab.posicaoValida(pos) && desbloqueada(pos)) {
                mat[pos.linha][pos.coluna] = true;
                Console.WriteLine("PODE: " + pos.linha + "," + pos.coluna);
            }
            pos.setValores(posicao.linha, posicao.coluna-1);
            if (tab.posicaoValida(pos) && desbloqueada(pos)) {
                mat[pos.linha][pos.coluna] = true;
                Console.WriteLine("PODE: " + pos.linha + "," + pos.coluna);
            }
            pos.setValores(posicao.linha, posicao.coluna+1);
            if (tab.posicaoValida(pos) && desbloqueada(pos)) {
                mat[pos.linha][pos.coluna] = true;
                Console.WriteLine("PODE: " + pos.linha + "," + pos.coluna);
            }
            pos.setValores(posicao.linha+1, posicao.coluna-1);
            if (tab.posicaoValida(pos) && desbloqueada(pos)) {
                mat[pos.linha][pos.coluna] = true;
                Console.WriteLine("PODE: " + pos.linha + "," + pos.coluna);
            }
            pos.setValores(posicao.linha+1, posicao.coluna);
            if (tab.posicaoValida(pos) && desbloqueada(pos)) {
                mat[pos.linha][pos.coluna] = true;
                Console.WriteLine("PODE: " + pos.linha + "," + pos.coluna);
            }
            pos.setValores(posicao.linha+1, posicao.coluna+1);
            if (tab.posicaoValida(pos) && desbloqueada(pos)) {
                mat[pos.linha][pos.coluna] = true;
                Console.WriteLine("PODE: " + pos.linha + "," + pos.coluna);
            }

            return mat;
        }
    }
}
