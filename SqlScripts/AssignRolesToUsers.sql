-- ============================================
-- Script para criar Roles e atribuir aos usuários
-- EduManager - Roles: Professor e Aluno
-- ============================================
-- 1. Criar Role "Professor" se não existir
IF NOT EXISTS (
    SELECT *
    FROM AspNetRoles
    WHERE Name = 'Professor'
) BEGIN
INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp)
VALUES (NEWID(), 'Professor', 'PROFESSOR', NEWID());
END -- 2. Criar Role "Aluno" se não existir
IF NOT EXISTS (
    SELECT *
    FROM AspNetRoles
    WHERE Name = 'Aluno'
) BEGIN
INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp)
VALUES (NEWID(), 'Aluno', 'ALUNO', NEWID());
END -- 3. Atribuir role "Professor" ao usuário professor@teste.com
DECLARE @ProfessorUserId NVARCHAR(450);
DECLARE @ProfessorRoleId NVARCHAR(450);
SELECT @ProfessorUserId = Id
FROM Usuarios
WHERE Email = 'professor@teste.com';
SELECT @ProfessorRoleId = Id
FROM AspNetRoles
WHERE Name = 'Professor';
IF @ProfessorUserId IS NOT NULL
AND @ProfessorRoleId IS NOT NULL BEGIN -- Verificar se já não está atribuído
IF NOT EXISTS (
    SELECT *
    FROM AspNetUserRoles
    WHERE UserId = @ProfessorUserId
        AND RoleId = @ProfessorRoleId
) BEGIN
INSERT INTO AspNetUserRoles (UserId, RoleId)
VALUES (@ProfessorUserId, @ProfessorRoleId);
PRINT '✓ Role Professor atribuída ao usuário professor@teste.com';
END
ELSE BEGIN PRINT '⚠ Usuário professor@teste.com já possui a role Professor';
END
END
ELSE BEGIN PRINT '✗ Erro: Usuário ou Role não encontrados';
END -- 4. Atribuir role "Aluno" ao usuário aluno@teste.com
DECLARE @AlunoUserId NVARCHAR(450);
DECLARE @AlunoRoleId NVARCHAR(450);
SELECT @AlunoUserId = Id
FROM Usuarios
WHERE Email = 'aluno@teste.com';
SELECT @AlunoRoleId = Id
FROM AspNetRoles
WHERE Name = 'Aluno';
IF @AlunoUserId IS NOT NULL
AND @AlunoRoleId IS NOT NULL BEGIN -- Verificar se já não está atribuído
IF NOT EXISTS (
    SELECT *
    FROM AspNetUserRoles
    WHERE UserId = @AlunoUserId
        AND RoleId = @AlunoRoleId
) BEGIN
INSERT INTO AspNetUserRoles (UserId, RoleId)
VALUES (@AlunoUserId, @AlunoRoleId);
PRINT '✓ Role Aluno atribuída ao usuário aluno@teste.com';
END
ELSE BEGIN PRINT '⚠ Usuário aluno@teste.com já possui a role Aluno';
END
END
ELSE BEGIN PRINT '✗ Erro: Usuário ou Role não encontrados';
END -- ============================================
-- Verificar roles atribuídas
-- ============================================
SELECT u.Email,
    r.Name as Role
FROM Usuarios u
    INNER JOIN AspNetUserRoles ur ON u.Id = ur.UserId
    INNER JOIN AspNetRoles r ON ur.RoleId = r.Id
WHERE u.Email IN ('professor@teste.com', 'aluno@teste.com');