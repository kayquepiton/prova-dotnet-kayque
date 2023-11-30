using System;
using System.Collections.Generic;
using System.Linq;

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