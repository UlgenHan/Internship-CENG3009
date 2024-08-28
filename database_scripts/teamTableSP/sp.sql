-- Create Procedure to Insert Team
CREATE OR REPLACE PROCEDURE football_teamtable_create (
    p_Name VARCHAR2,
    p_Stadium VARCHAR2,
    p_Coach VARCHAR2,
    p_FoundedYear NUMBER,
    p_City VARCHAR2,
    p_TeamId OUT NUMBER
) IS
BEGIN
    INSERT INTO TeamTable (
        Name, Stadium, Coach, FoundedYear, City
    ) VALUES (
        p_Name, p_Stadium, p_Coach, p_FoundedYear, p_City
    )
    RETURNING TeamId INTO p_TeamId;
    
    DBMS_OUTPUT.PUT_LINE('Team created with ID: ' || p_TeamId);
END;
/

-- Create Procedure to Get Team by ID
CREATE OR REPLACE PROCEDURE football_teamtable_getById (
    p_TeamId IN NUMBER,
    p_Cursor OUT SYS_REFCURSOR
) IS
BEGIN
    OPEN p_Cursor FOR
    SELECT * FROM TeamTable
    WHERE TeamId = p_TeamId;
END;
/

-- Create Procedure to Get All Teams
CREATE OR REPLACE PROCEDURE football_teamtable_getAll (
    p_Cursor OUT SYS_REFCURSOR
) IS
BEGIN
    OPEN p_Cursor FOR
    SELECT * FROM TeamTable;
END;
/

-- Create Procedure to Get Teams by Pagination
CREATE OR REPLACE PROCEDURE football_teamtable_getByPagination (
    p_PageSize IN NUMBER,
    p_PageNumber IN NUMBER,
    p_Cursor OUT SYS_REFCURSOR
) IS
BEGIN
    OPEN p_Cursor FOR
    SELECT * FROM (
        SELECT a.*, ROWNUM rnum FROM (
            SELECT * FROM TeamTable ORDER BY TeamId
        ) a
        WHERE ROWNUM <= p_PageSize * p_PageNumber
    )
    WHERE rnum > p_PageSize * (p_PageNumber - 1);
END;
/

-- Create Procedure to Update Team
CREATE OR REPLACE PROCEDURE football_teamtable_update (
    p_TeamId IN NUMBER,
    p_Name VARCHAR2,
    p_Stadium VARCHAR2,
    p_Coach VARCHAR2,
    p_FoundedYear NUMBER,
    p_City VARCHAR2
) IS
BEGIN
    UPDATE TeamTable
    SET Name = p_Name,
        Stadium = p_Stadium,
        Coach = p_Coach,
        FoundedYear = p_FoundedYear,
        City = p_City
    WHERE TeamId = p_TeamId;
END;
/

-- Create Procedure to Delete Team
CREATE OR REPLACE PROCEDURE football_teamtable_delete (
    p_TeamId IN NUMBER
) IS
BEGIN
    DELETE FROM TeamTable
    WHERE TeamId = p_TeamId;
END;
/
