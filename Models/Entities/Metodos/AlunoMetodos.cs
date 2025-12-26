using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduManager.InfraEstrutura.Data;
using EduManager.Models.Entities.Dominios;
using EduManager.Models.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduManager.Models.Entities.Metodos;

public class AlunoMetodos : IAlunosRepository
{

    private readonly EduManagerContext _context;

    public async Task AdicionarAsync(AlunoDominio aluno)
    {
        await _context.Aluno.AddAsync(aluno);
        await _context.SaveChangesAsync();
    }

    public Task AtualizarAsync(AlunoDominio aluno)
    {
        _context.Aluno.Update(aluno);
        return _context.SaveChangesAsync();
    }

    public async Task<AlunoDominio?> ObterPorCpfAsync(string cpf)
    {
        return await _context.Aluno
            .FirstOrDefaultAsync(a => a.AlunoCPF == cpf);
    }

    public async Task<AlunoDominio?> ObterPorEmailAsync(string email)
    {
        return await _context.Aluno
            .FirstOrDefaultAsync(a => a.AlunoEmail == email);
    }

    public async Task<AlunoDominio?> ObterPorIdAsync(int id)
    {
        return await _context.Aluno
            .FirstOrDefaultAsync(a => a.AlunoId == id);
    }

    public async Task<AlunoDominio?> ObterPorNomeAsync(string nome)
    {
        return await _context.Aluno
            .FirstOrDefaultAsync(a => a.AlunoNomeCompleto == nome);
    }

    public async Task<IEnumerable<AlunoDominio>> ListarAsync()
    {
        return await _context.Aluno.ToListAsync();
    }

    public async Task<bool> ExisteAsync(int id)
    {
        return await _context.Aluno.AnyAsync(a => a.AlunoId == id);
    }

    public async Task RemoverAsync(int id)
    {
        var aluno = await ObterPorIdAsync(id);
        if (aluno != null)
        {
            _context.Aluno.Remove(aluno);
            await _context.SaveChangesAsync();
        }
    }
    }
