namespace JogoAutomatico
{
    public class Program
    {

        static string[,]? Tabuleiro;
        static char[]? Jogadas;
        static char Jogador;
        static char Computador;
        static int[]? Coordenanda;
        static bool Continuar = true;

        static void Main(string[] args)
        {
            while (Continuar)
            {
                InicializarJogo();
                Jogar();
                AbrirDialogo();
            }
        }

        static void InicializarJogo()
        {
            InicializarMatrizTabuleiro();
            EscolherJogador();
            CriarJogadores();
            DefinirJogadas();
        }

        static void Jogar()
        {
            for (int i = 0; i < Jogadas!.Length; i++)
            {
                if (i % 2 == 0) JogarPlayer(i); else JogarPc();

                MarcarJogada(Jogadas[i]);

                if (VerificarVencedor())
                {
                    MostrarVencedor(i);
                    break;
                }
            }
        }

        static void InicializarMatrizTabuleiro()
        {
            Tabuleiro = new string[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Tabuleiro[i, j] = " ";
                }
            }
        }

        static void EscolherJogador()
        {
            bool opcaoValida = false;
            do
            {
                try
                {
                    Console.Clear();
                    Console.Write("Escolha X ou O\n=> ");
                    string inputOpcao = Console.ReadLine()!.ToUpper();

                    char opcao = Convert.ToChar(inputOpcao);
                    opcaoValida = opcao == 88 || opcao == 79 ? true : false;

                    if (opcaoValida) Jogador = opcao;
                    else continue;
                }
                catch (Exception ex)
                {
                    EnviarMensagem(ex.Message);
                    continue;
                }

            } while (!opcaoValida);
        }

        static void CriarJogadores()
        {
            Computador = Jogador == 88 ? (char)79 : (char)88;
        }

        static void DefinirJogadas()
        {
            Jogadas = new char[9];

            for (int i = 0; i < Jogadas.Length; i++)
            {
                Jogadas[i] = (i % 2 == 0) ? Jogador : Computador;
            }
        }


        static void JogarPc()
        {
            MostrarTabuleiro();
            MostrarBarraProgresso();

            do
            {
                CriarJogadaPc();

            } while (!EfetuarValidacoesPc());

        }


        static void JogarPlayer(int i)
        {
            do
            {
                MostrarTabuleiro();
                SolicitarJogada(Jogadas![i]);

            } while (!EfetuarValidacoes());
        }


        static void CriarJogadaPc()
        {
            Random X = new Random();
            Random Y = new Random();

            Coordenanda![0] = X.Next(0, 3);
            Coordenanda[1] = Y.Next(0, 3);
        }


        static void MostrarBarraProgresso()
        {
            Console.Write("\nComputador pensando! Aguarde.");

            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(900);
                Console.Write(".");
            }
        }
        static void MarcarJogada(char jogador)
        {
            Tabuleiro![Coordenanda![0], Coordenanda[1]] = jogador.ToString();
        }

        static void MostrarTabuleiro()
        {
            Console.Clear();
            Console.WriteLine("       |       |      ");
            Console.WriteLine($"   {Tabuleiro![0, 0]}   |   {Tabuleiro![0, 1]}   |   {Tabuleiro![0, 2]}  ");
            Console.WriteLine("_______|_______|_______");
            Console.WriteLine("       |       |      ");
            Console.WriteLine($"   {Tabuleiro![1, 0]}   |   {Tabuleiro![1, 1]}   |   {Tabuleiro![1, 2]}  ");
            Console.WriteLine("_______|_______|_______");
            Console.WriteLine("       |       |       ");
            Console.WriteLine($"   {Tabuleiro![2, 0]}   |   {Tabuleiro![2, 1]}   |   {Tabuleiro![2, 2]}  ");
            Console.WriteLine("       |       |      ");

        }

        static bool ValidarJogada()
        {
            return Coordenanda![0] < 0 || Coordenanda[0] > 2 || Coordenanda[1] < 0 || Coordenanda[1] > 2 ? false : true;
        }

        static bool VerificarCasaVazia()
        {
            return Tabuleiro![Coordenanda![0], Coordenanda![1]] != " " ? false : true;
        }

        static void SolicitarJogada(char jogador)
        {
            Console.Write($"\nJogador:  [ {jogador} ]   Escolha a posição da sua jogada.  Ex: 0,1\n=> ");
            string jogada = Console.ReadLine()!;

            Coordenanda = new int[2];

            var jogadaSplit = jogada!.Split(",");

            Coordenanda![0] = Convert.ToInt16(jogadaSplit[0]);
            Coordenanda![1] = Convert.ToInt16(jogadaSplit[1]);
        }

        static bool EfetuarValidacoes()
        {
            if (!ValidarJogada())
            {
                EnviarMensagem("Jogada fora do limite da Velha... Tente novamente!");
                return false;
            }

            if (!VerificarCasaVazia())
            {
                EnviarMensagem("Posição já ocupada...Tente novamente!");
                return false;
            }
            return true;
        }

        static bool VerificarVencedor()
        {
            return
            VerificarLinhaHorizontal() ? true :
            VerificarLinhaVertical() ? true :
            VerificarDiagonalEsquerda() ? true :
            VerificarDiagonalDireita() ? true :
            false;
        }

        static bool VerificarDiagonalDireita()
        {
            return Tabuleiro![0, 2] == Jogador.ToString() && Tabuleiro[1, 1] == Jogador.ToString() && Tabuleiro[2, 0] == Jogador.ToString() ||
            Tabuleiro![0, 2] == Computador.ToString() && Tabuleiro[1, 1] == Computador.ToString() && Tabuleiro[2, 0] == Computador.ToString() ? true : false;
        }

        static bool VerificarDiagonalEsquerda()
        {
            return Tabuleiro![0, 0] == Jogador.ToString() && Tabuleiro[1, 1] == Jogador.ToString() && Tabuleiro[2, 2] == Jogador.ToString() ||
            Tabuleiro![0, 0] == Computador.ToString() && Tabuleiro[1, 1] == Computador.ToString() && Tabuleiro[2, 2] == Computador.ToString() ? true : false;
        }

        static bool VerificarLinhaVertical()
        {
            for (int i = 0; i < 3; i++)
            {
                if (Tabuleiro![0, i] == Jogador.ToString() && Tabuleiro![1, i] == Jogador.ToString() && Tabuleiro![2, i] == Jogador.ToString() ||
                  Tabuleiro![0, i] == Computador.ToString() && Tabuleiro![1, i] == Computador.ToString() && Tabuleiro![2, i] == Computador.ToString())
                {
                    return true;
                }
            }
            return false;
        }

        static bool VerificarLinhaHorizontal()
        {
            for (int i = 0; i < 3; i++)
            {
                if (Tabuleiro![i, 0] == Jogador.ToString() && Tabuleiro![i, 1] == Jogador.ToString() && Tabuleiro![i, 2] == Jogador.ToString() ||
                 Tabuleiro![i, 0] == Computador.ToString() && Tabuleiro![i, 1] == Computador.ToString() && Tabuleiro![i, 2] == Computador.ToString())
                {
                    return true;
                }
            }
            return false;
        }

        static void MostrarVencedor(int i)
        {
            MostrarTabuleiro();
            Console.Write($"\nDeu velha! Jogador {Jogadas![i]} Venceu! ");
            Console.ReadKey();
        }


        static bool EfetuarValidacoesPc()
        {
            return ValidarJogada() && VerificarCasaVazia() ? true : false;
        }

        static void EnviarMensagem(string menssagem)
        {
            Console.Clear();
            Console.WriteLine(menssagem);
            Console.ReadKey();
        }

        static void AbrirDialogo()
        {
            var opcao = " ";
            do
            {
                Console.Clear();
                Console.Write("Deseja jogar novamente ?  [1] Sim  [2] Não\n=> ");
                opcao = Console.ReadLine();

            } while (opcao != "1" && opcao != "2");

            Continuar = opcao == "1" ? true : false;
        }

    }

}

