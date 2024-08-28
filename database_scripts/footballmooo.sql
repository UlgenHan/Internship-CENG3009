-- Test Case 1: Successful Insertion
DECLARE
    v_TeamStatsId NUMBER;
BEGIN
    -- Call the procedure
    football_teamstatisticstable_create(
        p_TeamId => 1,
        p_SeasonYear => '2023',
        p_GoalsScored => 45,
        p_GoalsConceded => 30,
        p_Wins => 15,
        p_Draws => 5,
        p_Losses => 10,
        p_HomeWins => 8,
        p_AwayWins => 7,
        p_RecentForm => 'WWLWD',
        p_TeamStatsId => v_TeamStatsId
    );

    -- Check if the record is inserted
    IF v_TeamStatsId IS NOT NULL THEN
        DBMS_OUTPUT.PUT_LINE('Test Case 1 Passed: TeamStatsId is ' || v_TeamStatsId);
    ELSE
        DBMS_OUTPUT.PUT_LINE('Test Case 1 Failed: No TeamStatsId returned');
    END IF;
END;
/

-- Test Case 2: Null or Empty Inputs
DECLARE
    v_TeamStatsId NUMBER;
BEGIN
    -- Call the procedure with null inputs
    BEGIN
        football_teamstatisticstable_create(
            p_TeamId => NULL,
            p_SeasonYear => NULL,
            p_GoalsScored => NULL,
            p_GoalsConceded => NULL,
            p_Wins => NULL,
            p_Draws => NULL,
            p_Losses => NULL,
            p_HomeWins => NULL,
            p_AwayWins => NULL,
            p_RecentForm => NULL,
            p_TeamStatsId => v_TeamStatsId
        );
        DBMS_OUTPUT.PUT_LINE('Test Case 2 Failed: Procedure did not handle null inputs');
    EXCEPTION
        WHEN OTHERS THEN
            DBMS_OUTPUT.PUT_LINE('Test Case 2 Passed: Null inputs handled correctly');
    END;
END;
/
