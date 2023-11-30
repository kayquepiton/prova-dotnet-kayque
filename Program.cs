using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<Advogado> advogados = new List<Advogado>();
        List<Cliente> clientes = new List<Cliente>();

        // Entrada de dados para advogados
        for (int i = 0; i < 2; i++){
            Console.WriteLine("Digite os dados do Advogado:");
            Console.Write("Nome: ");
            string nomeAdvogado = Console.ReadLine();

            Console.Write("Data de Nascimento (yyyy/mm/dd): ");
            DateTime dataNascimentoAdvogado = DateTime.Parse(Console.ReadLine());

            Console.Write("CPF (11 dígitos): ");
            string cpfAdvogado = Console.ReadLine();

            Console.Write("CNA: ");
            string cnaAdvogado = Console.ReadLine();

            try{
                Advogado advogado = new Advogado(nomeAdvogado, dataNascimentoAdvogado, cpfAdvogado, cnaAdvogado);
                advogados.Add(advogado);
            }
            catch (ArgumentException ex){
                Console.WriteLine($"Erro ao cadastrar Advogado: {ex.Message}");
                i--; // Repetir a iteração se houver um erro
            }
        }

        // Entrada de dados para clientes
        for (int i = 0; i < 2; i++){
            Console.WriteLine("\nDigite os dados do Cliente:");
            Console.Write("Nome: ");
            string nomeCliente = Console.ReadLine();

            Console.Write("Data de Nascimento (yyyy/mm/dd): ");
            DateTime dataNascimentoCliente = DateTime.Parse(Console.ReadLine());

            Console.Write("CPF (11 dígitos): ");
            string cpfCliente = Console.ReadLine();

            Console.Write("Estado Civil (Solteiro ou Casado): ");
            EstadoCivil estadoCivilCliente;
            if (!Enum.TryParse(Console.ReadLine(), true, out estadoCivilCliente)){
                Console.WriteLine("Estado civil inválido. Digite Solteiro ou Casado.");
                i--; // Repetir a iteração se houver um erro
                continue;
            }

            Console.Write("Profissão: ");
            string profissaoCliente = Console.ReadLine();

            try{
                Cliente cliente = new Cliente(nomeCliente, dataNascimentoCliente, cpfCliente, estadoCivilCliente, profissaoCliente);
                clientes.Add(cliente);
            }
            catch (ArgumentException ex){
                Console.WriteLine($"Erro ao cadastrar Cliente: {ex.Message}");
                i--; // Repetir a iteração se houver um erro
            }
        }

        // Relatório 1: Advogados com idade entre dois valores
        var advogadosPorIdade = advogados.Where(a => a.Idade >= 30 && a.Idade <= 40);
        Console.WriteLine("Advogados com idade entre 30 e 40 anos:");
        foreach (var adv in advogadosPorIdade){
            Console.WriteLine($"Nome: {adv.Nome}, Idade: {adv.Idade}");
        }

        // Relatório 2: Clientes com idade entre dois valores
        var clientesPorIdade = clientes.Where(c => c.Idade >= 25 && c.Idade <= 35);
        Console.WriteLine("\nClientes com idade entre 25 e 35 anos:");
        foreach (var cli in clientesPorIdade){
            Console.WriteLine($"Nome: {cli.Nome}, Idade: {cli.Idade}");
        }

        // Relatório 3: Clientes com estado civil informado pelo usuário
        EstadoCivil estadoCivilInformado = EstadoCivil.Casado; // Substitua pelo valor informado pelo usuário
        var clientesPorEstadoCivil = clientes.Where(c => c.EstadoCivil == estadoCivilInformado);
        Console.WriteLine($"\nClientes com estado civil {estadoCivilInformado}:");
        foreach (var cli in clientesPorEstadoCivil){
            Console.WriteLine($"Nome: {cli.Nome}, Estado Civil: {cli.EstadoCivil}");
        }

        // Relatório 4: Clientes em ordem alfabética
        var clientesOrdenados = clientes.OrderBy(c => c.Nome);
        Console.WriteLine("\nClientes em ordem alfabética:");
        foreach (var cli in clientesOrdenados){
            Console.WriteLine($"Nome: {cli.Nome}");
        }

        // Relatório 5: Clientes cuja profissão contenha texto informado pelo usuário
        string textoProfissao = "Médico"; // Substitua pelo texto informado pelo usuário
        var clientesPorProfissao = clientes.Where(c => c.Profissao.Contains(textoProfissao));
        Console.WriteLine($"\nClientes com profissão contendo '{textoProfissao}':");
        foreach (var cli in clientesPorProfissao){
            Console.WriteLine($"Nome: {cli.Nome}, Profissão: {cli.Profissao}");
        }

        // Relatório 6: Advogados e Clientes aniversariantes do mês informado
        int mesAniversario = 8; // Substitua pelo mês informado pelo usuário
        var aniversariantesDoMes = advogados.Concat<IPessoa>(clientes)
                                            .Where(p => p.DataNascimento.Month == mesAniversario);
        Console.WriteLine($"\nAniversariantes do mês {mesAniversario}:");
        foreach (var pessoa in aniversariantesDoMes){
            Console.WriteLine($"Nome: {pessoa.Nome}, Data de Nascimento: {pessoa.DataNascimento:dd/MM/yyyy}");
        }
        
    }
}

// Enum para o estado civil
enum EstadoCivil{
    Solteiro,
    Casado
}

// Interface para representar Advogado e Cliente
interface IPessoa{
    string Nome { get; }
    DateTime DataNascimento { get; }
}

// Classe base para Advogado e Cliente
abstract class Pessoa : IPessoa{
    public string Nome { get; }
    public DateTime DataNascimento { get; }

    protected Pessoa(string nome, DateTime dataNascimento){
        Nome = nome;
        DataNascimento = dataNascimento;
    }

    public int Idade => DateTime.Now.Year - DataNascimento.Year;
}

// Classe Advogado
class Advogado : Pessoa{
    public string CPF { get; }
    public string CNA { get; }

    public Advogado(string nome, DateTime dataNascimento, string cpf, string cna)
        : base(nome, dataNascimento)
    {
        // Validar CPF com 11 dígitos
        if (cpf.Length != 11 || !cpf.All(char.IsDigit)){
            throw new ArgumentException("CPF inválido");
        }

        CPF = cpf;
        CNA = cna;
    }
}

// Classe Cliente
class Cliente : Pessoa{
    public string CPF { get; }
    public EstadoCivil EstadoCivil { get; }
    public string Profissao { get; }

    public Cliente(string nome, DateTime dataNascimento, string cpf, EstadoCivil estadoCivil, string profissao)
        : base(nome, dataNascimento)
    {
        // Validar CPF com 11 dígitos
        if (cpf.Length != 11 || !cpf.All(char.IsDigit)){
            throw new ArgumentException("CPF inválido");
        }

        CPF = cpf;
        EstadoCivil = estadoCivil;
        Profissao = profissao;
    }
}