-- Create Procedure for MatchPerformancesTable
CREATE OR REPLACE PROCEDURE match_performance_create (
    p_PlayerId NUMBER,
    p_MatchId NUMBER,
    p_Goals NUMBER,
    p_Assists NUMBER,
    p_MinutesPlayed NUMBER,
    p_PassAccuracy NUMBER,
    p_Tackles NUMBER,
    p_Interceptions NUMBER,
    p_Clearances NUMBER,
    p_Shots NUMBER,
    p_ShotsOnTarget NUMBER,
    p_DribblesCompleted NUMBER,
    p_AerialDuelsWon NUMBER,
    p_YellowCards NUMBER,
    p_RedCards NUMBER,
    p_FoulsCommitted NUMBER,
    p_FoulsSuffered NUMBER,
    p_Offsides NUMBER,
    p_Saves NUMBER,
    p_CleanSheets NUMBER,
    p_MatchPerformanceId OUT NUMBER
) IS
BEGIN
    INSERT INTO MatchPerformancesTable (
        PlayerId, MatchId, Goals, Assists, MinutesPlayed, PassAccuracy, Tackles, Interceptions, Clearances, 
        Shots, ShotsOnTarget, DribblesCompleted, AerialDuelsWon, YellowCards, RedCards, FoulsCommitted, 
        FoulsSuffered, Offsides, Saves, CleanSheets
    ) VALUES (
        p_PlayerId, p_MatchId, p_Goals, p_Assists, p_MinutesPlayed, p_PassAccuracy, p_Tackles, p_Interceptions, 
        p_Clearances, p_Shots, p_ShotsOnTarget, p_DribblesCompleted, p_AerialDuelsWon, p_YellowCards, 
        p_RedCards, p_FoulsCommitted, p_FoulsSuffered, p_Offsides, p_Saves, p_CleanSheets
    )
    RETURNING MatchPerformanceId INTO p_MatchPerformanceId;
    
    DBMS_OUTPUT.PUT_LINE('MatchPerformance created with ID: ' || p_MatchPerformanceId);
END;
/

-- Get By ID Procedure for MatchPerformancesTable
CREATE OR REPLACE PROCEDURE match_performance_getById (
    p_MatchPerformanceId IN NUMBER,
    p_Cursor OUT SYS_REFCURSOR
) IS
BEGIN
    OPEN p_Cursor FOR
    SELECT * FROM MatchPerformancesTable
    WHERE MatchPerformanceId = p_MatchPerformanceId;
END;
/

-- Get All Procedure for MatchPerformancesTable
CREATE OR REPLACE PROCEDURE match_performance_getAll (
    p_Cursor OUT SYS_REFCURSOR
) IS
BEGIN
    OPEN p_Cursor FOR
    SELECT * FROM MatchPerformancesTable;
END;
/

-- Update Procedure for MatchPerformancesTable
CREATE OR REPLACE PROCEDURE match_performance_update (
    p_MatchPerformanceId IN NUMBER,
    p_PlayerId NUMBER,
    p_MatchId NUMBER,
    p_Goals NUMBER,
    p_Assists NUMBER,
    p_MinutesPlayed NUMBER,
    p_PassAccuracy NUMBER,
    p_Tackles NUMBER,
    p_Interceptions NUMBER,
    p_Clearances NUMBER,
    p_Shots NUMBER,
    p_ShotsOnTarget NUMBER,
    p_DribblesCompleted NUMBER,
    p_AerialDuelsWon NUMBER,
    p_YellowCards NUMBER,
    p_RedCards NUMBER,
    p_FoulsCommitted NUMBER,
    p_FoulsSuffered NUMBER,
    p_Offsides NUMBER,
    p_Saves NUMBER,
    p_CleanSheets NUMBER
) IS
BEGIN
    UPDATE MatchPerformancesTable
    SET PlayerId = p_PlayerId,
        MatchId = p_MatchId,
        Goals = p_Goals,
        Assists = p_Assists,
        MinutesPlayed = p_MinutesPlayed,
        PassAccuracy = p_PassAccuracy,
        Tackles = p_Tackles,
        Interceptions = p_Interceptions,
        Clearances = p_Clearances,
        Shots = p_Shots,
        ShotsOnTarget = p_ShotsOnTarget,
        DribblesCompleted = p_DribblesCompleted,
        AerialDuelsWon = p_AerialDuelsWon,
        YellowCards = p_YellowCards,
        RedCards = p_RedCards,
        FoulsCommitted = p_FoulsCommitted,
        FoulsSuffered = p_FoulsSuffered,
        Offsides = p_Offsides,
        Saves = p_Saves,
        CleanSheets = p_CleanSheets
    WHERE MatchPerformanceId = p_MatchPerformanceId;
END;
/

-- Delete Procedure for MatchPerformancesTable
CREATE OR REPLACE PROCEDURE match_performance_delete (
    p_MatchPerformanceId IN NUMBER
) IS
BEGIN
    DELETE FROM MatchPerformancesTable
    WHERE MatchPerformanceId = p_MatchPerformanceId;
END;
/
