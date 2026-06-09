USE GestionAlumnos;
GO;

-- Usuario: admin | Clave: admin123
INSERT INTO Usuario (nombre_usuario, clave_hash) 
VALUES ('admin', '240be518ebb2146c006fd2c869f3d030d55776a31f0226176313a48e4266a2be');

-- Usuario: pepe | Clave: 1234
INSERT INTO Usuario (nombre_usuario, clave_hash) 
VALUES ('pepe', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4');

-- Usuario: invitado | Clave: invitado
INSERT INTO Usuario (nombre_usuario, clave_hash) 
VALUES ('invitado', '39cf9e54ef7792694a50d24f0c43666d6d2b451556094b89f8a3138b704c7f12');
