-- Create Procedure for PlayerTeamLinkTable
CREATE OR REPLACE PROCEDURE player_teamlink_create (
    p_PlayerId NUMBER,
    p_TeamId NUMBER,
    p_StartDate DATE,
    p_EndDate DATE,
    p_IsLoan VARCHAR2,
    p_PlayerTeamLinkId OUT NUMBER
) IS
BEGIN
    INSERT INTO PlayerTeamLinkTable (
        PlayerId, TeamId, StartDate, EndDate, IsLoan
    ) VALUES (
        p_PlayerId, p_TeamId, p_StartDate, p_EndDate, p_IsLoan
    )
    RETURNING PlayerTeamLinkId INTO p_PlayerTeamLinkId;
    
    DBMS_OUTPUT.PUT_LINE('PlayerTeamLink created with ID: ' || p_PlayerTeamLinkId);
END;
/

-- Get By ID Procedure for PlayerTeamLinkTable
CREATE OR REPLACE PROCEDURE player_teamlink_getById (
    p_PlayerTeamLinkId IN NUMBER,
    p_Cursor OUT SYS_REFCURSOR
) IS
BEGIN
    OPEN p_Cursor FOR
    SELECT * FROM PlayerTeamLinkTable
    WHERE PlayerTeamLinkId = p_PlayerTeamLinkId;
END;
/

-- Get All Procedure for PlayerTeamLinkTable
CREATE OR REPLACE PROCEDURE player_teamlink_getAll (
    p_Cursor OUT SYS_REFCURSOR
) IS
BEGIN
    OPEN p_Cursor FOR
    SELECT * FROM PlayerTeamLinkTable;
END;
/

-- Update Procedure for PlayerTeamLinkTable
CREATE OR REPLACE PROCEDURE player_teamlink_update (
    p_PlayerTeamLinkId IN NUMBER,
    p_PlayerId NUMBER,
    p_TeamId NUMBER,
    p_StartDate DATE,
    p_EndDate DATE,
    p_IsLoan VARCHAR2
) IS
BEGIN
    UPDATE PlayerTeamLinkTable
    SET PlayerId = p_PlayerId,
        TeamId = p_TeamId,
        StartDate = p_StartDate,
        EndDate = p_EndDate,
        IsLoan = p_IsLoan
    WHERE PlayerTeamLinkId = p_PlayerTeamLinkId;
END;
/

-- Delete Procedure for PlayerTeamLinkTable
CREATE OR REPLACE PROCEDURE player_teamlink_delete (
    p_PlayerTeamLinkId IN NUMBER
) IS
BEGIN
    DELETE FROM PlayerTeamLinkTable
    WHERE PlayerTeamLinkId = p_PlayerTeamLinkId;
END;
/
