CREATE OR REPLACE PROCEDURE football_playertable_create (
    p_Name VARCHAR2,
    p_Surname VARCHAR2,
    p_Age NUMBER,
    p_DateOfBirth DATE,
    p_Nationality VARCHAR2,
    p_Position VARCHAR2,
    p_CurrentClub VARCHAR2,
    p_Height NUMBER,
    p_Weight NUMBER,
    p_PreferredFoot VARCHAR2,
    p_PlayerId OUT NUMBER
) IS
    v_PlayerStatsId NUMBER;
BEGIN
    -- Create default player statistics
    football_playerstatstablev_create(
        p_Goals => 0,
        p_Assists => 0,
        p_TotalMinutesIn => 0,
        p_PassAccuracy => 0,
        p_Tackles => 0,
        p_Interceptions => 0,
        p_Clearances => 0,
        p_Shots => 0,
        p_ShotsOnTarget => 0,
        p_DribblesCompleted => 0,
        p_AerialDuelsWon => 0,
        p_YellowCards => 0,
        p_RedCards => 0,
        p_FoulsCommitted => 0,
        p_FoulsSuffered => 0,
        p_Offsides => 0,
        p_Saves => 0,
        p_CleanSheets => 0,
        p_PlayerStatsId => v_PlayerStatsId
    );

    -- Insert into PlayerTable
    INSERT INTO PlayerTable (
        Name, Surname, Age, DateOfBirth, Nationality, Position, CurrentClub, Height, Weight, PreferredFoot, PlayerStatsId
    ) VALUES (
        p_Name, p_Surname, p_Age, p_DateOfBirth, p_Nationality, p_Position, p_CurrentClub, p_Height, p_Weight, p_PreferredFoot, v_PlayerStatsId
    )
    RETURNING PlayerId INTO p_PlayerId;
    
    DBMS_OUTPUT.PUT_LINE('Player created with ID: ' || p_PlayerId);
END;
/


CREATE OR REPLACE PROCEDURE football_playertable_getById (
    p_PlayerId IN NUMBER,
    p_Cursor OUT SYS_REFCURSOR
) IS
BEGIN
    OPEN p_Cursor FOR
    SELECT * FROM PlayerTable
    WHERE PlayerId = p_PlayerId;
END;
/

CREATE OR REPLACE PROCEDURE football_playertable_getAll (
    p_Cursor OUT SYS_REFCURSOR
) IS
BEGIN
    OPEN p_Cursor FOR
    SELECT * FROM PlayerTable;
END;
/


CREATE OR REPLACE PROCEDURE football_playertable_getByPagination (
    p_PageSize IN NUMBER,
    p_PageNumber IN NUMBER,
    p_Cursor OUT SYS_REFCURSOR
) IS
BEGIN
    OPEN p_Cursor FOR
    SELECT * FROM (
        SELECT a.*, ROWNUM rnum FROM (
            SELECT * FROM PlayerTable ORDER BY PlayerId
        ) a
        WHERE ROWNUM <= p_PageSize * p_PageNumber
    )
    WHERE rnum > p_PageSize * (p_PageNumber - 1);
END;
/


CREATE OR REPLACE PROCEDURE football_playertable_update (
    p_PlayerId IN NUMBER,
    p_Name VARCHAR2,
    p_Surname VARCHAR2,
    p_Age NUMBER,
    p_DateOfBirth DATE,
    p_Nationality VARCHAR2,
    p_Position VARCHAR2,
    p_CurrentClub VARCHAR2,
    p_Height NUMBER,
    p_Weight NUMBER,
    p_PreferredFoot VARCHAR2,
    p_PlayerStatsId NUMBER
) IS
BEGIN
    UPDATE PlayerTable
    SET Name = p_Name,
        Surname = p_Surname,
        Age = p_Age,
        DateOfBirth = p_DateOfBirth,
        Nationality = p_Nationality,
        Position = p_Position,
        CurrentClub = p_CurrentClub,
        Height = p_Height,
        Weight = p_Weight,
        PreferredFoot = p_PreferredFoot,
        PlayerStatsId = p_PlayerStatsId
    WHERE PlayerId = p_PlayerId;
END;
/


CREATE OR REPLACE PROCEDURE football_playertable_delete (
    p_PlayerId IN NUMBER
) IS
BEGIN
    DELETE FROM PlayerTable
    WHERE PlayerId = p_PlayerId;
END;
/
