-- Create Procedure for MatchTable
CREATE OR REPLACE PROCEDURE match_create (
    p_HomeTeamId NUMBER,
    p_AwayTeamId NUMBER,
    p_MatchDate DATE,
    p_Stadium VARCHAR2,
    p_RefereeId NUMBER,
    p_WeatherConditions VARCHAR2,
    p_Importance VARCHAR2,
    p_MatchId OUT NUMBER
) IS
BEGIN
    INSERT INTO MatchTable (
        HomeTeamId, AwayTeamId, MatchDate, Stadium, RefereeId, WeatherConditions, Importance
    ) VALUES (
        p_HomeTeamId, p_AwayTeamId, p_MatchDate, p_Stadium, p_RefereeId, p_WeatherConditions, p_Importance
    )
    RETURNING MatchId INTO p_MatchId;
    
    DBMS_OUTPUT.PUT_LINE('Match created with ID: ' || p_MatchId);
END;
/

-- Get By ID Procedure for MatchTable
CREATE OR REPLACE PROCEDURE match_getById (
    p_MatchId IN NUMBER,
    p_Cursor OUT SYS_REFCURSOR
) IS
BEGIN
    OPEN p_Cursor FOR
    SELECT * FROM MatchTable
    WHERE MatchId = p_MatchId;
END;
/

-- Get All Procedure for MatchTable
CREATE OR REPLACE PROCEDURE match_getAll (
    p_Cursor OUT SYS_REFCURSOR
) IS
BEGIN
    OPEN p_Cursor FOR
    SELECT * FROM MatchTable;
END;
/

-- Update Procedure for MatchTable
CREATE OR REPLACE PROCEDURE match_update (
    p_MatchId IN NUMBER,
    p_HomeTeamId NUMBER,
    p_AwayTeamId NUMBER,
    p_MatchDate DATE,
    p_Stadium VARCHAR2,
    p_RefereeId NUMBER,
    p_WeatherConditions VARCHAR2,
    p_Importance VARCHAR2
) IS
BEGIN
    UPDATE MatchTable
    SET HomeTeamId = p_HomeTeamId,
        AwayTeamId = p_AwayTeamId,
        MatchDate = p_MatchDate,
        Stadium = p_Stadium,
        RefereeId = p_RefereeId,
        WeatherConditions = p_WeatherConditions,
        Importance = p_Importance
    WHERE MatchId = p_MatchId;
END;
/

-- Delete Procedure for MatchTable
CREATE OR REPLACE PROCEDURE match_delete (
    p_MatchId IN NUMBER
) IS
BEGIN
    DELETE FROM MatchTable
    WHERE MatchId = p_MatchId;
END;
/
