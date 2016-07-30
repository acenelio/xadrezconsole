using System;
using ProfessorNelioAlves.JogoDeTabuleiro;
using ProfessorNelioAlves.JogoDeXadrez;

namespace ProfessorNelioAlves {
    class Program
    {
        static void Main(string[] args)
        {
            PartidaDeXadrez partida = new PartidaDeXadrez();
            char colunaOrigem, colunaDestino;
            int linhaOrigem, linhaDestino;
            PosicaoXadrez origem, destino;

            while (!partida.getTerminada()) {
                try {
                    imprimirConsole(partida);
                    Console.WriteLine("Escolha uma peca: ");
                    Console.Write("Coluna: ");
                    colunaOrigem = Convert.ToChar(Console.ReadLine());
                    Console.Write("Linha: ");
                    linhaOrigem = Convert.ToInt32(Console.ReadLine());

                    origem = new PosicaoXadrez(colunaOrigem, linhaOrigem);

                    partida.validarPecaEscolhida(origem);

                    imprimirPossiveis(partida, origem.toPosicao());

                    Console.WriteLine("Deseja mover a peça para qual posicao: ");
                    Console.Write("Coluna: ");
                    colunaDestino = Convert.ToChar(Console.ReadLine());
                    Console.Write("Linha: ");
                    linhaDestino = Convert.ToInt32(Console.ReadLine());

                    destino = new PosicaoXadrez(colunaDestino, linhaDestino);

                    partida.validarDestino(origem, destino);

                    partida.realizarJogada(origem, destino);
                }
                catch (TabuleiroException e) {
                    // Console.WriteLine(e.ToString()); // use esta para debug
                    Console.WriteLine(e.Message);
                    Console.WriteLine();
                }
            }
            imprimirConsole(partida);
            Console.ReadLine();
        }

        static void imprimirConsole(PartidaDeXadrez partida) {
            Tabuleiro tab = partida.getTabuleiro();
            for (int i = 0; i < 8; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < 8; j++) {
                    Posicao pos = new Posicao(i, j);
                    if (tab.getPeca(pos) == null) {
                        Console.Write("- ");
                    }
                    else {
                        imprimirConsole(tab.getPeca(pos));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h\n");
            Console.WriteLine("Turno: " + partida.getTurno());
            if (partida.getTerminada()) {
                Console.WriteLine("CHEQUEMATE!");
            }
            else {
                Console.WriteLine("Quem joga: " + partida.getJogadorAtual());
                if (partida.getEmXeque()) {
                    Console.WriteLine("CHEQUE!");
                }
            }
            Console.WriteLine();
        }


        static void imprimirConsole(Peca peca) {
            if (peca.getCor() == Cor.Preta) {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
            else {
                Console.Write(peca);
            }
        }

        static void imprimirPossiveis(PartidaDeXadrez partida, Posicao posOrigem) {
            Tabuleiro tab = partida.getTabuleiro();
            Grade grade = tab.getPeca(posOrigem).movimentosPossiveis();
            for (int i = 0; i < 8; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < 8; j++) {
                    Posicao pos = new Posicao(i, j);
                    imprimirConsole(tab.getPeca(pos), grade.ligada(i, j));
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h\n");
            Console.WriteLine("Turno: " + partida.getTurno());
            Console.WriteLine("Quem joga: " + partida.getJogadorAtual());
            Console.WriteLine();
        }

        static void imprimirConsole(Peca peca, bool ligada) {
            ConsoleColor bg = Console.BackgroundColor;
            ConsoleColor fg = Console.ForegroundColor;
            if (ligada) {
                Console.BackgroundColor = ConsoleColor.DarkGray;
            }

            if (peca!=null && peca.getCor() == Cor.Preta) {
                Console.ForegroundColor = ConsoleColor.Blue;
            }

            if (peca==null) {
                Console.Write("-");
            }
            else {
                Console.Write(peca);
            }
            Console.BackgroundColor = bg;
            Console.ForegroundColor = fg;
        }

    }
}
