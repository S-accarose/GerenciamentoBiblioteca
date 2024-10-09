using System.ComponentModel;
using System.ComponentModel.Design;
using System.Net.Mail;
using System.Reflection;
using System.Threading;
using System.Xml;
class Program
{
    public static void Main()
    {
        int qtdcatalogo = 3;
        int adicionar = qtdcatalogo + 1;
        string catalogo = "";
        string usuario = "Convidado";
        string senha = "000000";
        string[] usuarios = new string[100];
        string[] senhaadministrador = {"2203454"};
        int x = 0;
        int login = 0;
        bool login1 = false;
        string[] livros = new string[100];
        string[] autores = new string[100];
        string[] generos = new string[100];
        int[] estoques = new int[100];

        livros[1] = "Mason D'Hanson"; livros[2] = "Colors to Color"; livros[3] = "Psicologia e seus vertentes";
        autores[1] = "Conan Crysp"; autores[2] = "Samanta Willy"; autores[3] = "Richard Pown";
        generos[1] = "Terror"; generos[2] = "Educativo"; generos[3] = "Artigo científico";
        estoques[1] = 5; estoques[2] = 2; estoques[3] = 8;

        Login(ref login1,x,login,usuarios,usuario,senha,senhaadministrador);

        if(login1)
        {
            Processo(qtdcatalogo,estoques,livros,autores,generos);
        }
        else if(!login1)
        {
            ProcessoADM(adicionar,qtdcatalogo,catalogo,livros,autores,generos,estoques);
        }
    }

    public static void Login(ref bool login1,int login, int x, string[] usuarios, string usuario, string senha, string[] senhaadministrador)
    {
        Console.Clear();
        Console.WriteLine("Bem vindo ao gerencimento online da Biblioteca!");
        Thread.Sleep(2000);
        Console.Clear();

         do
        {
            Console.Write("\nQual a sua forma de login?\n[1] Usuário   [2] Administrador\n> ");
        }while(!int.TryParse(Console.ReadLine(), out login) || login < 1 || login > 2);

        if(login == 1)
        {
            do
            {
                Console.WriteLine("É necessario um nome de usuário superior a 4 dígitos.");
                Console.Write("\nInsira o seu nome de usuário: ");
                usuario = Console.ReadLine() ?? "";
            }while(usuario.Length < 5);
            
            usuarios[x] = usuario;
            login1 = true;
        }
        else
        {
            do
            {
                Console.Write("\nInsira a senha do administrador: ");
                senha = Console.ReadLine() ?? "";
                if(senha != senhaadministrador[0])
                {
                    Console.WriteLine("Senha incorreta!");
                    Thread.Sleep(2000);
                    Console.Clear();
                }
            }while(senha.Length > 7 || senha.Length < 7 && senha != senhaadministrador[0]);
            login1 = false;
        }
    }
    public static void Processo(int qtdcatalogo, int[] estoques, string[] livros, string[] autores, string[] generos)
    {   
        char b;
        int a;
        Console.Clear();
        Console.WriteLine("Menu de Opções:");

        menu:
        do
        {
            Console.WriteLine("\n[1] Consultar Catálogo.\n[2] Fazer Empréstimo.\n[3] Fazer Devolução.\n[4] Sair.\n> ");
            a = int.Parse(Console.ReadLine() ?? "0");
        }while(a < 1 || a > 4);
        Console.Clear();

        switch(a)
        {
            case 1:

                Console.WriteLine($"\nSe encontram disponíveis no catálogo {qtdcatalogo} livros.");
                Console.WriteLine("\nOs seguintes Livros estão cadastrados no sistema:");
                for(int e = 1; e <= qtdcatalogo; e++)
                {
                    Console.WriteLine($"\nO {e}° livro é:\n{livros[e]}\nAutor: {autores[e]}\nGenero: {generos[e]}\nQtd. Estoque: {estoques[e]}\n");
                }
                Console.Write("\nDeseja realizar o empréstimo de algum livro? (S) Sim / (N) Não\n> ");
                if(!char.TryParse(Console.ReadLine(), out b) || b != 's')
                {
                    goto menu;
                }
                else
                {
                    goto emprestimo;
                }

            case 2:

            emprestimo:


            break;
            
            case 3:

            break;

            case 4:

                Console.WriteLine("Obrigado por utilizar nossos serviços.");
                Console.WriteLine("Encerrando sistema.");
                Thread.Sleep(4000);
                Console.Clear();

            break;
        }
    }

    public static void ProcessoADM(int adicionar,int qtdcatalogo, string catalogo, string[] livros, string[] autores, string[] generos, int[] estoques)
    {
        char b;
        string livro = "";
        string autor = "";
        string genero = "";
        int estoque;
        int a;

        Console.Clear();
        Console.WriteLine("Menu de Opções:");

        menu:
        do
        {
            Console.WriteLine("\n[1] Consultar Catálogo.\n[2] Fazer Empréstimo.\n[3] Fazer Devolução.\n[4] Adicionar livro ao catálogo.\n[5] Sair.\n> ");
            a = int.Parse(Console.ReadLine() ?? "0");
        }while(a < 1 || a > 4);
        
        Console.Clear();
        switch(a)
        {
            case 1:

                Console.WriteLine($"\nSe encontram disponíveis no catálogo {qtdcatalogo} livros.");
                Console.WriteLine("\nOs seguintes Livros estão cadastrados no sistema:");
                for(int e = 1; e <= qtdcatalogo; e++)
                {
                    Console.WriteLine($"\nO {e} livro é:\n{livros[e]}\nAutor: {autores[e]}\nGenero: {generos[e]}\nQtd. Estoque: {estoques[e]}\n");
                }
                Console.Write("\nDeseja realizar o empréstimo de algum livro? (S) Sim / (N) Não\n> ");
                if(!char.TryParse(Console.ReadLine(), out b) || b != 's')
                {
                    goto menu;
                }
                else
                {
                    goto emprestimo;
                }
                
            case 2:

                emprestimo:

            break;
            
            case 3:

            break;

            case 4:

                do
                {
                    n:
                    Console.Write("\nQual nome do livro que deseja adicionar? ");
                    livro = Console.ReadLine() ?? "";
                     if(livro == "")
                    {
                        Console.WriteLine("Insira um nome válido");
                        Thread.Sleep(1500);
                        goto n;
                    }
                    Console.Clear();

                    au:
                    Console.Write("\nQual nome do autor do livro que deseja adicionar? ");
                    autor = Console.ReadLine() ?? "";
                     if(autor == "")
                    {
                        Console.WriteLine("Insira um nome válido");
                        Thread.Sleep(1500);
                        goto au;
                    }
                    Console.Clear();

                    gen:
                    Console.Write("\nQual genero do livro que deseja adicionar? ");
                    genero = Console.ReadLine() ?? "";
                     if(genero == "")
                    {
                        Console.WriteLine("Insira um nome válido");
                        Thread.Sleep(1500);
                        goto gen;
                    }
                    Console.Clear();

                     est:
                    Console.Write("\nQuantos deste livro irá adicionar ao estoque? ");
                    estoque = int.Parse(Console.ReadLine() ?? "0");
                     if(estoque == 0)
                    {
                        Console.WriteLine("Insira um valor válido");
                        Thread.Sleep(1500);
                        goto est;
                    }
                    Console.Clear();
                }while(livro == "" ||autor == "" || genero == "");

                int place = Array.IndexOf(livros, livro);
                if(livros.Contains(livro))
                {
                    estoques[place] = estoques[place] + estoque;
                }
                else if(!livros.Contains(livro))
                {
                    livros[adicionar] = livro;
                    autores[adicionar] = autor;
                    generos[adicionar] = genero;
                    estoques[adicionar] = estoque;
                }
                
                qtdcatalogo++;
                Console.WriteLine("Livro adicionado com sucesso.");

            break;

            case 5:

                Console.WriteLine("Obrigado por utilizar nossos serviços.");
                Console.WriteLine("Encerrando sistema.");
                Thread.Sleep(4000);
                Console.Clear();

            break;
        }

    }
}
