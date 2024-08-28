-- Create Procedure to Insert Team Statistics
CREATE OR REPLACE PROCEDURE football_teamstatisticstable_create (
    p_TeamId NUMBER,
    p_SeasonYear VARCHAR2,
    p_GoalsScored NUMBER,
    p_GoalsConceded NUMBER,
    p_Wins NUMBER,
    p_Draws NUMBER,
    p_Losses NUMBER,
    p_HomeWins NUMBER,
    p_AwayWins NUMBER,
    p_RecentForm VARCHAR2,
    p_TeamStatsId OUT NUMBER
) IS
BEGIN
    INSERT INTO TeamStatisticsTable (
        TeamId, SeasonYear, GoalsScored, GoalsConceded, Wins, Draws, Losses, HomeWins, AwayWins, RecentForm
    ) VALUES (
        p_TeamId, p_SeasonYear, p_GoalsScored, p_GoalsConceded, p_Wins, p_Draws, p_Losses, p_HomeWins, p_AwayWins, p_RecentForm
    )
    RETURNING TeamStatsId INTO p_TeamStatsId;
    
    DBMS_OUTPUT.PUT_LINE('TeamStatistics created with ID: ' || p_TeamStatsId);
END;
/

-- Create Procedure to Get Team Statistics by ID
CREATE OR REPLACE PROCEDURE football_teamstatisticstable_getById (
    p_TeamStatsId IN NUMBER,
    p_Cursor OUT SYS_REFCURSOR
) IS
BEGIN
    OPEN p_Cursor FOR
    SELECT * FROM TeamStatisticsTable
    WHERE TeamStatsId = p_TeamStatsId;
END;
/

-- Create Procedure to Get All Team Statistics
CREATE OR REPLACE PROCEDURE football_teamstatisticstable_getAll (
    p_Cursor OUT SYS_REFCURSOR
) IS
BEGIN
    OPEN p_Cursor FOR
    SELECT * FROM TeamStatisticsTable;
END;
/

-- Create Procedure to Get Team Statistics by Pagination
CREATE OR REPLACE PROCEDURE football_teamstatisticstable_getByPagination (
    p_PageSize IN NUMBER,
    p_PageNumber IN NUMBER,
    p_Cursor OUT SYS_REFCURSOR
) IS
BEGIN
    OPEN p_Cursor FOR
    SELECT * FROM (
        SELECT a.*, ROWNUM rnum FROM (
            SELECT * FROM TeamStatisticsTable ORDER BY TeamStatsId
        ) a
        WHERE ROWNUM <= p_PageSize * p_PageNumber
    )
    WHERE rnum > p_PageSize * (p_PageNumber - 1);
END;
/

-- Create Procedure to Update Team Statistics
CREATE OR REPLACE PROCEDURE football_teamstatisticstable_update (
    p_TeamStatsId IN NUMBER,
    p_TeamId NUMBER,
    p_SeasonYear VARCHAR2,
    p_GoalsScored NUMBER,
    p_GoalsConceded NUMBER,
    p_Wins NUMBER,
    p_Draws NUMBER,
    p_Losses NUMBER,
    p_HomeWins NUMBER,
    p_AwayWins NUMBER,
    p_RecentForm VARCHAR2
) IS
BEGIN
    UPDATE TeamStatisticsTable
    SET TeamId = p_TeamId,
        SeasonYear = p_SeasonYear,
        GoalsScored = p_GoalsScored,
        GoalsConceded = p_GoalsConceded,
        Wins = p_Wins,
        Draws = p_Draws,
        Losses = p_Losses,
        HomeWins = p_HomeWins,
        AwayWins = p_AwayWins,
        RecentForm = p_RecentForm
    WHERE TeamStatsId = p_TeamStatsId;
END;
/

-- Create Procedure to Delete Team Statistics
CREATE OR REPLACE PROCEDURE football_teamstatisticstable_delete (
    p_TeamStatsId IN NUMBER
) IS
BEGIN
    DELETE FROM TeamStatisticsTable
    WHERE TeamStatsId = p_TeamStatsId;
END;
/
