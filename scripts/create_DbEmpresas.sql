
CREATE DATABASE IF NOT EXISTS DbEmpresas;
USE DbEmpresas;

CREATE TABLE Usuarios (
    EmpresaId CHAR(36) NOT NULL,
    UserId CHAR(36) NOT NULL,
    UserNome VARCHAR(100) NOT NULL,
    UserLogin VARCHAR(50) NOT NULL,
    UserPass VARCHAR(255) NOT NULL,
    UserStatus TINYINT NOT NULL,
    UserDateCreate DATETIME NOT NULL,
    UserDateUpdate DATETIME,
    UserIdLog CHAR(36),
    PRIMARY KEY (EmpresaId, UserId)
);
