

DECLARE
    v_PlayerStatsId NUMBER;
BEGIN
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
    DBMS_OUTPUT.PUT_LINE('PlayerStatsId: ' || v_PlayerStatsId);
END;
/

select * from playerstatstablev

DECLARE
    v_PlayerStatsId NUMBER;
BEGIN
    football_playerstatstablev_create(
        p_Goals => 10,
        p_Assists => 5,
        p_TotalMinutesIn => 90,
        p_PassAccuracy => 85,
        p_Tackles => 3,
        p_Interceptions => 2,
        p_Clearances => 1,
        p_Shots => 4,
        p_ShotsOnTarget => 2,
        p_DribblesCompleted => 7,
        p_AerialDuelsWon => 5,
        p_YellowCards => 1,
        p_RedCards => 0,
        p_FoulsCommitted => 3,
        p_FoulsSuffered => 2,
        p_Offsides => 1,
        p_Saves => 0,
        p_CleanSheets => 1,
        p_PlayerStatsId => v_PlayerStatsId
    );
    DBMS_OUTPUT.PUT_LINE('PlayerStatsId: ' || v_PlayerStatsId);
END;
/



DECLARE
    v_Cursor SYS_REFCURSOR;
    v_Goals NUMBER;
    v_Assists NUMBER;
    v_TotalMinutesIn NUMBER;
    v_PassAccuracy NUMBER;
    v_Tackles NUMBER;
    v_Interceptions NUMBER;
    v_Clearances NUMBER;
    v_Shots NUMBER;
    v_ShotsOnTarget NUMBER;
    v_DribblesCompleted NUMBER;
    v_AerialDuelsWon NUMBER;
    v_YellowCards NUMBER;
    v_RedCards NUMBER;
    v_FoulsCommitted NUMBER;
    v_FoulsSuffered NUMBER;
    v_Offsides NUMBER;
    v_Saves NUMBER;
    v_CleanSheets NUMBER;
    v_PlayerStatsId NUMBER;
BEGIN
    DBMS_OUTPUT.PUT_LINE('start point case');
    football_playerstatstablev_getById(p_PlayerStatsId => 3, p_Cursor => v_Cursor);
    FETCH v_Cursor INTO v_Goals, v_Assists, v_TotalMinutesIn, v_PassAccuracy, v_Tackles, v_Interceptions, v_Clearances, v_Shots, v_ShotsOnTarget, v_DribblesCompleted, v_AerialDuelsWon, v_YellowCards, v_RedCards, v_FoulsCommitted, v_FoulsSuffered, v_Offsides, v_Saves, v_CleanSheets, v_PlayerStatsId;
    IF v_Cursor%FOUND THEN
        DBMS_OUTPUT.PUT_LINE('PlayerStatsId: ' || v_PlayerStatsId);
    ELSE
        DBMS_OUTPUT.PUT_LINE('No record found');
    END IF;
    CLOSE v_Cursor;
END;
/



DECLARE
    v_Cursor SYS_REFCURSOR;
    v_Record PlayerStatsTableV%ROWTYPE;
BEGIN
    football_playerstatstablev_getByPagination(p_PageSize => 5, p_PageNumber => 1, p_Cursor => v_Cursor);
    LOOP
        FETCH v_Cursor INTO v_Record;
        EXIT WHEN v_Cursor%NOTFOUND;
        DBMS_OUTPUT.PUT_LINE('PlayerStatsId: ' || v_Record.PlayerStatsId);
    END LOOP;
    CLOSE v_Cursor;
END;
/


BEGIN
    football_playerstatstablev_update(
        p_PlayerStatsId => 1,
        p_Goals => 15,
        p_Assists => 10
    );
END;
/


BEGIN
    football_playerstatstablev_delete(p_PlayerStatsId => 1);
END;
/