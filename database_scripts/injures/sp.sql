CREATE OR REPLACE PROCEDURE injuries_suspensions_create (
    p_PlayerId IN NUMBER,
    p_Type IN VARCHAR2,
    p_Description IN CLOB,
    p_StartDate IN DATE,
    p_EndDate IN DATE,
    p_InjurySuspensionId OUT NUMBER
) AS
BEGIN
    INSERT INTO InjuriesSuspensionsTable (PlayerId, Type, Description, StartDate, EndDate)
    VALUES (p_PlayerId, p_Type, p_Description, p_StartDate, p_EndDate)
    RETURNING InjurySuspensionId INTO p_InjurySuspensionId;
END;
/
 
CREATE OR REPLACE PROCEDURE injuries_suspensions_getById (
    p_InjurySuspensionId IN NUMBER,
    p_PlayerId OUT NUMBER,
    p_Type OUT VARCHAR2,
    p_Description OUT CLOB,
    p_StartDate OUT DATE,
    p_EndDate OUT DATE
) AS
BEGIN
    SELECT PlayerId, Type, Description, StartDate, EndDate
    INTO p_PlayerId, p_Type, p_Description, p_StartDate, p_EndDate
    FROM InjuriesSuspensionsTable
    WHERE InjurySuspensionId = p_InjurySuspensionId;
END;
/
 
CREATE OR REPLACE PROCEDURE injuries_suspensions_getAll (
    p_Cursor OUT SYS_REFCURSOR
) AS
BEGIN
    OPEN p_Cursor FOR
    SELECT * FROM InjuriesSuspensionsTable;
END;
/
 
CREATE OR REPLACE PROCEDURE injuries_suspensions_update (
    p_InjurySuspensionId IN NUMBER,
    p_PlayerId IN NUMBER,
    p_Type IN VARCHAR2,
    p_Description IN CLOB,
    p_StartDate IN DATE,
    p_EndDate IN DATE
) AS
BEGIN
    UPDATE InjuriesSuspensionsTable
    SET PlayerId = p_PlayerId,
        Type = p_Type,
        Description = p_Description,
        StartDate = p_StartDate,
        EndDate = p_EndDate
    WHERE InjurySuspensionId = p_InjurySuspensionId;
END;
/
 
CREATE OR REPLACE PROCEDURE injuries_suspensions_delete (
    p_InjurySuspensionId IN NUMBER
) AS
BEGIN
    DELETE FROM InjuriesSuspensionsTable
    WHERE InjurySuspensionId = p_InjurySuspensionId;
END;
/
