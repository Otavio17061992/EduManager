-- ============================================
-- Script para povoar dados da Professora
-- EduManager - Disciplinas, Turmas e Alunos
-- ============================================
DECLARE @ProfessorUserId NVARCHAR(450);
DECLARE @ProfessorId INT;
DECLARE @CursoId INT;
DECLARE @AlunoId INT;
-- 1. Obter IDs da Professora e do Aluno
SELECT @ProfessorUserId = Id
FROM Usuarios
WHERE Email = 'professor@teste.com';
SELECT @ProfessorId = ProfessorId
FROM Professores
WHERE UserId = @ProfessorUserId;
SELECT TOP 1 @CursoId = CursoId
FROM Cursos;
-- Pega o primeiro curso disponível
SELECT @AlunoId = AlunoId
FROM Alunos
WHERE AlunoEmail = 'aluno@teste.com';
IF @ProfessorId IS NOT NULL BEGIN PRINT 'Encontrado Professor ID: ' + CAST(@ProfessorId AS VARCHAR);
-- 2. Inserir Novas Disciplinas para a Professora
-- Incluindo CargaHoraria (obrigatória)
INSERT INTO Disciplinas (Nome, Codigo, CursoId, ProfessorId, CargaHoraria)
VALUES (
        'Desenvolvimento Web Avançado',
        'WEB-301',
        @CursoId,
        @ProfessorId,
        80
    ),
    (
        'Arquitetura de Software',
        'ARQ-402',
        @CursoId,
        @ProfessorId,
        60
    );
PRINT '✓ Disciplinas criadas';
-- Obter IDs das disciplinas recém criadas
DECLARE @DiscWebId INT;
DECLARE @DiscArqId INT;
SELECT @DiscWebId = DisciplinaId
FROM Disciplinas
WHERE Codigo = 'WEB-301';
SELECT @DiscArqId = DisciplinaId
FROM Disciplinas
WHERE Codigo = 'ARQ-402';
-- 3. Inserir Turmas para essas disciplinas
IF @DiscWebId IS NOT NULL
AND @DiscArqId IS NOT NULL BEGIN
INSERT INTO Turmas (
        Nome,
        Ano,
        Semestre,
        Ativa,
        DataInicio,
        DataFim,
        CursoId,
        DisciplinaId,
        ProfessorId
    )
VALUES (
        'Turma 2024-1 Web',
        '2024',
        1,
        1,
        '2024-02-01',
        '2024-06-30',
        @CursoId,
        @DiscWebId,
        @ProfessorId
    ),
    (
        'Turma 2024-1 Arq',
        '2024',
        1,
        1,
        '2024-02-01',
        '2024-06-30',
        @CursoId,
        @DiscArqId,
        @ProfessorId
    ),
    (
        'Turma 2024-2 Web',
        '2024',
        2,
        0,
        '2024-08-01',
        '2024-12-15',
        @CursoId,
        @DiscWebId,
        @ProfessorId
    );
PRINT '✓ Turmas criadas';
END
ELSE BEGIN PRINT '✗ Erro ao recuperar IDs das disciplinas criadas.';
END -- 4. Simular Alunos na Turma (via Frequência e Notas)
-- Como não vi tabela de MatriculaExplícita, vou inserir Frequencia para o aluno aparecer na contagem
DECLARE @TurmaWebId INT;
SELECT @TurmaWebId = TurmaId
FROM Turmas
WHERE Nome = 'Turma 2024-1 Web';
IF @AlunoId IS NOT NULL
AND @TurmaWebId IS NOT NULL BEGIN
INSERT INTO Frequencias (
        DataAula,
        Presente,
        AlunoId,
        DisciplinaId,
        TurmaId
    )
VALUES (GETDATE(), 1, @AlunoId, @DiscWebId, @TurmaWebId);
PRINT '✓ Aluno vinculado à turma Web via Frequência';
-- Inserir também uma nota
INSERT INTO Notas (
        Valor,
        TipoAvaliacao,
        DataAvaliacao,
        AlunoId,
        DisciplinaId
    )
VALUES (8.5, 'Prova 1', GETDATE(), @AlunoId, @DiscWebId);
PRINT '✓ Nota lançada para aluno';
END
END
ELSE BEGIN PRINT '✗ Erro: Professor não encontrado. Rode o script de criação de usuários primeiro.';
END