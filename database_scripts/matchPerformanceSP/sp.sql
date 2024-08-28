-- Create Procedure for MatchStatisticsTable
CREATE OR REPLACE PROCEDURE match_statistics_create (
    p_MatchId NUMBER,
    p_HomeGoals NUMBER,
    p_AwayGoals NUMBER,
    p_HomePossession NUMBER,
    p_AwayPossession NUMBER,
    p_HomeShots NUMBER,
    p_AwayShots NUMBER,
    p_HomeShotsOnTarget NUMBER,
    p_AwayShotsOnTarget NUMBER,
    p_HomeFouls NUMBER,
    p_AwayFouls NUMBER,
    p_HomeYellowCards NUMBER,
    p_AwayYellowCards NUMBER,
    p_HomeRedCards NUMBER,
    p_AwayRedCards NUMBER,
    p_MatchStatsId OUT NUMBER
) IS
BEGIN
    INSERT INTO MatchStatisticsTable (
        MatchId, HomeGoals, AwayGoals, HomePossession, AwayPossession, HomeShots, AwayShots, 
        HomeShotsOnTarget, AwayShotsOnTarget, HomeFouls, AwayFouls, HomeYellowCards, 
        AwayYellowCards, HomeRedCards, AwayRedCards
    ) VALUES (
        p_MatchId, p_HomeGoals, p_AwayGoals, p_HomePossession, p_AwayPossession, p_HomeShots, 
        p_AwayShots, p_HomeShotsOnTarget, p_AwayShotsOnTarget, p_HomeFouls, p_AwayFouls, 
        p_HomeYellowCards, p_AwayYellowCards, p_HomeRedCards, p_AwayRedCards
    )
    RETURNING MatchStatsId INTO p_MatchStatsId;
    
    DBMS_OUTPUT.PUT_LINE('MatchStatistics created with ID: ' || p_MatchStatsId);
END;
/

-- Get By ID Procedure for MatchStatisticsTable
CREATE OR REPLACE PROCEDURE match_statistics_getById (
    p_MatchStatsId IN NUMBER,
    p_Cursor OUT SYS_REFCURSOR
) IS
BEGIN
    OPEN p_Cursor FOR
    SELECT * FROM MatchStatisticsTable
    WHERE MatchStatsId = p_MatchStatsId;
END;
/

-- Get All Procedure for MatchStatisticsTable
CREATE OR REPLACE PROCEDURE match_statistics_getAll (
    p_Cursor OUT SYS_REFCURSOR
) IS
BEGIN
    OPEN p_Cursor FOR
    SELECT * FROM MatchStatisticsTable;
END;
/

-- Update Procedure for MatchStatisticsTable
CREATE OR REPLACE PROCEDURE match_statistics_update (
    p_MatchStatsId IN NUMBER,
    p_MatchId NUMBER,
    p_HomeGoals NUMBER,
    p_AwayGoals NUMBER,
    p_HomePossession NUMBER,
    p_AwayPossession NUMBER,
    p_HomeShots NUMBER,
    p_AwayShots NUMBER,
    p_HomeShotsOnTarget NUMBER,
    p_AwayShotsOnTarget NUMBER,
    p_HomeFouls NUMBER,
    p_AwayFouls NUMBER,
    p_HomeYellowCards NUMBER,
    p_AwayYellowCards NUMBER,
    p_HomeRedCards NUMBER,
    p_AwayRedCards NUMBER
) IS
BEGIN
    UPDATE MatchStatisticsTable
    SET MatchId = p_MatchId,
        HomeGoals = p_HomeGoals,
        AwayGoals = p_AwayGoals,
        HomePossession = p_HomePossession,
        AwayPossession = p_AwayPossession,
        HomeShots = p_HomeShots,
        AwayShots = p_AwayShots,
        HomeShotsOnTarget = p_HomeShotsOnTarget,
        AwayShotsOnTarget = p_AwayShotsOnTarget,
        HomeFouls = p_HomeFouls,
        AwayFouls = p_AwayFouls,
        HomeYellowCards = p_HomeYellowCards,
        AwayYellowCards = p_AwayYellowCards,
        HomeRedCards = p_HomeRedCards,
        AwayRedCards = p_AwayRedCards
    WHERE MatchStatsId = p_MatchStatsId;
END;
/

-- Delete Procedure for MatchStatisticsTable
CREATE OR REPLACE PROCEDURE match_statistics_delete (
    p_MatchStatsId IN NUMBER
) IS
BEGIN
    DELETE FROM MatchStatisticsTable
    WHERE MatchStatsId = p_MatchStatsId;
END;
/
