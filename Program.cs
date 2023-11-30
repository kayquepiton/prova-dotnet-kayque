using System;
using System.Collections.Generic;
using System.Linq;

class GerenciadorEscritorioAdvogacia{
    static void Main(){

        // Criando algumas instâncias de advogados e clientes para teste
        List<Advogado> advogados = new List<Advogado>{
            new Advogado("Adv1", new DateTime(1990, 1, 15), "12345678901", "CNA123"),
            new Advogado("Adv2", new DateTime(1985, 5, 20), "98765432101", "CNA456")
        };

        List<Cliente> clientes = new List<Cliente>{
            new Cliente("Cliente1", new DateTime(1980, 3, 10), "11122233344", EstadoCivil.Solteiro, "Engenheiro"),
            new Cliente("Cliente2", new DateTime(1995, 8, 25), "55566677788", EstadoCivil.Casado, "Médico")
        };

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