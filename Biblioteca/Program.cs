using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data.SqlTypes;
using System.Diagnostics;
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
        string senha = "0000000";
        string[] usuarios = new string[100];
        string[] senhaadministrador = {"2203454"};
        int x = 0;
        int log = 0;
        bool login = false;
        string[] livros = new string[100];
        string[] autores = new string[100];
        string[] generos = new string[100];
        int[] estoques = new int[100];

        livros[1] = "Mason D'Hanson"; livros[2] = "Colors to Color"; livros[3] = "Psicologia e seus vertentes";
        autores[1] = "Conan Crysp"; autores[2] = "Samanta Willy"; autores[3] = "Richard Pown";
        generos[1] = "Terror"; generos[2] = "Educativo"; generos[3] = "Artigo científico";
        estoques[1] = 5; estoques[2] = 2; estoques[3] = 8;

        Login(ref login,x,log,usuarios,usuario,senha,senhaadministrador);

        Processo(ref login,adicionar,qtdcatalogo,catalogo,livros,autores,generos,estoques);
    }

    public static void Login(ref bool login,int log, int x, string[] usuarios, string usuario, string senha, string[] senhaadministrador)
    {
        Console.Clear();
        Console.WriteLine("Bem vindo ao gerencimento online da Biblioteca!");
        Thread.Sleep(2000);
        Console.Clear();

         do
        {
            Console.Write("\nQual a sua forma de login?\n[1] Usuário   [2] Administrador\n> ");
        }while(!int.TryParse(Console.ReadLine(), out log) || log < 1 || log > 2);

        if(log == 1)
        {
            Console.Clear();
            do
            {
                Console.WriteLine("É necessario um nome de usuário superior a 4 dígitos.");
                Thread.Sleep(1500);
                Console.Clear();
                Console.Write("\nInsira o seu nome de usuário: ");
                usuario = Console.ReadLine() ?? "";
            }while(usuario.Length < 5);
            
            usuarios[x] = usuario;
            login = false;
        }
        else
        {
            Console.Clear();
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
            login = true;
        }
    }
  
    public static void Processo(ref bool login,int adicionar,int qtdcatalogo, string catalogo, string[] livros, string[] autores, string[] generos, int[] estoques)
    {
        
        string devolucao; char sn; string emprestimo; int lemp = 0; string livro = ""; string autor = ""; string genero = ""; char b; int estoque; int a;

        Console.Clear();
        Console.WriteLine("Menu de Opções:");

        menu:
        Console.Clear();
        do
        {
            Console.WriteLine("\n[1] Consultar Catálogo.\n[2] Fazer Empréstimo.\n[3] Fazer Devolução.\n[4] Adicionar livro ao catálogo.\n[5] Sair.\n> ");
            a = int.Parse(Console.ReadLine() ?? "0");
        }while(a < 1 || a > 5);
        
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
                if(!char.TryParse(Console.ReadLine() ?? "".ToLower(), out b) || b != 's')
                {
                    goto menu;
                }
                else
                {
                    goto emprestimo;
                }
                
            case 2:

                emprestimo:
                do
                {
                    Console.Write("Insira o nome do livro que deseja realizar o empréstimo: ");
                    emprestimo = Console.ReadLine() ?? "";
                }while(emprestimo == "");
                
                for(int x = 1; x <= qtdcatalogo; x++)
                {
                    if(emprestimo==livros[x])
                    {
                        if(lemp < 3)
                        {
                            Console.Clear();
                            Console.WriteLine($"Parabéns, você acaba de realizar o empréstimo do livro {emprestimo}");
                            lemp = lemp++;
                            estoques[x] = estoques[x]--;
                            Thread.Sleep(2000);
                            Console.WriteLine("Voltando ao menu.");
                            Thread.Sleep(3000);
                            goto menu;

                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Você atingiu o limite de empréstimos, realize uma devolução para continuar com o empréstimo.");
                            Thread.Sleep(2000);
                            do
                            {
                                Console.Write("Deseja continuar para a devolução? (S) Sim/(N) Não");
                                sn = char.Parse(Console.ReadLine() ?? "".ToLower());
                            }while(sn != 'n' || sn != 's');

                            if(sn == 's')
                            {
                                goto devolucao;
                            }
                            else
                            {
                                Console.WriteLine("Voltando ao menu.");
                                Thread.Sleep(3000);
                                goto menu;
                            }
                            
                        }
                        
                    }  
                }
                

            break;
            
            case 3:

                devolucao:

                do
                {
                    Console.Clear();
                    Console.Write("Insira o nome do livro que deseja realizar a devolução: ");
                    devolucao = Console.ReadLine() ?? "";
                }while(devolucao == "");
                
                for(int x = 1; x <= qtdcatalogo; x++)
                {
                    if(devolucao==livros[x])
                    {
                        if(lemp > 0)
                        {   
                            Console.Clear();
                            Console.WriteLine($"Parabéns, você realizou a devolução do livro {devolucao}");
                            lemp--;
                            estoques[x] = estoques[x]++;
                            Thread.Sleep(2000);
                            Console.Clear();
                            Console.WriteLine("Voltando ao menu.");
                            Thread.Sleep(3000);
                            goto menu;
                        }
                        
                    }
                }
            break;

            case 4:
                if(login)
                {
                    do
                    {
                        n:
                        Console.Clear();
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
                }
                else
                {
                    Console.WriteLine("Opção reservada aos administradores.");
                    Thread.Sleep(2000);
                    goto menu;
                }
                Console.WriteLine("Voltando ao menu.");
                Thread.Sleep(3000);
                goto menu;

            case 5:
                Console.WriteLine("Obrigado por utilizar nossos serviços.");
                Thread.Sleep(1500);
                Console.WriteLine("Encerrando sistema.");
                Thread.Sleep(2000);
            break;
            
        }
        

    }
}
