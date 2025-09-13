-- =====================================
-- Script de creación de la base de datos y datos de ejemplo para ABank
-- =====================================

-- 1. Crear secuencia para el Id de usuarios
CREATE SEQUENCE users_id_seq;

-- 2. Crear la tabla de usuarios
CREATE TABLE Users (
    Id INT PRIMARY KEY DEFAULT nextval('users_id_seq'),
    Nombres VARCHAR(50) NOT NULL,
    Apellidos VARCHAR(50) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    Direccion VARCHAR(100) NOT NULL,
    Password VARCHAR(120) NOT NULL,
    Telefono VARCHAR(20) NOT NULL,
    Email VARCHAR(50) NOT NULL,
    Estado CHAR(1) CHECK (Estado IN ('A', 'I')),
    FechaCreacion TIMESTAMP NOT NULL,
    FechaModificacion TIMESTAMP
);

-- 3. Función y trigger para actualizar FechaModificacion
CREATE OR REPLACE FUNCTION update_timestamps()
RETURNS TRIGGER AS $$
BEGIN
    NEW.FechaModificacion = NOW();
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- 4. Trigger para actualizar FechaModificacion en UPDATE
CREATE TRIGGER users_update_timestamps
BEFORE UPDATE ON Users
FOR EACH ROW
EXECUTE FUNCTION update_timestamps();

-- 5. Función y trigger para establecer FechaCreacion en INSERT
CREATE OR REPLACE FUNCTION set_creation_date()
RETURNS TRIGGER AS $$
BEGIN
    NEW.FechaCreacion = NOW();
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- 6. Trigger para establecer FechaCreacion en INSERT
CREATE TRIGGER users_creation_date
BEFORE INSERT ON Users
FOR EACH ROW
EXECUTE FUNCTION set_creation_date();

-- 7. Insertar datos de ejemplo en la tabla Users
-- Usuario de ejemplo: Juan Perez
INSERT INTO Users (Nombres, Apellidos, FechaNacimiento, Direccion, Password, Telefono, Email, Estado) VALUES ('Juan', 'Perez','1990-05-15', '13 calle poniente', 'UdkHB167@%@78','25551234', 'juan.perez@gmail.com', 'A');

-- Usuario de ejemplo: Maria Gomez
INSERT INTO Users (Nombres, Apellidos, FechaNacimiento, Direccion, Password, Telefono, Email, Estado) VALUES ('Maria', 'Gomez','1988-08-22', 'Avenida 742', 'MnHSYU17@%@78','25555678', 'maria.gomez@gmail.com', 'A');

-- Usuario de ejemplo: Carlos Lopez
INSERT INTO Users (Nombres, Apellidos, FechaNacimiento, Direccion, Password, Telefono, Email, Estado) VALUES ('Carlos', 'Lopez','1995-11-03', 'Boulevard el paraiso', 'Ouehsysus167@%@78','25559012', 'carlos.lopez@gmail.com', 'A');

-- Usuario de ejemplo: Ana Diaz
INSERT INTO Users (Nombres, Apellidos, FechaNacimiento, Direccion, Password, Telefono, Email, Estado) VALUES ('Ana', 'Diaz','1992-02-28', '89 calle oriente', 'nHgtB167@%@78','25553456', 'ana.diaz@gmail.com', 'A');

-- Usuario de ejemplo: Pedro Martinez
INSERT INTO Users (Nombres, Apellidos, FechaNacimiento, Direccion, Password, Telefono, Email, Estado) VALUES ('Pedro', 'Martinez','1985-07-10', 'avenida 101', 'UdkHB127@%@78','25557890', 'pedro.martinez@gmail.com', 'A');

-- Usuario de ejemplo: Laura Sanchez
INSERT INTO Users (Nombres, Apellidos, FechaNacimiento, Direccion, Password, Telefono, Email, Estado) VALUES ('Laura', 'Sanchez','1998-04-01', 'plaza central', 'UdkHB167@%@78','25551122', 'laura.sanchez@gmail.com', 'A');

-- Usuario de ejemplo: Sofia Rodriguez
INSERT INTO Users (Nombres, Apellidos, FechaNacimiento, Direccion, Password, Telefono, Email, Estado) VALUES ('Sofia', 'Rodriguez','2001-09-09', 'paseo 56', 'PAhhdsB457@%@78','25553344', 'sofia.rodriguez@gmail.com', 'A');

-- Usuario de ejemplo: Diego Fernandez
INSERT INTO Users (Nombres, Apellidos, FechaNacimiento, Direccion, Password, Telefono, Email, Estado) VALUES ('Diego', 'Fernandez','1980-01-20', 'camino del bosque 16', 'JJ88g6s6@%@78','25555566', 'diego.fernandez@gmail.com', 'A');

-- Usuario de ejemplo: Elena Torres
INSERT INTO Users (Nombres, Apellidos, FechaNacimiento, Direccion, Password, Telefono, Email, Estado) VALUES ('Elena', 'Torres','1994-06-30', 'calle del la montana', 'mbbUDUU77@%@78','25557788', 'elena.torres@gmail.com', 'A');

-- Usuario de ejemplo: Gabriel Ruiz
INSERT INTO Users (Nombres, Apellidos, FechaNacimiento, Direccion, Password, Telefono, Email, Estado) VALUES ('Gabriel', 'Ruiz','1997-12-05', 'ruta de la costa', 'TYney772j@%78','25559900', 'gabriel.ruiz@gmail.com', 'A');
