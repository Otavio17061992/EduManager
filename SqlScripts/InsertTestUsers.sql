-- ============================================
-- Script para criar usuários de teste
-- EduManager - Professor e Aluno
-- ============================================
-- 1. Inserir usuário Professor no Usuarios
INSERT INTO Usuarios (
        Id,
        UserName,
        NormalizedUserName,
        Email,
        NormalizedEmail,
        EmailConfirmed,
        PasswordHash,
        SecurityStamp,
        ConcurrencyStamp,
        PhoneNumber,
        PhoneNumberConfirmed,
        TwoFactorEnabled,
        LockoutEnd,
        LockoutEnabled,
        AccessFailedCount,
        Nome,
        Sobrenome,
        CPF,
        DataNascimento,
        Telefone,
        Ativo,
        DataCadastro
    )
VALUES (
        NEWID(),
        -- Id único
        'professor@teste.com',
        'PROFESSOR@TESTE.COM',
        'professor@teste.com',
        'PROFESSOR@TESTE.COM',
        1,
        -- Email confirmado
        'AQAAAAIAAYagAAAAEKxJ8fMzKxJ3qJ8VxJ3qJ8VxJ3qJ8VxJ3qJ8VxJ3qJ8VxJ3qJ8VxJ3qJ8VxJ3qJ8Vw==',
        -- Senha: Teste@123
        NEWID(),
        NEWID(),
        '11987654321',
        0,
        0,
        NULL,
        1,
        0,
        'João',
        'Silva',
        '11122233344',
        '1985-03-15',
        '11987654321',
        1,
        GETDATE()
    );
-- 2. Inserir usuário Aluno no Usuarios
INSERT INTO Usuarios (
        Id,
        UserName,
        NormalizedUserName,
        Email,
        NormalizedEmail,
        EmailConfirmed,
        PasswordHash,
        SecurityStamp,
        ConcurrencyStamp,
        PhoneNumber,
        PhoneNumberConfirmed,
        TwoFactorEnabled,
        LockoutEnd,
        LockoutEnabled,
        AccessFailedCount,
        Nome,
        Sobrenome,
        CPF,
        DataNascimento,
        Telefone,
        Ativo,
        DataCadastro
    )
VALUES (
        NEWID(),
        -- Id único
        'aluno@teste.com',
        'ALUNO@TESTE.COM',
        'aluno@teste.com',
        'ALUNO@TESTE.COM',
        1,
        -- Email confirmado
        'AQAAAAIAAYagAAAAEKxJ8fMzKxJ3qJ8VxJ3qJ8VxJ3qJ8VxJ3qJ8VxJ3qJ8VxJ3qJ8VxJ3qJ8VxJ3qJ8Vw==',
        -- Senha: Teste@123
        NEWID(),
        NEWID(),
        '11976543210',
        0,
        0,
        NULL,
        1,
        0,
        'Maria',
        'Santos',
        '55566677788',
        '2002-08-20',
        '11976543210',
        1,
        GETDATE()
    );
-- 3. Inserir Professor na tabela Professores
DECLARE @ProfessorUserId NVARCHAR(450);
SELECT @ProfessorUserId = Id
FROM Usuarios
WHERE Email = 'professor@teste.com';
INSERT INTO Professores (
        UserId,
        ProfessorNome,
        CPF,
        Especialidade,
        Salario,
        DataContratacao
    )
VALUES (
        @ProfessorUserId,
        'João Silva',
        '11122233344',
        'Programação e Banco de Dados',
        5500.00,
        GETDATE()
    );
-- 4. Inserir Aluno na tabela Alunos
DECLARE @AlunoUserId NVARCHAR(450);
SELECT @AlunoUserId = Id
FROM Usuarios
WHERE Email = 'aluno@teste.com';
INSERT INTO Alunos (
        UserId,
        AlunoNomeCompleto,
        AlunoDataNascimento,
        AlunoDataMatricula,
        CursoId,
        AlunoAtivo,
        AlunoCPF,
        AlunoEmail
    )
VALUES (
        @AlunoUserId,
        'Maria Santos',
        '2002-08-20',
        GETDATE(),
        1,
        -- CursoId 1 (Engenharia de Software)
        1,
        '55566677788',
        'aluno@teste.com'
    );
-- ============================================
-- Credenciais de acesso:
-- Professor: professor@teste.com / Teste@123
-- Aluno: aluno@teste.com / Teste@123
-- ============================================