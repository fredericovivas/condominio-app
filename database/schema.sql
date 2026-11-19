CREATE DATABASE IF NOT EXISTS condominio_db
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE condominio_db;

CREATE TABLE apartamento (
    id_apartamento INT AUTO_INCREMENT PRIMARY KEY,
    bloco          VARCHAR(10) NOT NULL,
    piso           INT         NOT NULL,
    numero         VARCHAR(10) NOT NULL,
    UNIQUE KEY uq_apartamento (bloco, numero)
);

CREATE TABLE condomino (
    id_condomino   INT AUTO_INCREMENT PRIMARY KEY,
    nome           VARCHAR(100) NOT NULL,
    email          VARCHAR(100),
    telefone       VARCHAR(20),
    id_apartamento INT NOT NULL,
    FOREIGN KEY (id_apartamento) REFERENCES apartamento(id_apartamento)
        ON DELETE RESTRICT ON UPDATE CASCADE
);

CREATE TABLE evento (
    id_evento INT AUTO_INCREMENT PRIMARY KEY,
    titulo    VARCHAR(150) NOT NULL,
    tipo      ENUM('reuniao', 'manutencao', 'prazo_pagamento') NOT NULL,
    data      DATETIME NOT NULL,
    descricao TEXT
);

CREATE TABLE pagamento (
    id_pagamento INT AUTO_INCREMENT PRIMARY KEY,
    valor        DECIMAL(10, 2) NOT NULL,
    estado       ENUM('pago', 'em_divida') NOT NULL DEFAULT 'em_divida',
    data_registo DATETIME DEFAULT CURRENT_TIMESTAMP,
    id_condomino INT NOT NULL,
    id_evento    INT NOT NULL,
    FOREIGN KEY (id_condomino) REFERENCES condomino(id_condomino)
        ON DELETE RESTRICT ON UPDATE CASCADE,
    FOREIGN KEY (id_evento) REFERENCES evento(id_evento)
        ON DELETE RESTRICT ON UPDATE CASCADE
);

CREATE TABLE intervencao (
    id_intervencao INT AUTO_INCREMENT PRIMARY KEY,
    descricao      TEXT NOT NULL,
    custo          DECIMAL(10, 2),
    data           DATETIME NOT NULL,
    id_evento      INT NOT NULL,
    FOREIGN KEY (id_evento) REFERENCES evento(id_evento)
        ON DELETE RESTRICT ON UPDATE CASCADE
);

CREATE TABLE notificacao (
    id_notificacao INT AUTO_INCREMENT PRIMARY KEY,
    mensagem       TEXT NOT NULL,
    data_envio     DATETIME DEFAULT CURRENT_TIMESTAMP,
    id_condomino   INT NOT NULL,
    FOREIGN KEY (id_condomino) REFERENCES condomino(id_condomino)
        ON DELETE RESTRICT ON UPDATE CASCADE
);