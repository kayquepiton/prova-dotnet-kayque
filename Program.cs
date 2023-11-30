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