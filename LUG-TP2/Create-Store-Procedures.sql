USE [GestionAlumnos]
GO

/****** Objeto: StoredProcedure [dbo].[sp_AlumnoActualizar] Fecha de script: 28/6/2026 03:40:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_AlumnoActualizar]
    @legajo             INT,
    @nombre_apellido    VARCHAR(200),
    @documento          INT,
    @fecha_nacimiento   DATE,
    @calle_numero       VARCHAR(250),
    @ciudad             VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

            UPDATE Alumno
            SET nombre_apellido  = @nombre_apellido,
                documento        = @documento,
                fecha_nacimiento = @fecha_nacimiento
            WHERE legajo = @legajo;

            -- Si no tenia direccion la inserta, si tenia la actualiza
            IF EXISTS (SELECT 1 FROM Direccion WHERE id_legajo = @legajo)
                UPDATE Direccion
                SET calle_numero = @calle_numero,
                    ciudad       = @ciudad
                WHERE id_legajo = @legajo;
            ELSE
                INSERT INTO Direccion (id_legajo, calle_numero, ciudad)
                VALUES (@legajo, @calle_numero, @ciudad);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO


CREATE PROCEDURE [dbo].[sp_AlumnoBorrar]
    @legajo INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE Alumno
        SET activo = 0
        WHERE legajo = @legajo;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO


CREATE PROCEDURE [dbo].[sp_AlumnoInsertar]
    @nombre_apellido    VARCHAR(200),
    @documento          INT,
    @fecha_nacimiento   DATE,
    @calle_numero       VARCHAR(250),
    @ciudad             VARCHAR(100),
    @nuevo_legajo       INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

            INSERT INTO Alumno (nombre_apellido, documento, fecha_nacimiento, activo)
            VALUES (@nombre_apellido, @documento, @fecha_nacimiento, 1);

            SET @nuevo_legajo = SCOPE_IDENTITY();

            INSERT INTO Direccion (id_legajo, calle_numero, ciudad)
            VALUES (@nuevo_legajo, @calle_numero, @ciudad);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[sp_AlumnoObtenerPorLegajo]
    @legajo INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT 
            a.legajo,
            a.nombre_apellido,
            a.documento,
            a.fecha_nacimiento,
            a.activo,
            d.calle_numero,
            d.ciudad
        FROM Alumno a
        LEFT JOIN Direccion d ON d.id_legajo = a.legajo
        WHERE a.legajo = @legajo;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[sp_AlumnoObtenerTodos]
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT 
            a.legajo,
            a.nombre_apellido,
            a.documento,
            a.fecha_nacimiento,
            a.activo,
            d.calle_numero,
            d.ciudad
        FROM Alumno a
        LEFT JOIN Direccion d ON d.id_legajo = a.legajo
        WHERE a.activo = 1
        ORDER BY a.nombre_apellido;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

