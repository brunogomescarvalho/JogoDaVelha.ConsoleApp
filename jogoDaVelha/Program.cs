namespace jogoDaVelha
{
    public class Program
    {

        static string[,]? Tabuleiro;
        static char[]? Jogadas;
        static char Jogador1;
        static char Jogador2;
        static int[]? Coordenanda;

        static void Main(string[] args)
        {
            IniciarJogo();

            foreach (var item in Jogadas!)
            {
                while (!Jogar(item)) ;

                MarcarJogada(item);

                if (VerificarVencedor())
                {
                    MostrarVencedor(item);
                    break;
                }
            }
        }

        static void IniciarJogo()
        {
            InicializarMatrizTabuleiro();
            EscolherJogador();
            CriarJogadores();
            DefinirJogadas();
        }

        static bool Jogar(char item)
        {
            MostrarTabuleiro();
            SolicitarJogada(item);
            return EfetuarValidacoes();
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

        static void EnviarMensagem(string mensagem)
        {
            Console.Clear();
            Console.WriteLine(mensagem);
            Console.ReadKey();
        }

        static void EscolherJogador()
        {
            bool opcaoValida = false;
            do
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Qual será o jogador um ?  X ou O ");
                    string inputOpcao = Console.ReadLine()!.ToUpper();

                    char opcao = Convert.ToChar(inputOpcao);
                    opcaoValida = opcao == 88 || opcao == 79 ? true : false;

                    if (opcaoValida) Jogador1 = opcao;
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
            Jogador2 = Jogador1 == 88 ? (char)79 : (char)88;
        }

        static void DefinirJogadas()
        {
            Jogadas = new char[9];

            for (int i = 0; i < Jogadas.Length; i++)
            {
                Jogadas[i] = (i % 2 == 0) ? Jogador1 : Jogador2;
            }
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
            Console.WriteLine($"\nJogador:  [ {jogador} ]   Escolha a posição da sua jogada.  Ex: 0,1");
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

        static void MarcarJogada(char jogador)
        {
            Tabuleiro![Coordenanda![0], Coordenanda[1]] = jogador.ToString();
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
        static void MostrarVencedor(char item)
        {
            MostrarTabuleiro();
            Console.Write($"\nDeu velha! Jogador {item} Venceu! ");
            Console.ReadKey();
        }
        
        static bool VerificarDiagonalDireita()
        {
            return Tabuleiro![0, 2] == Jogador1.ToString() && Tabuleiro[1, 1] == Jogador1.ToString() && Tabuleiro[2, 0] == Jogador1.ToString() ||
            Tabuleiro![0, 2] == Jogador2.ToString() && Tabuleiro[1, 1] == Jogador2.ToString() && Tabuleiro[2, 0] == Jogador2.ToString() ? true : false;
        }

        static bool VerificarDiagonalEsquerda()
        {
            return Tabuleiro![0, 0] == Jogador1.ToString() && Tabuleiro[1, 1] == Jogador1.ToString() && Tabuleiro[2, 2] == Jogador1.ToString() ||
            Tabuleiro![0, 0] == Jogador2.ToString() && Tabuleiro[1, 1] == Jogador2.ToString() && Tabuleiro[2, 2] == Jogador2.ToString() ? true : false;
        }

        static bool VerificarLinhaVertical()
        {
            for (int i = 0; i < 3; i++)
            {
                if (Tabuleiro![0, i] == Jogador1.ToString() && Tabuleiro![1, i] == Jogador1.ToString() && Tabuleiro![2, i] == Jogador1.ToString() ||
                  Tabuleiro![0, i] == Jogador2.ToString() && Tabuleiro![1, i] == Jogador2.ToString() && Tabuleiro![2, i] == Jogador2.ToString())
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
                if (Tabuleiro![i, 0] == Jogador1.ToString() && Tabuleiro![i, 1] == Jogador1.ToString() && Tabuleiro![i, 2] == Jogador1.ToString() ||
                 Tabuleiro![i, 0] == Jogador2.ToString() && Tabuleiro![i, 1] == Jogador2.ToString() && Tabuleiro![i, 2] == Jogador2.ToString())

                    return true;
            }

            return false;
        }
    }
}
