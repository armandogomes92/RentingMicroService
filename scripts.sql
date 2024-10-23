CREATE TABLE motorcycles (
    identificador VARCHAR(255) PRIMARY KEY,
    ano INT NOT NULL,
    modelo VARCHAR(255) NOT NULL,
    placa VARCHAR(255) NOT NULL UNIQUE
); 

CREATE TABLE deliverymen (
    identificador VARCHAR(255) PRIMARY KEY,
    nome VARCHAR(255) NOT NULL,
    cnpj VARCHAR(11) NOT NULL UNIQUE,
    data_nascimento TIMESTAMP NOT NULL,
    numero_cnh VARCHAR(255) NOT NULL UNIQUE,
    tipo_cnh VARCHAR(2) NOT NULL
);

CREATE TABLE rentals (
    identificador SERIAL PRIMARY KEY,
    entregador_id VARCHAR(255) NOT NULL,
    moto_id VARCHAR(255) NOT NULL,
    data_inicio TIMESTAMP NOT NULL,
    data_termino TIMESTAMP NOT NULL,
    data_previsao_termino TIMESTAMP NOT NULL,
    data_devolucao TIMESTAMP,
    plano INT NOT NULL,
    rented BOOLEAN NOT NULL,
    valor_diaria FLOAT,
    valor_total DECIMAL,
    CONSTRAINT fk_entregador
        FOREIGN KEY(entregador_id) 
        REFERENCES deliverymen(identificador),
    CONSTRAINT fk_moto
        FOREIGN KEY(moto_id) 
        REFERENCES motorcycles(identificador)
);
-- Inserir 3 motos
INSERT INTO motorcycles (identificador, ano, modelo, placa) VALUES
('moto1', 2020, 'Modelo A', 'ABC1234'),
('moto2', 2021, 'Modelo B', 'DEF5678'),
('moto3', 2022, 'Modelo C', 'GHI9012');

-- Inserir 2 entregadores
INSERT INTO deliverymen (identificador, nome, cnpj, data_nascimento, numero_cnh, tipo_cnh) VALUES
('entregador1', 'João Silva', '12345678901', '1994-10-13 08:00:00', '12345678900', 'A'),
('entregador2', 'Maria Souza', '98765432100', '1984-10-13 08:00:00', '12345678901', 'AB');

-- Inserir 1 locação
INSERT INTO rentals (entregador_id, moto_id, data_inicio, data_termino, data_previsao_termino, plano, rented, valor_diaria, valor_total) VALUES
('entregador1', 'moto1', '2024-10-12 08:00:00', '2024-10-13 08:00:00', '2024-10-13 08:00:00', 7, TRUE, 100.0, 100.0);