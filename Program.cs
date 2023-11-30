using System;
using System.Collections.Generic;
using System.Linq;

class GerenciadorEscritoriaAdvogacia{
    static void Main(){
        List<Advogado> advogados = new List<Advogado>();
        List<Cliente> clientes = new List<Cliente>();

        // Entrada de dados para advogados
        do{
            Console.WriteLine("\nCadastro de Advogados:");
            Advogado advogado = CadastrarAdvogado();
            advogados.Add(advogado);

            Console.Write("Deseja cadastrar outro advogado? (S/N): ");
        } while (Console.ReadLine().ToUpper() == "S");

        // Entrada de dados para clientes
        do
        {
            Console.WriteLine("\nCadastro de Clientes:");
            Cliente cliente = CadastrarCliente();
            clientes.Add(cliente);

            Console.Write("Deseja cadastrar outro cliente? (S/N): ");
        } while (Console.ReadLine().ToUpper() == "S");

        // Relatório 1: Advogados com idade entre dois valores
        var advogadosPorIdade = advogados.Where(a => a.Idade >= 30 && a.Idade <= 40);
        Console.WriteLine("\nAdvogados com idade entre 30 e 40 anos:");
        foreach (var adv in advogadosPorIdade)
        {
            Console.WriteLine($"Nome: {adv.Nome}, Idade: {adv.Idade}");
        }

        // Relatório 2: Clientes com idade entre dois valores
        var clientesPorIdade = clientes.Where(c => c.Idade >= 25 && c.Idade <= 35);
        Console.WriteLine("\nClientes com idade entre 25 e 35 anos:");
        foreach (var cli in clientesPorIdade)
        {
            Console.WriteLine($"Nome: {cli.Nome}, Idade: {cli.Idade}");
        }

        // Relatório 3: Clientes com estado civil informado pelo usuário
        Console.Write("\nInforme o estado civil (Solteiro ou Casado): ");
        EstadoCivil estadoCivilInformado;
        if (!Enum.TryParse(Console.ReadLine(), true, out estadoCivilInformado))
        {
            Console.WriteLine("Estado civil inválido. Relatório não gerado.");
        }
        else
        {
            var clientesPorEstadoCivil = clientes.Where(c => c.EstadoCivil == estadoCivilInformado);
            Console.WriteLine($"\nClientes com estado civil {estadoCivilInformado}:");
            foreach (var cli in clientesPorEstadoCivil)
            {
                Console.WriteLine($"Nome: {cli.Nome}, Estado Civil: {cli.EstadoCivil}");
            }
        }

        // Relatório 4: Clientes em ordem alfabética
        var clientesOrdemAlfabetica = clientes.OrderBy(c => c.Nome);
        Console.WriteLine("\nClientes em ordem alfabética:");
        foreach (var cli in clientesOrdemAlfabetica)
        {
            Console.WriteLine($"Nome: {cli.Nome}");
        }

        // Relatório 5: Clientes cuja profissão contenha texto informado pelo usuário
        Console.Write("\nInforme um texto para buscar nas profissões dos clientes: ");
        string textoBusca = Console.ReadLine();
        var clientesPorProfissao = clientes.Where(c => c.Profissao.Contains(textoBusca, StringComparison.OrdinalIgnoreCase));
        Console.WriteLine($"\nClientes cuja profissão contém '{textoBusca}':");
        foreach (var cli in clientesPorProfissao)
        {
            Console.WriteLine($"Nome: {cli.Nome}, Profissão: {cli.Profissao}");
        }

        // Relatório 6: Advogados e Clientes aniversariantes do mês informado
        Console.Write("\nInforme o mês para buscar aniversariantes (1 a 12): ");
        if (int.TryParse(Console.ReadLine(), out int mesAniversario))
        {
            try
            {
                var aniversariantesAdvogados = advogados.Where(a => a.DataNascimento.Month == mesAniversario);
                var aniversariantesClientes = clientes.Where(c => c.DataNascimento.Month == mesAniversario);

                Console.WriteLine($"\nAdvogados aniversariantes do mês {mesAniversario}:");
                foreach (var adv in aniversariantesAdvogados)
                {
                    Console.WriteLine($"Nome: {adv.Nome}, Data de Nascimento: {adv.DataNascimento:dd/MM/yyyy}");
                }

                Console.WriteLine($"\nClientes aniversariantes do mês {mesAniversario}:");
                foreach (var cli in aniversariantesClientes)
                {
                    Console.WriteLine($"Nome: {cli.Nome}, Data de Nascimento: {cli.DataNascimento:dd/MM/yyyy}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao gerar relatório de aniversariantes: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Entrada inválida para o mês. Relatório não gerado.");
        }
    }

    static Advogado CadastrarAdvogado()
    {
        Console.Write("Nome: ");
        string nomeAdvogado = Console.ReadLine();

        Console.Write("Data de Nascimento (dd/mm/aaaa): ");
        DateTime dataNascimentoAdvogado;
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dataNascimentoAdvogado))
        {
            Console.WriteLine("Data de nascimento inválida. Tente novamente.");
            return CadastrarAdvogado();
        }

        Console.Write("CPF (11 dígitos): ");
        string cpfAdvogado = Console.ReadLine();

        Console.Write("CNA: ");
        string cnaAdvogado = Console.ReadLine();

        try
        {
            return new Advogado(nomeAdvogado, dataNascimentoAdvogado, cpfAdvogado, cnaAdvogado);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Erro ao cadastrar Advogado: {ex.Message}");
            return CadastrarAdvogado(); // Repetir a entrada se houver um erro
        }
    }

    static Cliente CadastrarCliente()
    {
        Console.Write("Nome: ");
        string nomeCliente = Console.ReadLine();

        Console.Write("Data de Nascimento (dd/mm/aaaa): ");
        DateTime dataNascimentoCliente;
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dataNascimentoCliente))
        {
            Console.WriteLine("Data de nascimento inválida. Tente novamente.");
            return CadastrarCliente();
        }

        Console.Write("CPF (11 dígitos): ");
        string cpfCliente = Console.ReadLine();

        Console.Write("Estado Civil (Solteiro ou Casado): ");
        EstadoCivil estadoCivilCliente;
        if (!Enum.TryParse(Console.ReadLine(), true, out estadoCivilCliente))
        {
            Console.WriteLine("Estado civil inválido. Digite Solteiro ou Casado.");
            return CadastrarCliente(); // Repetir a entrada se houver um erro
        }

        Console.Write("Profissão: ");
        string profissaoCliente = Console.ReadLine();

        try
        {
            return new Cliente(nomeCliente, dataNascimentoCliente, cpfCliente, estadoCivilCliente, profissaoCliente);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Erro ao cadastrar Cliente: {ex.Message}");
            return CadastrarCliente(); // Repetir a entrada se houver um erro
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