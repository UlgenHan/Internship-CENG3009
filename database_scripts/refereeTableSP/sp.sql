-- Create Referee table
CREATE OR REPLACE PROCEDURE football_refereetable_create (
    p_Name VARCHAR2,
    p_Surname VARCHAR2,
    p_Nationality VARCHAR2,
    p_ExperienceYears NUMBER,
    p_Bias NUMBER,
    p_RefereeId OUT NUMBER
) IS
BEGIN
    INSERT INTO RefereeTable (
        Name, Surname, Nationality, ExperienceYears, Bias
    ) VALUES (
        p_Name, p_Surname, p_Nationality, p_ExperienceYears, p_Bias
    )
    RETURNING RefereeId INTO p_RefereeId;
    
    DBMS_OUTPUT.PUT_LINE('Referee created with ID: ' || p_RefereeId);
END;
/
-- Get By ID Procedure
CREATE OR REPLACE PROCEDURE football_refereetable_getById (
    p_RefereeId IN NUMBER,
    p_Cursor OUT SYS_REFCURSOR
) IS
BEGIN
    OPEN p_Cursor FOR
    SELECT * FROM RefereeTable
    WHERE RefereeId = p_RefereeId;
END;
/
-- Get All Procedure
CREATE OR REPLACE PROCEDURE football_refereetable_getAll (
    p_Cursor OUT SYS_REFCURSOR
) IS
BEGIN
    OPEN p_Cursor FOR
    SELECT * FROM RefereeTable;
END;
/
-- Get By Pagination Procedure
CREATE OR REPLACE PROCEDURE football_refereetable_getByPagination (
    p_PageSize IN NUMBER,
    p_PageNumber IN NUMBER,
    p_Cursor OUT SYS_REFCURSOR
) IS
BEGIN
    OPEN p_Cursor FOR
    SELECT * FROM (
        SELECT a.*, ROWNUM rnum FROM (
            SELECT * FROM RefereeTable ORDER BY RefereeId
        ) a
        WHERE ROWNUM <= p_PageSize * p_PageNumber
    )
    WHERE rnum > p_PageSize * (p_PageNumber - 1);
END;
/
-- Update Procedure
CREATE OR REPLACE PROCEDURE football_refereetable_update (
    p_RefereeId IN NUMBER,
    p_Name VARCHAR2,
    p_Surname VARCHAR2,
    p_Nationality VARCHAR2,
    p_ExperienceYears NUMBER,
    p_Bias NUMBER
) IS
BEGIN
    UPDATE RefereeTable
    SET Name = p_Name,
        Surname = p_Surname,
        Nationality = p_Nationality,
        ExperienceYears = p_ExperienceYears,
        Bias = p_Bias
    WHERE RefereeId = p_RefereeId;
END;
/
-- Delete Procedure
CREATE OR REPLACE PROCEDURE football_refereetable_delete (
    p_RefereeId IN NUMBER
) IS
BEGIN
    DELETE FROM RefereeTable
    WHERE RefereeId = p_RefereeId;
END;
/
